using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float turnSpeed = 100f;

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    }
}