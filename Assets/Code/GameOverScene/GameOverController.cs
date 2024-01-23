using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public static GameOverController instance;

    // Outlets
    public GameObject restartBtn;

    private void Awake()
    {
        instance = this;
    }

    public void restartLevel()
    {
        SceneManager.LoadScene("level-" + GameDataController.currentLevel);
    }
}
