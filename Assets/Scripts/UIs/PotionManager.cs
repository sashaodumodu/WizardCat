using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PotionManager : MonoBehaviour
{
    [Header("UI References")]
    public Image potImage;             // Center pot
    public Image characterImage;       // Left character
    public GameObject inventoryPanel;  // Right inventory panel
    public GameObject recipePopup;     // Recipe popup panel

    [Header("Potion Sprites")]
    public Sprite defaultPotion;   // default pot + char
    public Sprite goodPotion;      // Ypotion
    public Sprite badPotion;       // Xpotion
    public Sprite confusedPotion;  // huhpotion

    [Header("Inventory Sprites")]
    public Sprite ywater, xwater;
    public Sprite yfruit, nfruit;
    public Sprite ypoison, npoison;
    public Sprite bottle;

    // Track used items
    private Dictionary<string, bool> usedItems = new Dictionary<string, bool>();

    // Items currently in pot
    private List<string> itemsInPot = new List<string>();

    void Start()
    {
        // Set default images
        potImage.sprite = defaultPotion;
        characterImage.sprite = defaultPotion;
    }

    // Called by buttons: inventory click
    public void UseItem(string itemName)
    {
        // Check if item is already used
        if (usedItems.ContainsKey(itemName) && usedItems[itemName])
        {
            // Show confused character
            characterImage.sprite = confusedPotion;
            return;
        }

        // Check if item is usable in pot
        if (itemName == "ywater" || itemName == "yfruit" || itemName == "ypoison")
        {
            // Add to pot
            itemsInPot.Add(itemName);

            // Check recipes
            CheckPotions();

            // Mark item as used
            usedItems[itemName] = true;

            // Update inventory icon
            UpdateInventoryIcon(itemName);
        }
        else
        {
            // Non-usable item: bottle or already empty items
            characterImage.sprite = confusedPotion;
        }
    }

    void CheckPotions()
    {
        // Healing potion recipe: ywater + yfruit
        if (itemsInPot.Contains("ywater") && itemsInPot.Contains("yfruit"))
        {
            potImage.sprite = goodPotion;
            characterImage.sprite = goodPotion;
            itemsInPot.Clear();
        }
        // Poison potion recipe: ywater + ypoison
        else if (itemsInPot.Contains("ywater") && itemsInPot.Contains("ypoison"))
        {
            potImage.sprite = badPotion;
            characterImage.sprite = badPotion;
            itemsInPot.Clear();
        }
    }

    void UpdateInventoryIcon(string itemName)
    {
        // Find the inventory button in the panel
        Transform itemButton = inventoryPanel.transform.Find(itemName);
        if (itemButton)
        {
            Image img = itemButton.GetComponent<Image>();

            // Replace with X version after used
            switch (itemName)
            {
                case "ywater": img.sprite = xwater; break;
                case "yfruit": img.sprite = nfruit; break;
                case "ypoison": img.sprite = npoison; break;
                case "bottle": img.sprite = bottle; break; // stays same, cannot be used
            }
        }
    }

    // Called by recipe button
    public void ShowRecipePopup(bool show)
    {
        if (recipePopup)
            recipePopup.SetActive(show);
    }
}