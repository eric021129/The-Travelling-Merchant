using UnityEngine;

public class TruckInteractable : MonoBehaviour, IInteractable
{
    public string GetPrompt() => "Press R to open Truck";
    public KeyType GetKey() => KeyType.R;
    public void Interact() => UIManager.Instance.OpenTruck();
}