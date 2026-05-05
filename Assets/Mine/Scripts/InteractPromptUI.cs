using UnityEngine;
using TMPro;

public class InteractPromptUI : MonoBehaviour
{
    public static InteractPromptUI Instance;

    public GameObject promptObject;
    public TextMeshProUGUI promptText;
    public Interactor interactor;

    private NPCInteractable _lastNPC;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (interactor == null) return;

        // Hide prompt if any UI panel is open
        if (UIManager.Instance != null && UIManager.Instance.IsAnyUIOpen)
        {
            if (_lastNPC != null)
            {
                _lastNPC.HideBubble();
                _lastNPC = null;
            }
            promptObject.SetActive(false);
            return;
        }

        IInteractable target = interactor.CurrentTarget;

        if (_lastNPC != null && (target as NPCInteractable) != _lastNPC)
        {
            _lastNPC.HideBubble();
            _lastNPC = null;
        }

        if (target != null)
        {
            promptText.text = target.GetPrompt();
            promptObject.SetActive(true);

            if (target is NPCInteractable npc)
            {
                npc.ShowBubble();
                _lastNPC = npc;
            }
        }
        else
        {
            promptObject.SetActive(false);
        }
    }
}