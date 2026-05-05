using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorehouseItemTile : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI quantityText;
    public Button minusButton;
    public Button plusButton;
    public Button purchaseButton;

    private ItemData _item;
    private int _quantity = 1;

    public void Setup(ItemData item)
    {
        _item = item;
        _quantity = 1;

        iconImage.sprite = item.icon;
        nameText.text = item.itemName;
        priceText.text = $"₩ {item.price:N0}";

        minusButton.onClick.AddListener(OnMinus);
        plusButton.onClick.AddListener(OnPlus);
        purchaseButton.onClick.AddListener(OnPurchase);

        UpdateUI();
    }

    private void OnPlus()
    {
        _quantity++;
        UpdateUI();
    }

    private void OnMinus()
    {
        if (_quantity > 1) _quantity--;
        UpdateUI();
    }

private void OnPurchase()
{
    int totalCost = _item.price * _quantity;

    if (PlayerState.Instance.SpendMoney(totalCost))
    {
        TruckInventory.Instance.Add(_item.itemID, _quantity);
        Debug.Log($"Bought {_quantity}x {_item.itemID}. Truck now has: {TruckInventory.Instance.GetCount(_item.itemID)}");
        _quantity = 1;
        UpdateUI();
    }
    else
    {
        Debug.Log($"Purchase failed - insufficient money");
    }
}
    private void UpdateUI()
    {
        quantityText.text = _quantity.ToString();
        int totalCost = _item.price * _quantity;

        if (PlayerState.Instance != null)
            purchaseButton.interactable = PlayerState.Instance.Money >= totalCost;
        else
            purchaseButton.interactable = true;
    }
}