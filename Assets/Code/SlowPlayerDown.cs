using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayerDown : MonoBehaviour
{
    public float slipSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            SoundEffectsManager.instance.PlaySlipSound();
            BowlController.instance._rb.velocity = Vector2.right * slipSpeed;
        }
    }
}