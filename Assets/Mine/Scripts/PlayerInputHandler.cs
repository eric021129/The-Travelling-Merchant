using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;


    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";


    [Header("Action Name References")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string rotation = "Rotation";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string interaction = "Interact";




    private InputAction movementAction;
    private InputAction rotationAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction interactionAction;



    public Vector2 MovementInput {  get; private set; }
    public Vector2 RotationInput {  get; private set; }
    public bool JumpTriggered {  get; private set; }
    public bool SprintTriggered {  get; private set; }
    public bool InteractionTriggered { get; private set; }



private void Awake()
{
    InputActionMap mapReference = playerControls.FindActionMap(actionMapName);

    if (mapReference == null)
    {
        Debug.LogError("ACTION MAP '" + actionMapName + "' NOT FOUND!");
        return;
    }

    movementAction = mapReference.FindAction(movement);
    rotationAction = mapReference.FindAction(rotation);
    jumpAction = mapReference.FindAction(jump);
    sprintAction = mapReference.FindAction(sprint);
    interactionAction = mapReference.FindAction(interaction);

    Debug.Log("Movement: " + movementAction);
    Debug.Log("Rotation: " + rotationAction);
    Debug.Log("Jump: " + jumpAction);
    Debug.Log("Sprint: " + sprintAction);
    Debug.Log("Interaction: " + interactionAction);

    SubscribeActionValuesToInputEvents();
}


    private void SubscribeActionValuesToInputEvents()
    {
        movementAction.performed += inputInfo => MovementInput = inputInfo.ReadValue<Vector2>();
        movementAction.canceled += inputInfo => MovementInput = Vector2.zero;


        rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
        rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;


        jumpAction.performed += inputInfo => JumpTriggered = true;
        jumpAction.canceled += inputInfo => JumpTriggered = false;


        sprintAction.performed += inputInfo => SprintTriggered = true;
        sprintAction.canceled += inputInfo => SprintTriggered = false;


        interactionAction.performed += inputInfo => InteractionTriggered = true;
        interactionAction.canceled += inputInfo => InteractionTriggered = false;
    }
}