using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// a class that represents a single progress.
[System.Serializable]
public class ProgressData
{
    public int[] collected_stars_per_level;
    public int max_unlocked_level;

    public ProgressData(int[] collected_stars_per_level, int max_unlocked_level) {
        this.collected_stars_per_level = collected_stars_per_level; 
        this.max_unlocked_level = max_unlocked_level;
    }

    public ProgressData() {
        this.collected_stars_per_level = new int[GameDataController.getLastLevel()];
        this.max_unlocked_level = 1;
    }

    public void set_collected_stars_per_level(int[] collected_stars_per_level) {
        this.collected_stars_per_level = collected_stars_per_level;
    }

    public void set_collected_stars_for_level(int level, int stars) {
        if (collected_stars_per_level[level - 1] < stars) {
            collected_stars_per_level[level - 1] = stars;
        }
    }

    public void set_max_unlocked_level(int max_unlocked_level) {
        this.max_unlocked_level = max_unlocked_level;
    }

    public int[] get_collected_stars_per_level() {
        return collected_stars_per_level;
    }

    public int get_max_unlocked_level() {
        return max_unlocked_level;
    }

    public int get_collected_stars(int level) {
        return collected_stars_per_level[level - 1];
    }

    // returns the total number of stars collected through all levels
    public int get_total_stars() {
        int sum = 0;
        for (int i = 0; i < collected_stars_per_level.Length; i++) {
            sum += collected_stars_per_level[i];
        }
        return sum;
    }

    public void print_as_string() {
        Debug.Log("max_unlocked_level: " + max_unlocked_level);
        for (int i = 0; i < collected_stars_per_level.Length; i++) {
            Debug.Log((i+1) + " : " + collected_stars_per_level[i]);
        }
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
            Debug.Log("Progress Found");
            string jsonData = File.ReadAllText(progressFilePath);
            return JsonUtility.FromJson<ProgressData>(jsonData);
        }
        else
        {
            Debug.Log("No Progress Found");
            return new ProgressData();
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
