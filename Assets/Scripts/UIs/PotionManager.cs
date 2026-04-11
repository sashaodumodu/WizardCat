using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    // Track remaining uses
    private Dictionary<string, int> itemCounts = new Dictionary<string, int>();

    // Items currently in pot
    private List<string> itemsInPot = new List<string>();

    // Track which buttons are currently "selected" (green)
    private List<Button> selectedButtons = new List<Button>();

    void Start()
    {
        potImage.sprite = defaultPotion;
        characterImage.sprite = defaultPotion;

        // Initialize counts
        itemCounts["ywater"] = 2;
        itemCounts["yfruit"] = 1;
        itemCounts["ypoison"] = 1;
        itemCounts["bottle"] = 1;
    }

    public void UseItem(string itemName)
    {
        // Find the button for visual feedback
        Transform itemButton = inventoryPanel.transform.Find(itemName);
        Button btn = itemButton ? itemButton.GetComponent<Button>() : null;

        // Check if item is usable and has remaining quantity
        if (itemCounts.ContainsKey(itemName) && itemCounts[itemName] > 0)
        {
            if (itemName == "ywater" || itemName == "yfruit" || itemName == "ypoison")
            {
                // Add to pot
                itemsInPot.Add(itemName);

                // Flash green on the button
                if (btn != null && !selectedButtons.Contains(btn))
                {
                    btn.image.color = Color.green;
                    selectedButtons.Add(btn);
                }

                CheckPotions();

                // Decrease count
                itemCounts[itemName]--;

                // Update inventory icon if needed
                UpdateInventoryIcon(itemName);
            }
            else
            {
                // Non-usable item (bottle)
                characterImage.sprite = confusedPotion;
            }
        }
        else
        {
            // Item has no remaining uses
            characterImage.sprite = confusedPotion;
        }
    }

    void CheckPotions()
    {
        bool potionMade = false;

        // Healing potion
        if (itemsInPot.Contains("ywater") && itemsInPot.Contains("yfruit"))
        {
            potImage.sprite = goodPotion;
            characterImage.sprite = goodPotion;
            potionMade = true;
        }
        // Poison potion
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
                    img.sprite = itemCounts["ywater"] > 0 ? ywater : xwater;
                    break;
                case "yfruit":
                    img.sprite = itemCounts["yfruit"] > 0 ? yfruit : nfruit;
                    break;
                case "ypoison":
                    img.sprite = itemCounts["ypoison"] > 0 ? ypoison : npoison;
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
}