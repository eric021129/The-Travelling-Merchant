using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;    // drag the camera here
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private LayerMask interactableLayer = ~0;

    private IInteractable _currentTarget;

    public IInteractable CurrentTarget => _currentTarget;

    private void Update()
    {
        DetectInteractable();
        HandleInput();
    }

    private void DetectInteractable()
    {
        _currentTarget = null;

        if (rayOrigin == null) return;

        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                _currentTarget = interactable;
            }
        }
    }
    private void HandleInput()
    {
        if (_currentTarget == null) return;

        KeyType required = _currentTarget.GetKey();

        bool pressed = required switch
        {
            KeyType.E => Keyboard.current.eKey.wasPressedThisFrame,
            KeyType.R => Keyboard.current.rKey.wasPressedThisFrame,
            _ => false
        };

        if (pressed)
        {
            _currentTarget.Interact();
        }
    }
}