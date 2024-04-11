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
    void Awake()
    {
        instance = this;
        // initialize the player's star collection progress
        CoinsPerLevel.load_star_progress();
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
        SoundManager.instance.PlayButtonClickSound();
        HomeMenu.SetActive(false);
        LevelsMenu.SetActive(false);
        SettingsMenu.SetActive(false);

        menu.SetActive(true);
    }

    public void StartGame() {
        SoundManager.instance.PlayButtonClickSound();

        // start from where player left off
        ProgressData progress = ProgressDataManager.LoadProgress();
        if (progress == null) {
            // if there is no progress, start from level 1
            GameDataController.setLevel(1);
        } else {
            GameDataController.setLevel(progress.get_current_level());
        }

        SceneManager.LoadScene("Level-" + GameDataController.getLevel());
    }

    public static void StartLevel (int level) {
        SoundManager.instance.PlayButtonClickSound();
        GameDataController.setLevel(level);
        SceneManager.LoadScene("Level-" + level);
    }

    // TODO: Delete for release
    // for debugging purposes only. Don't forget to remove 
    // the clear progress button in settings
    public void clearAllProgress() {
        CoinsPerLevel.clear_all_progress();
    }
}
