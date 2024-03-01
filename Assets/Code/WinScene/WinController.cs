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
        if (GameDataController.getLevel() == GameDataController.getLastLevel()) {
            nextLevelBtn.SetActive(false);
        }
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
}
