using UnityEngine;

public class WifeInteractable : MonoBehaviour, IInteractable
{
    public string GetPrompt() => "Press E to talk to Wife";
    public KeyType GetKey() => KeyType.E;
    public void Interact() => UIManager.Instance.OpenWife();
}