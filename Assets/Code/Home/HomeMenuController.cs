using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenuController : MonoBehaviour
{
    public static HomeMenuController instance;

    // Outlets
    public GameObject HomeMenu;
    public GameObject SettingsMenu;
    public GameObject LevelsMenu;
    void Awake()
    {
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

    public void ShowSettingsMenu () {
        SwitchMenu(SettingsMenu);
    }

    void SwitchMenu (GameObject menu) {
        HomeMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        LevelsMenu.SetActive(false);

        menu.SetActive(true);
    }

    public void StartGame() {
        SceneManager.LoadScene("Level-1");
    }

    public void StartLevel (int level) {
        SceneManager.LoadScene("Level-" + level);
        GameDataController.setLevel(level);
    }
}
