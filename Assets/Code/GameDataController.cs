using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
    public static GameDataController instance;
    static int currentLevel = 0;
    static int lastLevel = LevelsList.get_number_of_levels();
    static int current_level_coins = 0;

    static ProgressData progress;

    public static void setLevel(int level) {
        currentLevel = level;
    }

    public static int getLevel() {
        return currentLevel;
    }

    public static void incrementLevel() {
        currentLevel++;
    }

    public static int getLastLevel() {
        return lastLevel;
    }

    public static void updateCurrentLevelCoins(int coins) {
        current_level_coins = coins;
    }

    public static int getCurrentLevelCoins() {
        return current_level_coins;
    }

    public static void set_progress(ProgressData new_progress) {
        progress = new_progress;
    }

    public static void update_starts_for_level(int level, int stars) {
        progress.set_collected_stars_for_level(level, stars);
    }

    public static int get_collected_stars(int level) {
        return progress.get_collected_stars(level);
    }

    public static int[] get_collected_stars_per_level() {
        return progress.get_collected_stars_per_level();
    }

    public static void set_max_unlocked_level(int max_unlocked_level) {
        progress.set_max_unlocked_level(max_unlocked_level);
    }

    public static int get_max_unlocked_level() {
        return progress.get_max_unlocked_level();
    }

    public static ProgressData get_progress() {
        return progress;
    }

    // returns the total number of stars collected through all levels
    public static int get_total_stars() {
        return progress.get_total_stars();
    }

    public static bool is_level_locked(int level) {
        return progress == null && level != 1 || progress.get_max_unlocked_level() < level;
    }

    public static void initialize_progress() {
        if (progress != null) {
            return; // progress data is already initialized
        }
        Debug.Log("Initializing Progress");
        // initialize the player's star collection progress
        progress = ProgressDataManager.LoadProgress();
        if (progress == null || progress.get_max_unlocked_level() == 0) {
            // if there is no progress, start from level 1
            Debug.Log("defaulting max level to 0");
            GameDataController.setLevel(1);
        } else {
            Debug.Log("Max unlocked level found: " + progress.get_max_unlocked_level());
            GameDataController.setLevel(progress.get_max_unlocked_level());
        }

        // initialize settings
        AudioSettingsData audio_settings = ProgressDataManager.LoadAudioSettings();
        if (audio_settings != null) {
            SoundManager.initialize_volume(
                        audio_settings.get_sfx_volume(), 
                        audio_settings.get_bg_volume());
        } else {
            SoundManager.initialize_volume(0.2f, 0.2f);
        }
    }

    public static void save_current_progress() {
        ProgressDataManager.SaveProgress(progress);
    }

    public static void clear_all_progress() {
        ProgressData progress = new ProgressData();
        ProgressDataManager.SaveProgress(progress);
        ProgressDataManager.LoadProgress();

        // reset UI on level select window after clearing process
        LevelSelect.instance.destroy_all_level_prefabs();
        LevelSelect.instance.setup_level_select_grid();
    }
}