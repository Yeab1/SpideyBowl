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
    
    void Start() {
        instance = this;
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
        
        StartLevel(GameDataController.get_max_unlocked_level());
    }

    public static void StartLevel (int level) {
        if (!BowlController.is_debug_mode) {
            if (GameDataController.is_level_locked(level)) {
                return;
            }
        }
    
        SoundEffectsManager.instance.PlayButtonClickSound();
        GameDataController.setLevel(level);

        if (!TutorialUtils.level_needs_tutorial(
            LevelsList.get_level_name_from_index(
                GameDataController.getLevel()))) {
                    SceneManager.LoadScene(LevelsList.get_level_name_from_index(level));
                }
        else {
            SceneManager.LoadScene("Tutorial");
        } 
    }

    public void next_section() {
        if (LevelSelect.instance.current_section >= Mathf.Floor(GameDataController.getLastLevel() / 15)) return;
        LevelSelect.instance.destroy_all_level_prefabs();
        LevelSelect.instance.current_section += 1;
        LevelSelect.instance.setup_level_select_grid();
    }

    public void previous_section() {
        if (LevelSelect.instance.current_section <= 0) return;
        LevelSelect.instance.destroy_all_level_prefabs();
        LevelSelect.instance.current_section -= 1;
        LevelSelect.instance.setup_level_select_grid();
    }

    // TODO: Delete for release
    // for debugging purposes only. Don't forget to remove 
    // the clear progress button in settings
    public void clearAllProgress() {
        GameDataController.clear_all_progress();
    }
}
