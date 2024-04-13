using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// a class that represents a single progress.
[System.Serializable]
public class ProgressData
{
    public int[] collected_stars_per_level;
    public int max_level;

    public ProgressData(int[] collected_stars_per_level, int max_level) {
        this.collected_stars_per_level = collected_stars_per_level; 
        this.max_level = max_level;
    }

    public ProgressData() {
        this.collected_stars_per_level = new int[GameDataController.getLastLevel()];
        this.max_level = 1;
    }

    public int[] get_collected_stars_per_level() {
        return collected_stars_per_level;
    }

    public int get_max_level() {
        return max_level;
    }
}

// a class that represents a single progress.
[System.Serializable]
public class AudioSettingsData 
{
    public float sfx_volume;
    public float bg_volume;
    public AudioSettingsData(float sfx_volume,
                        float bg_volume) {
        this.sfx_volume = sfx_volume;
        this.bg_volume = bg_volume;
    }

    public AudioSettingsData() {
        this.sfx_volume = 1.0f;
        this.bg_volume = 1.0f;
    }

    public void set_sfx_volume(float sfx_volume) {
        this.sfx_volume = sfx_volume;
    }

    public void set_bg_volume(float bg_volume) {
        this.bg_volume = bg_volume;
    }

    public void set_volume(float sfx_volume, float bg_volume) {
        this.sfx_volume = sfx_volume;
        this.bg_volume = bg_volume;
    }

    public float get_sfx_volume() {
        return sfx_volume;
    }

    public float get_bg_volume() {
        return bg_volume;
    }
}

public class ProgressDataManager : MonoBehaviour
{
    private static string progressFilePath = 
        Path.Combine(Application.persistentDataPath, "ProgressData.json");
    private static string AudioSettingsFilePath =
        Path.Combine(Application.persistentDataPath, "AudioSettingsData.json");

    // saves progress as a json file
    public static void SaveProgress(ProgressData data) {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(progressFilePath, jsonData);
    }

    public static void SaveAudioSettings(AudioSettingsData data) {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(AudioSettingsFilePath, jsonData);
    }

    public static ProgressData LoadProgress() {
        if (File.Exists(progressFilePath))
        {
            string jsonData = File.ReadAllText(progressFilePath);
            return JsonUtility.FromJson<ProgressData>(jsonData);
        }
        else
        {
            return null;
        }
    }

    public static AudioSettingsData LoadAudioSettings() {
        if (File.Exists(AudioSettingsFilePath))
        {
            string jsonData = File.ReadAllText(AudioSettingsFilePath);
            return JsonUtility.FromJson<AudioSettingsData>(jsonData);
        }
        else
        {
            return null;
        }
    }
}
