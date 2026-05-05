using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string itemID;

    [FormerlySerializedAs("productName")]
    public string itemName;

    [FormerlySerializedAs("basePrice")]
    public int price;

    public Sprite icon;
    public GameObject prefab;
}