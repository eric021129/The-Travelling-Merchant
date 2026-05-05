using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환 기능을 위해 필수!

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // "GameScene" 자리에 실제 게임 플레이 화면(씬)의 이름을 정확히 적어주세요!
        SceneManager.LoadScene("Test");
    }
}