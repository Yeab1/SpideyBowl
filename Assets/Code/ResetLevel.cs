using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public static ResetLevel instance;

    // private void Awake()
    // {
    //     instance = this;
    // }
    public void restartLevel()
    {
        SceneManager.LoadScene("level-" + GameDataController.getLevel());
    }
}
