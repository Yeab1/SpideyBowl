using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            SoundEffectsManager.instance.PlayDoubleJumpTokenCollectSound();
            BowlController.instance.hasCollectedJumpToken = true;
            Destroy(gameObject);
        }
    }
}
