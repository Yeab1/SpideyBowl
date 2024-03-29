using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    public bool isZoomOut;
    public float zoomAmount;
    public float zoomSpeed;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            
            if (isZoomOut)
                CameraController.instance.zoomOut(zoomAmount, zoomSpeed);
            else
                CameraController.instance.zoomIn(zoomAmount, zoomSpeed);
            
            Destroy(gameObject);
        }
    }
}
