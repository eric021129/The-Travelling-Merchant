using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class FirstPersonController : MonoBehaviour
{
    public Animator animator;
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;


    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravityMultiplier = 1.0f;


    [Header("Look Parameters")]
    [SerializeField] private float mouseSensitivity = 0.1f;


    [Header ("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private PlayerInputHandler playerInputHandler;


    private Vector3 currentMovement;
    private float CurrentSpeed => walkSpeed * (playerInputHandler.SprintTriggered ? sprintMultiplier : 1);
    private CinemachinePanTilt panTilt;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        panTilt = virtualCamera.GetComponent<CinemachinePanTilt>();

    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();

        Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);
        animator.SetFloat("Speed", horizontalVelocity.magnitude);
    }


    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0f, playerInputHandler.MovementInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }


    private void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;


            if (playerInputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }
    }


    private void HandleMovement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * CurrentSpeed;
        currentMovement.z = worldDirection.z * CurrentSpeed;


        HandleJumping();
        characterController.Move(currentMovement * Time.deltaTime);
    }


    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }

    private void HandleRotation()
    {
        float mouseXRotation = playerInputHandler.RotationInput.x * mouseSensitivity;
        float mouseYRotation = playerInputHandler.RotationInput.y * mouseSensitivity;

        // rotate the player body left/right
        transform.Rotate(0, mouseXRotation, 0);

        // let Cinemachine handle the camera tilt up/down
        panTilt.TiltAxis.Value -= mouseYRotation;
    }
}
