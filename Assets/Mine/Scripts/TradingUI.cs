using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TradingUI : MonoBehaviour
{
    public static TradingUI Instance;

    [Header("UI References")]
    public GameObject panel;
    public TextMeshProUGUI requestText;
    public TMP_InputField priceInput;
    public Button submitButton;
    public Button closeButton;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI attemptsText;

    private NPCInteractable _currentNPC;
    private int _attemptsLeft;
    private List<int> _rejectedPrices = new List<int>();

    private void Awake()
    {
        Instance = this;
        submitButton.onClick.AddListener(OnSubmit);
        closeButton.onClick.AddListener(Close);
    }

    public void Open(NPCInteractable npc)
    {
        _currentNPC = npc;
        _attemptsLeft = 3;
        _rejectedPrices.Clear();

        int totalBase = npc.requestedItem.price * npc.requestedQuantity;
        requestText.text = $"Wants: {npc.requestedQuantity}x {npc.requestedItem.itemName}\n(Base price: {totalBase:N0}₩ total)";
        resultText.text = "";
        priceInput.text = "";
        UpdateAttemptsText();

        // Check if player can trade with this NPC
        bool canTrade = CheckCanTrade();

        submitButton.interactable = canTrade;
        priceInput.interactable = canTrade;

        UIManager.Instance.OpenTrading();
    }

    private bool CheckCanTrade()
    {
        if (!PlayerState.Instance.IsCarrying)
        {
            resultText.text = "You aren't carrying anything to sell.";
            return false;
        }

        if (PlayerState.Instance.HeldItemID != _currentNPC.requestedItem.itemID)
        {
            resultText.text = $"NPC wants {_currentNPC.requestedItem.itemName}, but you're carrying something else.";
            return false;
        }

        if (PlayerState.Instance.HeldItemCount < _currentNPC.requestedQuantity)
        {
            resultText.text = $"NPC wants {_currentNPC.requestedQuantity}, but you only have {PlayerState.Instance.HeldItemCount}.";
            return false;
        }

        return true;
    }

    public void Close()
    {
        _currentNPC = null;
        UIManager.Instance.CloseAll();
    }

    private void OnSubmit()
    {
        if (_currentNPC == null) return;

        // Re-verify in case state changed
        if (!CheckCanTrade())
        {
            submitButton.interactable = false;
            priceInput.interactable = false;
            return;
        }

        if (!int.TryParse(priceInput.text, out int price))
        {
            resultText.text = "Please enter a valid number.";
            return;
        }

        if (price % 100 != 0)
        {
            resultText.text = "Price must be in increments of 100₩.";
            return;
        }

        if (price <= 0)
        {
            resultText.text = "Price must be positive.";
            return;
        }

        if (_rejectedPrices.Contains(price))
        {
            resultText.text = "You already offered that price. Try lower.";
            return;
        }

        if (_rejectedPrices.Count > 0 && price >= _rejectedPrices[_rejectedPrices.Count - 1])
        {
            resultText.text = "You must offer a lower price than before.";
            return;
        }

        int baseTotal = _currentNPC.requestedItem.price * _currentNPC.requestedQuantity;
        float probability = (2f * baseTotal - price) / (0.6f * baseTotal);
        probability = Mathf.Clamp01(probability);

        float roll = Random.value;

        if (roll <= probability)
        {
            resultText.text = $"Accepted! Sold for {price:N0}₩.";
            submitButton.interactable = false;
            priceInput.interactable = false;

            // Remove items from player and add money
            PlayerState.Instance.RemoveHeldItems(_currentNPC.requestedQuantity);
            PlayerState.Instance.AddMoney(price);

            AudioManager.Instance.PlayTransactionSuccess();  

            Destroy(_currentNPC.gameObject, 1.5f);
            Invoke(nameof(Close), 1.5f);
        }
        else
        {
            _rejectedPrices.Add(price);
            _attemptsLeft--;
            UpdateAttemptsText();

            if (_attemptsLeft <= 0)
            {
                resultText.text = "NPC refuses to trade. Deal failed.";
                submitButton.interactable = false;
                priceInput.interactable = false;

                AudioManager.Instance.PlayTransactionFail(); 

                Destroy(_currentNPC.gameObject, 1.5f);
                Invoke(nameof(Close), 1.5f);
            }
            else
            {
                resultText.text = $"Rejected. Try a lower price.";
            }

            priceInput.text = "";
        }
    }

    private void UpdateAttemptsText()
    {
        attemptsText.text = $"Attempts left: {_attemptsLeft}";
    }
}