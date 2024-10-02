using Cinemachine;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera is not assigned in the Inspector.");
            return;
        }

        if (PController.instance != null)
        {
            // Set the Follow and LookAt targets to the player's transform
            virtualCamera.Follow = PController.instance.transform;
            virtualCamera.LookAt = PController.instance.transform;
        }
        else
        {
            Debug.LogError("PController instance is null. Camera cannot follow the player.");
        }
    }
}
