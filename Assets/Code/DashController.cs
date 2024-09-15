using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            BowlController.instance.canDash = true;
            transform.parent = other.gameObject.transform;
            // Set the local position of this object to be at the center
            // of the bowl in the x-axis, a little higher in the y-axis,
            // and in front of the bowl in the z-axis.
            transform.localPosition = new Vector3(0f, 2.25f, -0.1f);

            // automatically dash as soon as spice is collected
            BowlController.instance.dash();
        }
    }
}
