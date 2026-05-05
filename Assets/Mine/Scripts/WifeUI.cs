using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WifeUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI tuitionStatusText;
    public TMP_InputField amountInput;
    public Button payButton;
    public TextMeshProUGUI resultText;

    private void Awake()
    {
        payButton.onClick.AddListener(OnPay);
    }

    private void OnEnable()
    {
        RefreshUI();
        amountInput.text = "";
        resultText.text = "";
    }

    private void RefreshUI()
    {
        if (PlayerState.Instance == null) return;

        moneyText.text = $"Your money: ₩ {PlayerState.Instance.Money:N0}";

        int paid = PlayerState.Instance.TuitionPaid;
        int goal = PlayerState.TuitionGoal;
        tuitionStatusText.text = $"Tuition paid: {paid:N0} / {goal:N0}";
    }

    private void OnPay()
    {
        if (!int.TryParse(amountInput.text, out int amount))
        {
            resultText.text = "Please enter a valid number.";
            return;
        }

        if (amount <= 0)
        {
            resultText.text = "Amount must be positive.";
            return;
        }

        if (amount % 100 != 0)
        {
            resultText.text = "Amount must be in increments of 100₩.";
            return;
        }

        // Cap by player money and remaining tuition
        int remaining = PlayerState.TuitionGoal - PlayerState.Instance.TuitionPaid;
        int maxPayable = Mathf.Min(PlayerState.Instance.Money, remaining);

        if (amount > maxPayable)
        {
            resultText.text = $"Maximum you can pay now: ₩ {maxPayable:N0}";
            return;
        }

        PlayerState.Instance.PayTuition(amount);
        resultText.text = $"Paid ₩ {amount:N0}";

        amountInput.text = "";
        RefreshUI();

        // Check win condition
        if (PlayerState.Instance.HasWon)
        {
            resultText.text = "🎉 Tuition fully paid! YOU WIN!";
            // Phase F will handle the actual win screen
        }
    }
}