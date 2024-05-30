using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public static WinController instance;

    // Outlets
    public GameObject nextLevelBtn;
    public TMP_Text currentLevelCoinsUI;
    public TMP_Text stars_collected_UI;
    public TMP_Text total_stars_collected_UI;
    public TMP_Text tutorial_complete_message;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    public float three_star_threshold = 0.9f;
    public float two_star_threshold = 0.6f;


    private void Awake()
    {
        int currentCoins = GameDataController.getCurrentLevelCoins();
        currentLevelCoinsUI.text = "" + currentCoins;

        instance = this;
        if (GameDataController.getLevel() == GameDataController.getLastLevel()) {
            nextLevelBtn.gameObject.SetActive(false);
        }

        ProgressData progress = new ProgressData();
        int current_level = GameDataController.getLevel();
        int max_unlocked_level = LevelController.get_max_unlocked_level();

        if (current_level == 0) {
            // tutorial
            currentLevelCoinsUI.gameObject.SetActive(false);
            stars_collected_UI.gameObject.SetActive(false);
            total_stars_collected_UI.gameObject.SetActive(false);
            tutorial_complete_message.gameObject.SetActive(true);
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);
            image3.gameObject.SetActive(false);
        } else {
            int awarded_stars = award_stars(currentCoins);
            LevelController.set_collected_stars(GameDataController.getLevel(), awarded_stars);

            stars_collected_UI.text = "" + awarded_stars;
            total_stars_collected_UI.text = "" + LevelController.get_total_stars();
        }

        progress.set_collected_stars_per_level(LevelController.get_all_collected_stars());

        // max unlocked level should only be updated when player unlocks a new level
        if (current_level == max_unlocked_level) {
            LevelController.set_max_unlocked_level(max_unlocked_level + 1);
            progress.set_max_unlocked_level(max_unlocked_level + 1);
        } else {
            LevelController.set_max_unlocked_level(max_unlocked_level);
            progress.set_max_unlocked_level(max_unlocked_level);
        }

        ProgressDataManager.SaveProgress(progress);
    }
    public void restartLevel()
    {
        SceneManager.LoadScene(LevelsList.get_level_name_from_index(GameDataController.getLevel()));
    }

    public void nextLevel() 
    {
        GameDataController.incrementLevel();
        SceneManager.LoadScene(LevelsList.get_level_name_from_index(GameDataController.getLevel()));
    }

    public void returnHome() {
        SceneManager.LoadScene("Home");
    }

    public int award_stars(int coins_collected) {
        int total_possible_coins = LevelController.get_total_coins(GameDataController.getLevel());
        if (coins_collected >= total_possible_coins * three_star_threshold) {
            return 3;
        } else if (coins_collected >= total_possible_coins * two_star_threshold) {
            return 2;
        } else {
            return 1;
        }
    }
}
