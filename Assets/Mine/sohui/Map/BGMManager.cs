using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgmSource;

    void Start()
    {
        bgmSource.volume = 0.1f; // 배경음악은 너무 크지 않게 0.3 정도로 조절
    }
}