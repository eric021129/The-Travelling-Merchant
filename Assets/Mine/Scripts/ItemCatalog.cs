using UnityEngine;

[CreateAssetMenu(fileName = "ItemCatalog", menuName = "Game/Item Catalog")]
public class ItemCatalog : ScriptableObject
{
    public ItemData[] items;
}