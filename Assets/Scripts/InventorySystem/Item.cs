using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CustomCode/Item", order = 1)] //creating a new type of asset
public class Item : ScriptableObject //declaring the item class as extending from the ScriptObj class
{
    public string id; //unique id
    public string description; //"metadata"
    public Sprite icon; //icon
    public GameObject prefab; //the GO in the game
}