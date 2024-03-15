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
    void Awake()
    {
        instance = this;
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

    void SwitchMenu (GameObject menu) {
        SoundManager.instance.PlayButtonClickSound();
        HomeMenu.SetActive(false);
        LevelsMenu.SetActive(false);

        menu.SetActive(true);
    }

    public void StartGame() {
        SoundManager.instance.PlayButtonClickSound();
        SceneManager.LoadScene("Level-" + GameDataController.getLevel());
    }

    public void StartLevel (int level) {
        SoundManager.instance.PlayButtonClickSound();
        SceneManager.LoadScene("Level-" + level);
        GameDataController.setLevel(level);
    }
}
