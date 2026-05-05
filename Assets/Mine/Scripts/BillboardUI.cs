using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.forward = _mainCam.transform.forward;
    }
}