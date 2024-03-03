using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    // Outlets
    public GameObject pauseMenu;
    void Awake()
    {
        instance = this;
        hide();
    }

    public void show() {
        ShowPauseMenu();
        gameObject.SetActive(true);

        Time.timeScale = 0f;
        BowlController.instance.isPaused = true;
    }

    public void hide() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        // this function also runs on Awake at which point Bowl Controller
        // may not have been initialized yet.
        if (BowlController.instance != null) {
            BowlController.instance.isPaused = false;
        }
        
    }

    public void ShowPauseMenu () {
        SwitchMenu(pauseMenu);
    }

    void SwitchMenu (GameObject menu) {
        pauseMenu.SetActive(false);
        menu.SetActive(true);
    }

    public void returnHome() {
        SceneManager.LoadScene("Home");
    }
}
