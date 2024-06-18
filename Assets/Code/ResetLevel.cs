using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public void restartLevel()
    {
        if (!TutorialUtils.level_needs_tutorial(
            LevelsList.get_level_name_from_index(
                GameDataController.getLevel()))) {
                    SceneManager.LoadScene(LevelsList.get_level_name_from_index(GameDataController.getLevel()));
                }
        else {
            SceneManager.LoadScene("Tutorial");
        }   
    }
}
