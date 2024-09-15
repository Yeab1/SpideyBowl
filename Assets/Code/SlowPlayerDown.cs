using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayerDown : MonoBehaviour
{
    public float slipSpeed;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            // TODO: find a new sound.
            SoundEffectsManager.instance.PlaySlipSound();
            BowlController.instance._rb.velocity = Vector2.right * slipSpeed;
        }
    }
}
