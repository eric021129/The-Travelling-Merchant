using UnityEngine;

public class StorehouseInteractable : MonoBehaviour, IInteractable
{
    public string GetPrompt() => "Press E to open Storehouse";
    public KeyType GetKey() => KeyType.E;
    public void Interact() => UIManager.Instance.OpenStorehouse();
}