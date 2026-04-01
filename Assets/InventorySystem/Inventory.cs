using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{
    [Header("References")] 
    [SerializeField]
    InventoryUI ui; //reference to the class
    [SerializeField]
    AudioSource audioSource;
    
    [Header("Prefabs")]
    [SerializeField]
    GameObject droppedItemPrefab;

    [Header("Audio Clips")] [SerializeField] //two audio clips to get from
    AudioClip pickUpItemAudio;
    [SerializeField]
    AudioClip dropItemAudio;

    [Header("State")] 
    [SerializeField] 
    SerializedDictionary<string, Item> inventory = new(); //key and value

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
            Destroy(other.gameObject); //destroy the item in the world
            audioSource.PlayOneShot(pickUpItemAudio); //play audio
        }
    }

    void AddItem(Item item)
    {
        var inventoryId = Guid.NewGuid().ToString(); //generate a new id for the item to allow multiple copies
        inventory.Add(inventoryId, item); //add item to inv
        ui.AddUIItem(inventoryId, item);  //add item to screen ui
    }

    public void DropItem(string inventoryId)
    {
        var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<DroppedItem>();
        var item = inventory.GetValueOrDefault(inventoryId); 
        droppedItem.Initialize(item);
        inventory.Remove(inventoryId);
        ui.RemoveUIItem(inventoryId);
        audioSource.PlayOneShot(dropItemAudio);
    }

}
