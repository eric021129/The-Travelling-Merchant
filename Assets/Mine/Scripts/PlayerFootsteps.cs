using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip walkClip;
    [Range(0f, 1f)] public float volume = 0.5f;
    public float walkPitch = 1f;
    public float runPitch = 1.5f;

    public CharacterController characterController;
    public PlayerInputHandler playerInputHandler;

    private AudioSource _source;

    private void Awake()
    {
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = walkClip;
        _source.loop = true;
        _source.volume = volume;
        _source.playOnAwake = false;
    }

    private void Update()
    {
        if (characterController == null || playerInputHandler == null) return;

        bool isMoving = characterController.isGrounded
                     && playerInputHandler.MovementInput.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            // Adjust pitch based on sprinting
            _source.pitch = playerInputHandler.SprintTriggered ? runPitch : walkPitch;

            if (!_source.isPlaying)
                _source.Play();
        }
        else
        {
            if (_source.isPlaying)
                _source.Stop();
        }
    }
}