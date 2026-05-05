using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Money")]
    public TextMeshProUGUI moneyText;

    [Header("Tuition")]
    public Image tuitionFill;
    public TextMeshProUGUI tuitionText;

    [Header("Held Item")]
    public GameObject heldItemGroup;
    public Image heldItemIcon;
    public TextMeshProUGUI heldItemCount;

    public ItemCatalog catalog;  // to look up item icons by ID

    private void Update()
    {
        if (PlayerState.Instance == null) return;

        UpdateMoney();
        UpdateTuition();
        UpdateHeldItem();
    }

    private void UpdateMoney()
    {
        moneyText.text = $"₩ {PlayerState.Instance.Money:N0}";
    }

    private void UpdateTuition()
    {
        int paid = PlayerState.Instance.TuitionPaid;
        int goal = PlayerState.TuitionGoal;

        tuitionFill.fillAmount = (float)paid / goal;
        tuitionText.text = $"{paid:N0} / {goal:N0}";
    }

    private void UpdateHeldItem()
    {
        if (!PlayerState.Instance.IsCarrying)
        {
            heldItemGroup.SetActive(false);
            return;
        }

        heldItemGroup.SetActive(true);

        // Look up item in catalog
        string heldID = PlayerState.Instance.HeldItemID;
        ItemData item = null;
        foreach (var i in catalog.items)
        {
            if (i.itemID == heldID)
            {
                item = i;
                break;
            }
        }

        if (item != null && item.icon != null)
            heldItemIcon.sprite = item.icon;

        heldItemCount.text = $"x{PlayerState.Instance.HeldItemCount}";
    }
}