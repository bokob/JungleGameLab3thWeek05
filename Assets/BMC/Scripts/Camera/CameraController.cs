using Unity.Cinemachine;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    CinemachineCamera _followCamera;

    void Awake()
    {
        _followCamera = FindAnyObjectByType<CinemachineCamera>();
    }

    public void Init()
    {
        CameraTarget cameraTarget = new CameraTarget();
        cameraTarget.TrackingTarget = GameObject.FindGameObjectWithTag("Player").transform;
        _followCamera.Target = cameraTarget;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
