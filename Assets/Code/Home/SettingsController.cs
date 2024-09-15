using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsController : MonoBehaviour
{
    public Slider soundEffectsVolume;
    public Slider musicVolume;
    public static SettingsController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        musicVolume.value = DontDestroyAudio.instance.getVolume();
        soundEffectsVolume.value = SoundManager.instance.getVolume();
        soundEffectsVolume.onValueChanged.AddListener(delegate { OnSFXVolumeChanged(); });
        musicVolume.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });
    }

    void OnSFXVolumeChanged() {
        SoundManager.instance.updateSFXVolume(soundEffectsVolume.value);
    }
    void OnMusicVolumeChanged() {
        DontDestroyAudio.instance.updateMusicVolume(musicVolume.value);
    }
}
