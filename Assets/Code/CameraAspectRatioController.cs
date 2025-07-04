using UnityEngine;
using Cinemachine;

public class CameraAspectRatioController : MonoBehaviour
{
    // Camera Aspect Ratio Settings
    public float targetAspectRatio = 19.5f / 9f;
    public float targetOrthographicSize = 6f;
    public CinemachineVirtualCamera virtualCamera;

    void Awake() {
        if (virtualCamera == null)
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            if (virtualCamera == null)
            {
                Debug.LogError("CinemachineOrthographicCameraController requires a CinemachineVirtualCamera reference.");
                return;
            }
        }
        AdjustCameraAspectRatio();
    }

    // Adjusts camera aspect ratio to fit 19.5 x 9 for the current
    // screen dimensions.
    public void AdjustCameraAspectRatio() {
        // Get the current aspect ratio.
        float currentAspectRatio = (float)Screen.width / Screen.height;

        // The horizontal size that needs to be maintained for all screens
        // This size should be able to fit all elements an Iphone-12 screen
        // would fit.
        float targetHorizontalSize = targetOrthographicSize * targetAspectRatio;

        // The Orthographic size that is need to maintain the target horizontal
        // size.
        float newOrthographicSize = targetHorizontalSize / currentAspectRatio;
        virtualCamera.m_Lens.OrthographicSize = newOrthographicSize;
        Debug.Log("Setting O size to: " + newOrthographicSize);
    }
}
