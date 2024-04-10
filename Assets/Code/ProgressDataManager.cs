using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// a class that represents a single progress.
[System.Serializable]
public class ProgressData
{
    public int [] collected_stars_per_level;
    public int current_level;
    public ProgressData(int[] collected_stars_per_level, int current_level) {
        this.collected_stars_per_level = collected_stars_per_level; 
        this.current_level = current_level;
    }

    public ProgressData() {
        this.collected_stars_per_level = new int[GameDataController.getLastLevel()];
        this.current_level = 1;
    }

    public int[] get_collected_stars_per_level() {
        return collected_stars_per_level;
    }

    public int get_current_level() {
        return current_level;
    }
}

public class ProgressDataManager : MonoBehaviour
{
    private static string dataFilePath = 
        Path.Combine(Application.persistentDataPath, "ProgressData.json");

    // saves progress as a json file
    public static void SaveProgress(ProgressData data) {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(dataFilePath, jsonData);
    }

    public static ProgressData LoadProgress() {
        if (File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);
            return JsonUtility.FromJson<ProgressData>(jsonData);
        }
        else
        {
            return null;
        }
    }
}
