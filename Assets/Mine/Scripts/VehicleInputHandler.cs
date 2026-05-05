using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name")]
    [SerializeField] private string actionMapName = "Car";

    [Header("Action Names")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string interaction = "Interact";

    private InputAction movementAction;
    private InputAction interactionAction;

    public float AccelerateInput { get; private set; }
    public float SteerInput { get; private set; }
    public bool InteractionTriggered { get; private set; }

    [SerializeField] private CarInteraction carInteraction;

    private void Awake()
    {
        if (playerControls == null)
        {
            Debug.LogError("playerControls is not assigned in the Inspector on " + gameObject.name);
            return;
        }

        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);

        if (mapReference == null)
        {
            Debug.LogError("ACTION MAP '" + actionMapName + "' NOT FOUND!");
            return;
        }

        movementAction = mapReference.FindAction(movement);
        interactionAction = mapReference.FindAction(interaction);

        Debug.Log("Vehicle Movement: " + movementAction);
        Debug.Log("Vehicle Interaction: " + interactionAction);

        if (movementAction == null)
        {
            Debug.LogError("Action '" + movement + "' not found in map '" + actionMapName + "'");
        }

        if (interactionAction == null)
        {
            Debug.LogError("Action '" + interaction + "' not found in map '" + actionMapName + "'");
        }

        SubscribeActionValuesToInputEvents();
    }

    private void SubscribeActionValuesToInputEvents()
    {
        movementAction.performed += inputInfo =>
        {
            Vector2 input = inputInfo.ReadValue<Vector2>();
            SteerInput = input.x;
            AccelerateInput = input.y;
        };
        movementAction.canceled += inputInfo =>
        {
            SteerInput = 0f;
            AccelerateInput = 0f;
        };

        interactionAction.performed += inputInfo => InteractionTriggered = true;
        interactionAction.canceled += inputInfo => InteractionTriggered = false;
    }

    private void Update()
    {
        if (InteractionTriggered)
        {
            carInteraction.ExitCar();
        }
    }
}