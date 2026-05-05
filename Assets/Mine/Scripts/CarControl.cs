using UnityEngine;

public class CarControl : MonoBehaviour
{
    [Header("Car Properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    [Header("References")]
    [SerializeField] private VehicleInputHandler vehicleInputHandler;

    private WheelControl[] wheels;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.5f, 0);

        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;

        wheels = GetComponentsInChildren<WheelControl>();
    }

    void FixedUpdate()
    {
        // read input from VehicleInputHandler instead of directly from input system
        float vInput = vehicleInputHandler.AccelerateInput;
        float hInput = vehicleInputHandler.SteerInput;

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            // No input → apply parking brake
            if (Mathf.Abs(vInput) < 0.01f)
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = brakeTorque;
            }
            else if (isAccelerating)
            {
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0f;
            }
            else
            {
                // Reverse direction (e.g., pressing W while moving backward) → brake
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
            }
        }
    }
}