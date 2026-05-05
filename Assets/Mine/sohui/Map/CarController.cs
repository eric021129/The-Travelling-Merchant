using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public AudioSource engineAudio;
    public float fadeSpeed = 2.0f;

    void Update()
    {
        // 1. 기본적으로 직진(위/아래) 키를 누르고 있는가?
        bool isMoving = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);
        // 2. 좌우 조향 키를 누르고 있는가?
        bool isSteering = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

        if (isMoving)
        {
            if (!engineAudio.isPlaying) engineAudio.Play();

            // 조향 중이면 볼륨을 0.2(작게), 아니면 0.5(원래대로)로 설정
            float targetVolume = isSteering ? 0.2f : 0.5f;

            engineAudio.volume = Mathf.MoveTowards(engineAudio.volume, targetVolume, fadeSpeed * Time.deltaTime);
        }
        else
        {
            // 움직이지 않으면 볼륨을 0으로
            engineAudio.volume = Mathf.MoveTowards(engineAudio.volume, 0.0f, fadeSpeed * Time.deltaTime);
            if (engineAudio.volume <= 0) engineAudio.Stop();
        }
    }
}