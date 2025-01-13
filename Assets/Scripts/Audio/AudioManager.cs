using UnityEngine;
public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip fireBullet;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip playerHurt;
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
    public void PlayFireSound()
    {
        sfxSource.clip = fireBullet;
        sfxSource.Play();
    }
    public void PlayPlayerHurtSound()
    {
        sfxSource.clip = playerHurt;
        sfxSource.Play();
    }
}
