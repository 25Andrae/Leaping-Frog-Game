using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("------------Audio Clip------------")]
    public AudioClip game_background;
    public AudioClip main_menu_background;
    public AudioClip jump;
    public AudioClip crouch;
    public AudioClip bonus;
    public AudioClip invincible;
    public AudioClip death;
    public AudioClip play_retry;
    public AudioClip buttons;

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Main Menu")
        {
            musicSource.clip = main_menu_background;
        }
        else if (currentScene == "Start Game")
        {
            musicSource.clip = game_background;
        }
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void StopSFX()
    {
        SFXSource.Stop();
    }
}
