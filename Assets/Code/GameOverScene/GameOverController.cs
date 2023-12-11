using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public static WinController instance;

    // Outlets
    public GameObject restartBtn;

    private void Awake()
    {
        instance = this;
    }

    public void restartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
