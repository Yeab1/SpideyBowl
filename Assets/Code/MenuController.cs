using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    // Outlets
    public GameObject pauseMenu;
    public GameObject Mute;
    public GameObject Unmute;
    void Awake()
    {
        instance = this;
        hide();
    }

    void Start() 
    {
        if (SoundEffectsManager.instance.getVolume() == 0 &&
            DontDestroyAudio.instance.getVolume() == 0) {
            Unmute.SetActive(true);
            Mute.SetActive(false);
        } else {
            Unmute.SetActive(false);
            Mute.SetActive(true);
        }
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
        SoundEffectsManager.instance.PlayButtonClickSound();
        pauseMenu.SetActive(false);
        menu.SetActive(true);
    }

    public void returnHome() {
        // Time needs to resume so that coroutines following the home
        // scene can have proper timing. Ex: TutorialController button.
        Time.timeScale = 1.0f;
        SoundEffectsManager.instance.PlayButtonClickSound();
        SceneManager.LoadScene("Home");
    }

    public void mute() {        
        // Mute the volume
        DontDestroyAudio.instance.updateMusicVolume(0f);
        SoundEffectsManager.instance.updateSFXVolume(0f);
        
        // Swap the mute asset to an unmute button.
        Mute.SetActive(false);
        Unmute.SetActive(true);
    }

    public void unMute() {
        // Retrieve the previous volume.
        GameDataController.reset_audio_settings();
        SoundEffectsManager.instance.updateSFXVolume(
            GameDataController.get_audio_settings().get_previous_sfx_volume());
        DontDestroyAudio.instance.updateMusicVolume(
            GameDataController.get_audio_settings().get_previous_bg_volume());

        // Swap the unmute asset to a mute button.
        Mute.SetActive(true);
        Unmute.SetActive(false);
    }
}
