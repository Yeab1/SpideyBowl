using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target;

    public Vector3 offset;
    public float smoothness;
    public bool startZoom;
    public float initialFOV;
    public float desiredFOV;
    public float zoomSpeed;
    public float currentFOV;
    bool isZoomingOut;

    Vector3 _velocity;

    private void Awake()
    {
        instance = this;
    }
    
    void Start() {
        if (target) {
            offset = transform.position - target.position;
            currentFOV = Camera.main.orthographicSize;
        }
    }

    void Update() {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position + offset,
            ref _velocity,
            smoothness
        );

        if (startZoom)
        {
            if (isZoomingOut) {
                currentFOV += Time.deltaTime * zoomSpeed;
                currentFOV = Mathf.Clamp(currentFOV, initialFOV, desiredFOV);
                Camera.main.orthographicSize = currentFOV;
                if (currentFOV >= desiredFOV)
                {
                    startZoom = false;
                }
            } else {
                currentFOV -= Time.deltaTime * zoomSpeed;
                currentFOV = Mathf.Clamp(currentFOV, desiredFOV, initialFOV);
                Camera.main.orthographicSize = currentFOV;
                if (currentFOV <= desiredFOV)
                {
                    startZoom = false;
                }
            }
        }
    }

    public void zoomOut(float FOVChangeAmount, float zoomSpeed) {
        initialFOV = currentFOV; 
        desiredFOV = initialFOV + FOVChangeAmount;
        this.zoomSpeed = zoomSpeed;
        startZoom = true;
        isZoomingOut = true;
    }

    public void zoomIn(float FOVChangeAmount, float zoomSpeed) {
        initialFOV = currentFOV; 
        desiredFOV = initialFOV - FOVChangeAmount;
        this.zoomSpeed = zoomSpeed;
        startZoom = true;
        isZoomingOut = false;
    }
}
