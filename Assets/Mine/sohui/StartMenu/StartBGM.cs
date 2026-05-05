using UnityEngine;

public class StartBGM : MonoBehaviour
{
    public AudioSource bgmSource;

    void Start()
    {
        bgmSource.volume = 0.3f;
    }
}
