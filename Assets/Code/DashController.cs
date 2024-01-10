using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
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
            BowlController.instance.canDash = true;
            transform.parent = other.gameObject.transform;
            // Set the local position of this object to be at the center
            // of the bowl in the x-axis, a little higher in the y-axis,
            // and in front of the bowl in the z-axis.
            transform.localPosition = new Vector3(0f, 2.25f, -0.1f);
        }
    }
}
