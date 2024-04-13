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

    public float three_star_threshold = 0.9f;
    public float two_star_threshold = 0.6f;


    private void Awake()
    {
        int currentCoins = GameDataController.getCurrentLevelCoins();
        currentLevelCoinsUI.text = "" + currentCoins;

        instance = this;
        if (GameDataController.getLevel() == GameDataController.getLastLevel()) {
            nextLevelBtn.SetActive(false);
        }

        int awarded_stars = award_stars(currentCoins);
        LevelController.set_collected_stars(GameDataController.getLevel(), awarded_stars);

        stars_collected_UI.text = "" + awarded_stars;
        total_stars_collected_UI.text = "" + LevelController.get_total_stars();

        // Saving current level + 1 because we want the player to return to the next level
        // when they return not back to the one they just won
        ProgressData progress = new ProgressData(
                                        LevelController.get_all_collected_stars(), 
                                        GameDataController.getLevel() + 1);
        ProgressDataManager.SaveProgress(progress);                                
    }
    public void restartLevel()
    {
        SceneManager.LoadScene("level-" + GameDataController.getLevel());
    }

    public void nextLevel() 
    {
        GameDataController.incrementLevel();
        SceneManager.LoadScene("Level-" + GameDataController.getLevel());
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
