using System;
using System.Collections.Generic;
using UnityEngine;

// This script keeps track of potion ingredients based on the main Inventory!
// Ie. If Heart_fruit is picked up and put inside of the inventory, 
// it will update the potion ingrediants list, so the player can only use what they have
public class PotionIngredientsManager : MonoBehaviour
{
    [Serializable]
    public class IngredientBinding
    {
        // The ID from the inventory item (like heart_fruit)
        [Tooltip("The Item.id from Inventory. ie: heart_fruit")]
        public string inventoryItemId;

        // The ID used in the potion system (like yfruit)
        [Tooltip("The potion ingredient name used by PotionManager. ie: yfruit")]
        public string ingredientId;
    }

    [Header("References")]
    [SerializeField] private Inventory inventory; // inventory from sasha's scene

    [Header("ID")]
    [SerializeField] private List<IngredientBinding> ingredientBindings = new List<IngredientBinding>();

    // stores how many of each ingredient we have
    private readonly Dictionary<string, int> ingredientCounts = new Dictionary<string, int>();

    // converts inventory item id into ingredient id
    private readonly Dictionary<string, string> inventoryToIngredient = new Dictionary<string, string>();

    // converts ingredient id into inventory item id
    private readonly Dictionary<string, string> ingredientToInventory = new Dictionary<string, string>();

    // event to tell other scripts (like UI) that something changed
    public event Action OnIngredientsChanged;

    private void Awake()
    {
        // approxtrain...... sets up the LUTs
        BuildLookupTables();
        EnsureKnownKeys();
    }

    private void OnEnable()
    {
        //  inventory events
        if (inventory != null)
        {
            inventory.ItemAdded += HandleItemAdded;
            inventory.ItemRemoved += HandleItemRemoved;
        }
    }

    private void Start()
    {
        // build counts from whatever is already in inventory
        RebuildCountsFromInventory();
    }

    private void OnDisable()
    {
        // disable
        if (inventory != null)
        {
            inventory.ItemAdded -= HandleItemAdded;
            inventory.ItemRemoved -= HandleItemRemoved;
        }
    }

    
    private void BuildLookupTables()
    {
        inventoryToIngredient.Clear();
        ingredientToInventory.Clear();

        foreach (var binding in ingredientBindings)
        {
            if (binding == null)
            {
                continue;
            }

            // make sure values are Not empty
            if (!string.IsNullOrWhiteSpace(binding.inventoryItemId) &&
                !string.IsNullOrWhiteSpace(binding.ingredientId))
            {
                inventoryToIngredient[binding.inventoryItemId] = binding.ingredientId;
                ingredientToInventory[binding.ingredientId] = binding.inventoryItemId;
            }
        }
    }

    // makes sure all ingredient keys exist in the dictionary
    private void EnsureKnownKeys()
    {
        foreach (var binding in ingredientBindings)
        {
            if (binding != null && !string.IsNullOrWhiteSpace(binding.ingredientId))
            {
                if (!ingredientCounts.ContainsKey(binding.ingredientId))
                {
                    ingredientCounts[binding.ingredientId] = 0;
                }
            }
        }
    }

    // rebuilds all counts. using inventory
    private void RebuildCountsFromInventory()
    {
        ingredientCounts.Clear();
        EnsureKnownKeys();

        if (inventory == null)
        {
            OnIngredientsChanged?.Invoke();
            return;
        }

        // go through all items in inventory
        foreach (var item in inventory.GetAllItems())
        {
            AddItemToCounts(item, 1, notify: false);
        }

        // update UI after everything is counted
        OnIngredientsChanged?.Invoke();
    }

    // when an item is ADDED to inventory
    private void HandleItemAdded(Item item)
    {
        AddItemToCounts(item, 1, notify: true);
    }

    //  when an item is REMOVED from inventory
    private void HandleItemRemoved(Item item)
    {
        AddItemToCounts(item, -1, notify: true);
    }

    // adds or removes from ingredient #s
    private void AddItemToCounts(Item item, int delta, bool notify)
    {
        string ingredientId = ResolveIngredientId(item);

        if (string.IsNullOrWhiteSpace(ingredientId))
        {
            return;
        }

        // if not already in dictionary, add it
        if (!ingredientCounts.ContainsKey(ingredientId))
        {
            ingredientCounts[ingredientId] = 0;
        }

        // update count but don't let it go below 0 bc you can't have negative of an item
        ingredientCounts[ingredientId] = Mathf.Max(0, ingredientCounts[ingredientId] + delta);

        // tell UI something changed
        if (notify)
        {
            OnIngredientsChanged?.Invoke();
        }
    }

