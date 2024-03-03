using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinationController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            // update the total coin count to display on Win screen
            GameDataController.updateCurrentLevelCoins(BowlController.instance.coinCount);
            GameDataController.updateTotalCoins();
            SceneManager.LoadScene("win");
        }
    }
}
