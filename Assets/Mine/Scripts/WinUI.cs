using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WinUI : MonoBehaviour
{
    public static WinUI Instance;

    public GameObject panel;
    public TextMeshProUGUI statsText;
    public Button restartButton;
    public Button quitButton;

    private void Awake()
    {
        Instance = this;
        if (panel != null) panel.SetActive(false);

        restartButton.onClick.AddListener(OnRestart);
        quitButton.onClick.AddListener(OnQuit);
    }

    public void ShowWin()
    {
        panel.SetActive(true);

        // Show some stats
        statsText.text = $"Final money: ₩ {PlayerState.Instance.Money:N0}\n" +
                         $"Tuition paid: ₩ {PlayerState.Instance.TuitionPaid:N0}";

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause time so player can't keep playing
        Time.timeScale = 0f;
    }

    private void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnQuit()
    {
        Time.timeScale = 1f;

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}