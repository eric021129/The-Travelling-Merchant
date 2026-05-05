using UnityEngine;
using TMPro;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    public ItemCatalog catalog;

    [Header("Visuals")]
    public GameObject[] skins;   // Drag the 9 skin GameObjects here

    [Header("UI")]
    public GameObject speechBubble;
    public TextMeshProUGUI bubbleText;

    [HideInInspector] public ItemData requestedItem;
    [HideInInspector] public int requestedQuantity;

    public void SetSkin(int skinIndex)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i] != null)
                skins[i].SetActive(i == skinIndex);
        }
    }

    private void Start()
    {
        int index = Random.Range(0, catalog.items.Length);
        requestedItem = catalog.items[index];
        requestedQuantity = Random.Range(1, 6);

        bubbleText.text = $"{requestedItem.itemName} x{requestedQuantity}";
        speechBubble.SetActive(false);

        Debug.Log($"{gameObject.name} wants {requestedQuantity}x {requestedItem.itemName} (price: {requestedItem.price}₩ each)");
    }

    public string GetPrompt() => $"Press E to trade ({requestedQuantity}x {requestedItem.itemName})";
    public KeyType GetKey() => KeyType.E;
    public void Interact() => TradingUI.Instance.Open(this);

    public void ShowBubble() => speechBubble.SetActive(true);
    public void HideBubble() => speechBubble.SetActive(false);
}