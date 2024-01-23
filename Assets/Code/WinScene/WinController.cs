using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public static WinController instance;

    // Outlets
    public GameObject restartBtn;
    public GameObject nextLevelBtn;

    private void Awake()
    {
        instance = this;
    }

    public void restartLevel()
    {
        SceneManager.LoadScene("level-" + GameDataController.currentLevel);
    }

    public void nextLevel() 
    {
        if (GameDataController.currentLevel != GameDataController.lastLevel) {
            GameDataController.currentLevel++;
            SceneManager.LoadScene("Level-" + GameDataController.currentLevel);
        }
    }
}
