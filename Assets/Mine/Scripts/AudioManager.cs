using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioClip startMenuMusic;
    public AudioClip ingameMusic;

    [Header("SFX")]
    public AudioClip transactionSuccess;
    public AudioClip transactionFail;

    [Header("Volume")]
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float ingameMusicVolume = 0.8f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    private void Awake()
    {
        Instance = this;

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;
        _musicSource.playOnAwake = false;

        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.loop = false;
        _sfxSource.playOnAwake = false;
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "StartMenu")
            PlayStartMenuMusic();
        else
            PlayIngameMusic();
    }

    public void PlayStartMenuMusic()
    {
        _musicSource.clip = startMenuMusic;
        _musicSource.volume = musicVolume;
        _musicSource.Play();
    }

    public void PlayIngameMusic()
    {
        _musicSource.clip = ingameMusic;
        _musicSource.volume = ingameMusicVolume;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PlayTransactionSuccess()
    {
        _sfxSource.PlayOneShot(transactionSuccess, sfxVolume);
    }

    public void PlayTransactionFail()
    {
        _sfxSource.PlayOneShot(transactionFail, sfxVolume);
    }
}