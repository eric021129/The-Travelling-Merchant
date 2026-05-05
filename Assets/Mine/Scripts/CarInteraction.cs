using UnityEngine;

public class CarInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Transform exitPosition;
    [SerializeField] private CarEngineSound carEngineSound;   // ← NEW

    private GameObject player;
    private bool playerNearDoor = false;
    private bool playerInCar = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerNearDoor = true;
            Debug.Log("Player is near the door");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearDoor = false;
            Debug.Log("Walked away from the car");
        }
    }

    private void Update()
    {
        if (playerNearDoor)
        {
            Debug.Log("Near door. InteractionTriggered: " + playerInputHandler.InteractionTriggered);
        }

        if (playerNearDoor && !playerInCar && playerInputHandler.InteractionTriggered)
        {
            EnterCar();
        }
    }

    private void EnterCar()
    {
        playerInCar = true;
        playerNearDoor = false;

        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.SetActive(false);
        inputManager.SwitchToVehicle();

        // Start engine sound
        if (carEngineSound != null) carEngineSound.StartEngine();   // ← NEW

        Debug.Log("Entered the car");
    }

    public void ExitCar()
    {
        playerInCar = false;

        player.SetActive(true);
        player.transform.position = exitPosition.position;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<FirstPersonController>().enabled = true;
        inputManager.SwitchToPlayer();

        // Stop engine sound
        if (carEngineSound != null) carEngineSound.StopEngine();   // ← NEW

        Debug.Log("Exited the car");
    }
}