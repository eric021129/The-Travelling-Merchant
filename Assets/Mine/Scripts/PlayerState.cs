using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    [Header("Money")]
    [SerializeField] private int money = 500000;

    [Header("Tuition")]
    [SerializeField] private int tuitionPaid = 0;
    public const int TuitionGoal = 1000000;

    [Header("Held Item")]
    [SerializeField] private string heldItemID = null;
    [SerializeField] private int heldItemCount = 0;

    public int Money => money;
    public int TuitionPaid => tuitionPaid;
    public string HeldItemID => heldItemID;
    public int HeldItemCount => heldItemCount;
    public bool IsCarrying => !string.IsNullOrEmpty(heldItemID) && heldItemCount > 0;
    public bool HasWon => tuitionPaid >= TuitionGoal;

    private void Awake()
    {
        Instance = this;
    }

    // Money
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log($"Earned {amount:N0}₩. Total: {money:N0}₩");
    }

    public bool SpendMoney(int amount)
    {
        if (amount > money)
        {
            Debug.Log("Not enough money!");
            return false;
        }
        money -= amount;
        return true;
    }

    // Tuition
    public void PayTuition(int amount)
    {
        if (SpendMoney(amount))
        {
            tuitionPaid += amount;
            Debug.Log($"Paid {amount:N0}₩ toward tuition. Total paid: {tuitionPaid:N0} / {TuitionGoal:N0}₩");

            if (HasWon)
            {
                Debug.Log("🎉 YOU WON! Tuition fully paid!");
                if (WinUI.Instance != null)
                    WinUI.Instance.ShowWin();
            }
        }
    }

    // Held item management
    public bool PickupItem(string itemID, int amount = 1)
    {
        if (!IsCarrying)
        {
            heldItemID = itemID;
            heldItemCount = amount;
            return true;
        }

        if (heldItemID == itemID)
        {
            heldItemCount += amount;
            return true;
        }

        Debug.Log("You can only carry one type of item at a time!");
        return false;
    }

    public bool ReturnItem(int amount = 1)
    {
        if (!IsCarrying) return false;

        heldItemCount -= amount;
        if (heldItemCount <= 0)
        {
            heldItemID = null;
            heldItemCount = 0;
        }
        return true;
    }

    public bool RemoveHeldItems(int amount)
    {
        if (!IsCarrying || heldItemCount < amount) return false;

        heldItemCount -= amount;
        if (heldItemCount <= 0)
        {
            heldItemID = null;
            heldItemCount = 0;
        }
        return true;
    }
}