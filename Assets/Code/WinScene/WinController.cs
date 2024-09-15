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
    public TMP_Text totalCoinsUI;

    private void Awake()
    {
        int currentCoins = GameDataController.getCurrentLevelCoins();
        int totalCoins = GameDataController.getTotalCoins();
        currentLevelCoinsUI.text = "" + currentCoins;
        totalCoinsUI.text = "" + totalCoins;

        instance = this;
        if (GameDataController.getLevel() == GameDataController.getLastLevel()) {
            nextLevelBtn.SetActive(false);
        }
    }
    public void restartLevel()
    {
        GameDataController.revertTotalCoinUpdate();
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
}