    // converts an Item to a potion ingredient
    private string ResolveIngredientId(Item item)
    {
        if (item == null || string.IsNullOrWhiteSpace(item.id))
        {
            return null;
        }

        // check if we have a custom mapping
        if (inventoryToIngredient.TryGetValue(item.id, out string mappedIngredientId))
        {
            return mappedIngredientId;
        }

       
        return item.id;
    }

    // converts ingredient id back to inventory id
    private string ResolveInventoryItemId(string ingredientId)
    {
        if (ingredientToInventory.TryGetValue(ingredientId, out string mappedInventoryId))
        {
            return mappedInventoryId;
        }

        return ingredientId;
    }

    // returns how many of an ingredient we have
    public int GetCount(string ingredientId)
    {
        if (string.IsNullOrWhiteSpace(ingredientId))
        {
            return 0;
        }

        return ingredientCounts.TryGetValue(ingredientId, out int count) ? count : 0;
    }

    // checks if we have at least one
    public bool HasIngredient(string ingredientId)
    {
        return GetCount(ingredientId) > 0;
    }

    // tries to use/remove an ingredient
    public bool TryConsumeIngredient(string ingredientId)
    {
        if (!HasIngredient(ingredientId))
        {
            return false;
        }

        // remove from real inventory if possible
        if (inventory != null)
        {
            string inventoryItemId = ResolveInventoryItemId(ingredientId);
            return inventory.ConsumeItemById(inventoryItemId);
        }

        ingredientCounts[ingredientId] = Mathf.Max(0, GetCount(ingredientId) - 1);
        OnIngredientsChanged?.Invoke();
        return true;
    }

    // refresh
    public void RefreshFromInventory()
    {
        RebuildCountsFromInventory();
    }
}


//https://learn.unity.com/tutorial/introduction-to-scriptableobjects?language=en
// https://generalistprogrammer.com/tutorials/engines/unity/inventory-system
// https://unity.com/how-to/architect-game-code-scriptable-objects
// https://www.reddit.com/r/Unity3D/comments/nooktv/inventory_with_scriptable_objects/

// Decided to make a 2nd potion ing manager instead of focing the potion manage to do everything


/*

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    InventoryUI ui;
    [SerializeField]
    AudioSource audioSource;

    [Header("Prefabs")]
    [SerializeField]
    GameObject droppedItemPrefab;

    [Header("Audio Clips")]
    [SerializeField]
    AudioClip pickUpItemAudio;
    [SerializeField]
    AudioClip dropItemAudio;

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, Item> inventory = new();

    // KB added code: start
    // Events so other scripts (like PotionIngredientsManager) know when inventory changes
    public event Action<Item> ItemAdded;
    public event Action<Item> ItemRemoved;

    // Returns all items (used to rebuild counts)
    public List<Item> GetAllItems()
    {
        return new List<Item>(inventory.Values);
    }

    // Removes ONE item by its Item.id (used by potion system)
    public bool ConsumeItemById(string itemId)
    {
        string foundKey = null;
        Item foundItem = null;

        foreach (var kvp in inventory)
        {
            if (kvp.Value != null && kvp.Value.id == itemId)
            {
                foundKey = kvp.Key;
                foundItem = kvp.Value;
                break;
            }
        }

        if (foundKey == null)
        {
            return false;
        }

        inventory.Remove(foundKey);
        ui.RemoveUIItem(foundKey);

        // notify listeners
        ItemRemoved?.Invoke(foundItem);

        return true;
    }
    // KB added code: End ^

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DroppedItem"))
        {
            var droppedItem = other.GetComponent<DroppedItem>();
            if (droppedItem.pickedUp)
            {
                return;
            }
            droppedItem.pickedUp = true;
            AddItem(droppedItem.item);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(pickUpItemAudio);
        }
    }
    
    void AddItem(Item item)
    {
        var inventoryId = Guid.NewGuid().ToString();
        inventory.Add(inventoryId, item);
        ui.AddUIItem(inventoryId, item);

        // KB added code: start
        // tell other systems an item was added
        ItemAdded?.Invoke(item);
        // KB added code: End ^
    }

    public void DropItem(string inventoryId)
    {
        var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<DroppedItem>();
        var item = inventory.GetValueOrDefault(inventoryId);
        droppedItem.Initialize(item);
        inventory.Remove(inventoryId);
        ui.RemoveUIItem(inventoryId);
        audioSource.PlayOneShot(dropItemAudio);

        // KB added code: start
        // tell other systems an item was removed
        if (item != null)
        {
            ItemRemoved?.Invoke(item);
        }
        // KB added code: End ^
    }
}



*/