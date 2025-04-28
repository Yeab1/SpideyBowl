using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSettingsData audioSettingsData; // data type to save audio settings

    public static void save_sfx_volume(float sfx_volume) {
        audioSettingsData.set_sfx_volume(sfx_volume);
        ProgressDataManager.SaveAudioSettings(audioSettingsData);
    }

    public static void save_bg_volume(float bg_volume) {
        audioSettingsData.set_bg_volume(bg_volume);
        ProgressDataManager.SaveAudioSettings(audioSettingsData);
    }

    public static void save_volume(float sfx_volume, float bg_volume) {
        audioSettingsData.set_volume(sfx_volume, bg_volume);
        ProgressDataManager.SaveAudioSettings(audioSettingsData);
    }

    public static void initialize_volume(float sfx_volume, float bg_volume) {
        audioSettingsData = new AudioSettingsData();
        SoundEffectsManager.instance.initialize_volume(sfx_volume);
        DontDestroyAudio.instance.initialize_volume(bg_volume);
        audioSettingsData.set_volume(sfx_volume, bg_volume);
    }
}
