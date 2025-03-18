using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class volumeMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer gameMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume")|| PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else { 
            SetMusicVolume();
            SetSFXVolume();
        }
    }
    public void SetMusicVolume() {
        float volume = musicSlider.value;
        gameMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        gameMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume() {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
}