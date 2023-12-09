using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            BowlController.instance.canDoubleJump = true;
            Destroy(gameObject);
        }
    }
}
