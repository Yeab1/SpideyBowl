using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public void restartLevel()
    {
        SceneManager.LoadScene(LevelsList.get_level_name_from_index(GameDataController.getLevel()));
    }
}
