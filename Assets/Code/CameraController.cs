using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public CinemachineVirtualCamera virtualCamera;
    public bool startZoom;
    public float initialOrthoSize;
    public float desiredOrthoSize;
    public float zoomSpeed;
    public float currentOrthoSize;
    public bool isZoomingOut;

    private void Awake()
    {
        instance = this;
    }
    
    void Start() {
        if (virtualCamera != null)
        {
            // Access the lens settings of the virtual camera
            currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        }
    }

    void Update() {
        if (startZoom)
        {
            if (isZoomingOut) {
                // zoom out
                currentOrthoSize = Mathf.Lerp(currentOrthoSize, desiredOrthoSize, Time.deltaTime * zoomSpeed);
                virtualCamera.m_Lens.OrthographicSize = currentOrthoSize;

                // finished zooming?
                if (Mathf.Approximately(currentOrthoSize, desiredOrthoSize))
                {
                    startZoom = false;
                } 
            } else {
                // zoom in
                currentOrthoSize = Mathf.Lerp(currentOrthoSize, desiredOrthoSize, Time.deltaTime * zoomSpeed);
                virtualCamera.m_Lens.OrthographicSize = currentOrthoSize;

                // finished zooming?
                if (Mathf.Approximately(currentOrthoSize, desiredOrthoSize))
                {
                    startZoom = false;
                }
            }
        }
    }

    public void zoomOut(float FOVChangeAmount, float zoomSpeed) {
        initialOrthoSize = currentOrthoSize; 
        desiredOrthoSize = initialOrthoSize + FOVChangeAmount;
        this.zoomSpeed = zoomSpeed;
        startZoom = true;
        isZoomingOut = true;
    }

    public void zoomIn(float FOVChangeAmount, float zoomSpeed) {
        initialOrthoSize = currentOrthoSize; 
        desiredOrthoSize = initialOrthoSize - FOVChangeAmount;
        this.zoomSpeed = zoomSpeed;
        startZoom = true;
        isZoomingOut = false;
    }
}
