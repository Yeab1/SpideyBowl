using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            BowlController.instance.canDash = true;

            // automatically dash as soon as spice is collected
            BowlController.instance.dash();
        }
    }
}
