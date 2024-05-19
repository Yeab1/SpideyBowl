using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenuController : MonoBehaviour
{
    public static HomeMenuController instance;

    // Outlets
    public GameObject HomeMenu;
    public GameObject LevelsMenu;
    public GameObject SettingsMenu;
    public static ProgressData progress;
    void Awake()
    {
        instance = this;
        // initialize the player's star collection progress
        progress = LevelController.load_progress();

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
    void Start() {
        show();
    }

    public void show() {
        ShowHomeMenu();
        gameObject.SetActive(true);
    }

    public void ShowHomeMenu () {
        SwitchMenu(HomeMenu);
    }
    public void ShowLevelsMenu () {
        SwitchMenu(LevelsMenu);
    }
    public void ShowSettingssMenu () {
        SwitchMenu(SettingsMenu);
    }

    void SwitchMenu (GameObject menu) {
        SoundEffectsManager.instance.PlayButtonClickSound();
        HomeMenu.SetActive(false);
        LevelsMenu.SetActive(false);
        SettingsMenu.SetActive(false);

        menu.SetActive(true);
    }

    public void StartGame() {
        SoundEffectsManager.instance.PlayButtonClickSound();
        if (progress == null) {
            // if there is no progress, start from level 1
            GameDataController.setLevel(1);
        } else {
            GameDataController.setLevel(progress.get_max_level());
        }
        SceneManager.LoadScene(LevelsList.get_level_name_from_index(GameDataController.getLevel()));
    }

    public static void StartLevel (int level) {
        if (!BowlController.is_debug_mode) {
            if (progress == null && level != 1 || progress.get_max_level() < level) {
                Debug.Log("Locked");
                return;
            }
        }
    
        SoundEffectsManager.instance.PlayButtonClickSound();
        GameDataController.setLevel(level);
        Debug.Log("Loading: " + level);
        SceneManager.LoadScene(LevelsList.get_level_name_from_index(level));
    }

    // TODO: Delete for release
    // for debugging purposes only. Don't forget to remove 
    // the clear progress button in settings
    public void clearAllProgress() {
        LevelController.clear_all_progress();
    }
}
