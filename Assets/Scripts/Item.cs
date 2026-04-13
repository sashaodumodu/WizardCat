using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "RumpledCode/Item", order = 1)]
public class Item : ScriptableObject
{
    public string id;
    public string description;
    public Sprite icon;
    public GameObject prefab;
    
    [Header("Combinations")]
    public ItemCombination[] combinations;
}

[System.Serializable]
public class ItemCombination
{
    public Item otherItem;
    public Item result;
}