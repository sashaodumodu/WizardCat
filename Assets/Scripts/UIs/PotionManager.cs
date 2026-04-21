using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class PotionManager : MonoBehaviour
{
    [Header("UI References")] 
    public Image potImage;
    public Image characterImage;
    public GameObject inventoryPanel;
    public GameObject recipePopup;

    [Header("Potion Sprites")]
    public Sprite defaultPotion;
    public Sprite goodPotion;
    public Sprite badPotion;
    public Sprite confusedPotion;

    [Header("Inventory Sprites")]
    public Sprite ywater, xwater;
    public Sprite yfruit, nfruit;
    public Sprite ypoison, npoison;
    public Sprite bottle;

    // how many left
    //private Dictionary<string, int> itemCounts = new Dictionary<string, int>();

    // Use the new ingredient manager 
    [SerializeField] private PotionIngredientsManager ingredientsManager;

    // Items currently in pot
    private List<string> itemsInPot = new List<string>();

    // Track which buttons are currently "selected" (green)
    private List<Button> selectedButtons = new List<Button>();

    void Start()
    {
        potImage.sprite = defaultPotion;
        characterImage.sprite = defaultPotion;

        // Initialize counts: OLD
        //itemCounts["ywater"] = 2;
        //itemCounts["yfruit"] = 1;
        //itemCounts["ypoison"] = 1;
        //itemCounts["bottle"] = 1;

        UpdateInventoryIcon("ywater");
        UpdateInventoryIcon("yfruit");
        UpdateInventoryIcon("ypoison");
        UpdateInventoryIcon("bottle");

    }

    public void UseItem(string itemName)
    {
        // Find the button for visual feedback
        Transform itemButton = inventoryPanel.transform.Find(itemName);
        Button btn = itemButton ? itemButton.GetComponent<Button>() : null;

        // Only allow usable items
        //if (!itemCounts.ContainsKey(itemName) || itemCounts[itemName] <= 0)
        if (ingredientsManager == null || !ingredientsManager.HasIngredient(itemName))
        {
            characterImage.sprite = confusedPotion;
            return;
        }

        if (itemName != "ywater" && itemName != "yfruit" && itemName != "ypoison")
        {
            characterImage.sprite = confusedPotion;
            return;
        }

        // if fruit and poison conflict, clear previous selection
        if ((itemsInPot.Contains("yfruit") && itemName == "ypoison") ||
            (itemsInPot.Contains("ypoison") && itemName == "yfruit"))
        {
            // Remove the previous selection
            foreach (Button b in selectedButtons)
            {
                if (b != null)
                    b.image.color = Color.white;
            }

            itemsInPot.Clear();
            selectedButtons.Clear();

            // Optionally, show confused character
            characterImage.sprite = confusedPotion;

            // Do NOT add the new item yet
            return;
        }

        //from real inventory, new
        if (!ingredientsManager.TryConsumeIngredient(itemName))
        {
            characterImage.sprite = confusedPotion;
            return;
        }

        // Valid selection: add to pot
        itemsInPot.Add(itemName);

        // Highlight green
        if (btn != null && !selectedButtons.Contains(btn))
        {
            btn.image.color = Color.gray3;
            selectedButtons.Add(btn);
        }

        // Decrement inventory, old
        //itemCounts[itemName]--;

        // Update inventory icon
        UpdateInventoryIcon(itemName);

        // Check for potion completion
        CheckPotions();
    }

    void CheckPotions()
    {
        bool potionMade = false;

        // Healing potion: ywater + yfruit
        if (itemsInPot.Contains("ywater") && itemsInPot.Contains("yfruit"))
        {
            potImage.sprite = goodPotion;
            characterImage.sprite = goodPotion;
            potionMade = true;
        }
        // Poison potion: ywater + ypoison
        else if (itemsInPot.Contains("ywater") && itemsInPot.Contains("ypoison"))
        {
            potImage.sprite = badPotion;
            characterImage.sprite = badPotion;
            potionMade = true;
        }

        if (potionMade)
        {
            itemsInPot.Clear();

            // Reset selected button colors
            foreach (Button btn in selectedButtons)
            {
                if (btn != null)
                    btn.image.color = Color.white;
            }
            selectedButtons.Clear();
        }
    }

    void UpdateInventoryIcon(string itemName)
    {
        Transform itemButton = inventoryPanel.transform.Find(itemName);
        if (itemButton)
        {
            Image img = itemButton.GetComponent<Image>();

            switch (itemName)
            {
                case "ywater":
                    //img.sprite = itemCounts["ywater"] > 0 ? ywater : xwater;
                    img.sprite = ingredientsManager.GetCount("ywater") > 0 ? ywater : xwater;
                    break;
                case "yfruit":
                    //img.sprite = itemCounts["yfruit"] > 0 ? yfruit : nfruit;
                    img.sprite = ingredientsManager.GetCount("yfruit") > 0 ? yfruit : nfruit;
                    break;
                case "ypoison":
                    //img.sprite = itemCounts["ypoison"] > 0 ? ypoison : npoison;
                    img.sprite = ingredientsManager.GetCount("ypoison") > 0 ? ypoison : npoison;
                    break;
                case "bottle":
                    img.sprite = bottle;
                    break;
            }
        }
    }

    public void ShowRecipePopup(bool show)
    {
        if (recipePopup)
            recipePopup.SetActive(show);
    }

    //  flash red on invalid button ---
    private IEnumerator FlashRedButton(Button btn)
    {
        Color original = btn.image.color;
        btn.image.color = Color.red;
        yield return new WaitForSeconds(0.5f); // red for 0.5 seconds
        btn.image.color = original;
    }
}
*/