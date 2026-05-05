using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    public AudioClip engineClip;
    [Range(0f, 1f)] public float idleVolume = 0.3f;
    [Range(0f, 1f)] public float drivingVolume = 0.7f;
    public float idlePitch = 0.8f;
    public float drivingPitch = 1.3f;

    [Header("References")]
    public VehicleInputHandler vehicleInput;

    private AudioSource _source;
    private bool _engineOn = false;

    private void Awake()
    {
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = engineClip;
        _source.loop = true;
        _source.spatialBlend = 1f;
        _source.playOnAwake = false;
    }

    public void StartEngine()
    {
        if (!_engineOn)
        {
            _engineOn = true;
            _source.Play();
        }
    }

    public void StopEngine()
    {
        if (_engineOn)
        {
            _engineOn = false;
            _source.Stop();
        }
    }

    private void Update()
    {
        if (!_engineOn || vehicleInput == null) return;

        // Detect motion via accelerator or steering
        bool isMoving = Mathf.Abs(vehicleInput.AccelerateInput) > 0.01f
                     || Mathf.Abs(vehicleInput.SteerInput) > 0.01f;

        _source.volume = isMoving ? drivingVolume : idleVolume;
        _source.pitch = isMoving ? drivingPitch : idlePitch;
    }
}