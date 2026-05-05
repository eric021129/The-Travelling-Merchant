using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TruckItemTile : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI countText;
    public Button minusButton;
    public Button plusButton;
    public CanvasGroup canvasGroup;

    private ItemData _item;

    public void Setup(ItemData item)
    {
        _item = item;

        iconImage.sprite = item.icon;
        nameText.text = item.itemName;

        minusButton.onClick.AddListener(OnMinus);
        plusButton.onClick.AddListener(OnPlus);

        UpdateUI();
    }

    private void OnPlus()
    {
        // Move 1 from truck to hands
        if (TruckInventory.Instance.Remove(_item.itemID, 1))
        {
            if (PlayerState.Instance.PickupItem(_item.itemID, 1))
            {
                // Success
            }
            else
            {
                // PickupItem failed (carrying different type) — return to truck
                TruckInventory.Instance.Add(_item.itemID, 1);
            }
        }
        TruckUI.Instance.RefreshAllTiles();
    }

    private void OnMinus()
    {
        // Move 1 from hands to truck (only valid if currently holding this item)
        if (PlayerState.Instance.HeldItemID == _item.itemID)
        {
            if (PlayerState.Instance.RemoveHeldItems(1))
            {
                TruckInventory.Instance.Add(_item.itemID, 1);
            }
        }
        TruckUI.Instance.RefreshAllTiles();
    }

    public void UpdateUI()
    {
        int count = TruckInventory.Instance.GetCount(_item.itemID);
        countText.text = $"In truck: {count}";

        bool isCarryingThis = PlayerState.Instance.HeldItemID == _item.itemID;
        bool isCarryingNothing = !PlayerState.Instance.IsCarrying;

        // Enable interactions only if not carrying anything OR carrying this same type
        bool interactable = isCarryingNothing || isCarryingThis;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = interactable ? 1f : 0.4f;
            canvasGroup.interactable = interactable;
        }

        plusButton.interactable = count > 0 && interactable;
        minusButton.interactable = isCarryingThis && PlayerState.Instance.HeldItemCount > 0;
    }
}