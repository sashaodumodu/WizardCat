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