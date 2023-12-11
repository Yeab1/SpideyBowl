using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBound : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
