using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public Transform wheelModel;
    public Vector3 wheelModelRotationOffset = new Vector3(0, 90, 0);

    [HideInInspector] public WheelCollider WheelCollider;

    public bool steerable;
    public bool motorized;

    [Header("Friction Settings")]
    public float forwardStiffness = 1.5f;
    public float sidewaysStiffness = 1.5f;

    Vector3 position;
    Quaternion rotation;

    private void Start()
    {
        WheelCollider = GetComponent<WheelCollider>();
        SetFriction();
    }

    void SetFriction()
    {
        WheelFrictionCurve forward = WheelCollider.forwardFriction;
        forward.stiffness = forwardStiffness;
        WheelCollider.forwardFriction = forward;

        WheelFrictionCurve sideways = WheelCollider.sidewaysFriction;
        sideways.stiffness = sidewaysStiffness;
        WheelCollider.sidewaysFriction = sideways;
    }

    void Update()
    {
        WheelCollider.GetWorldPose(out position, out rotation);
        wheelModel.transform.position = position;
        wheelModel.transform.rotation = rotation * Quaternion.Euler(wheelModelRotationOffset);
    }
}