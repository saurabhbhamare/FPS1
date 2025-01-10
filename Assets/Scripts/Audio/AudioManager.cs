using UnityEngine;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    public static AudioManager Instance { get; private set; }
    public AudioClip fireBullet;
    public AudioClip backgroundMusic;
    public AudioClip playerHurt;
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
