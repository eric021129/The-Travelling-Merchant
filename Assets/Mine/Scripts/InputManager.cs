using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string playerActionMap = "Player";
    [SerializeField] private string vehicleActionMap = "Car";

    private void Start()
    {
        if (inputActions == null)
        {
            Debug.LogError("INPUT ACTIONS NOT ASSIGNED on InputManager! Drag your InputActionAsset here.");
            return;
        }

        SwitchToPlayer();
    }

    public void SwitchToPlayer()
    {
        if (inputActions == null)
        {
            Debug.LogError("inputActions is NULL on InputManager!");
            return;
        }

        inputActions.FindActionMap(vehicleActionMap).Disable();
        inputActions.FindActionMap(playerActionMap).Enable();
        Debug.Log("Switched to Player controls");
    }

    public void SwitchToVehicle()
    {
        if (inputActions == null)
        {
            Debug.LogError("inputActions is NULL on InputManager!");
            return;
        }

        inputActions.FindActionMap(playerActionMap).Disable();
        inputActions.FindActionMap(vehicleActionMap).Enable();
        Debug.Log("Switched to Vehicle controls");
    }
}