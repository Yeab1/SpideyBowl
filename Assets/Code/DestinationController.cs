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
            StartCoroutine(WinRoutine());
        }
    }

    IEnumerator WinRoutine()
    {
        // Update the total coin count to display on the Win screen
        GameDataController.updateCurrentLevelCoins(BowlController.instance.coinCount);

        // Play win sound
        SoundEffectsManager.instance.PlayWinSound();

        yield return new WaitForSeconds(1f); // Wait for 1 second

        SceneManager.LoadScene("win");
    }
}
