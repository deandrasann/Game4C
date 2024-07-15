using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] AudioSource[] musicAudioSources;
    [SerializeField] AudioSource[] sfxAudioSources;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
        }

        Load();
    }

    public void ChangeMusicVolume()
    {
        foreach (AudioSource audioSource in musicAudioSources)
        {
            audioSource.volume = musicVolumeSlider.value;
        }
        Save();
    }

    public void ChangeSFXVolume()
    {
        foreach (AudioSource audioSource in sfxAudioSources)
        {
            audioSource.volume = sfxVolumeSlider.value;
        }
        Save();
    }

    private void Load()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        ChangeMusicVolume();
        ChangeSFXVolume();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }


}
