using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    public GameObject storehousePanel;
    public GameObject truckPanel;
    public GameObject wifePanel;
    public GameObject tradingPanel;

    [Header("Player Control")]
    public FirstPersonController playerController;

    private bool _isAnyUIOpen = false;

    public bool IsAnyUIOpen => _isAnyUIOpen;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (storehousePanel != null) storehousePanel.SetActive(false);
        if (truckPanel != null) truckPanel.SetActive(false);
        if (wifePanel != null) wifePanel.SetActive(false);
        if (tradingPanel != null) tradingPanel.SetActive(false);
    }

    private void Update()
    {
        if (_isAnyUIOpen && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseAll();
        }
    }

    public void OpenStorehouse()
    {
        CloseAll();
        storehousePanel.SetActive(true);
        SetUIMode(true);
    }

    public void OpenTruck()
    {
        CloseAll();
        truckPanel.SetActive(true);
        SetUIMode(true);
    }

    public void OpenWife()
    {
        CloseAll();
        wifePanel.SetActive(true);
        SetUIMode(true);
    }

    public void OpenTrading()
    {
        CloseAll();
        tradingPanel.SetActive(true);
        SetUIMode(true);
    }

    public void CloseAll()
    {
        if (storehousePanel != null) storehousePanel.SetActive(false);
        if (truckPanel != null) truckPanel.SetActive(false);
        if (wifePanel != null) wifePanel.SetActive(false);
        if (tradingPanel != null) tradingPanel.SetActive(false);
        SetUIMode(false);
    }

    private void SetUIMode(bool uiOpen)
    {
        _isAnyUIOpen = uiOpen;

        Cursor.lockState = uiOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = uiOpen;

        if (playerController != null)
            playerController.enabled = !uiOpen;
    }
}