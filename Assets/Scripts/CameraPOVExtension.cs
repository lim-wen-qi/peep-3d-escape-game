using UnityEngine;
using Cinemachine;

public class CameraPOVExtension : CinemachineExtension
{
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float clampAngle = 80f;

    private InputManager inputManager;
    private Vector2 currentRotation;

    protected override void Awake()
    {
        base.Awake(); // don't access InputManager here
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage != CinemachineCore.Stage.Aim || vcam.Follow == null)
            return;

        // Lazy get InputManager
        if (inputManager == null)
        {
            inputManager = InputManager.Instance;
            if (inputManager == null) return; // still not ready
        }

        Vector2 mouseDelta = inputManager.GetMouseDelta();
        currentRotation.x += mouseDelta.x * horizontalSpeed * deltaTime;
        currentRotation.y += mouseDelta.y * verticalSpeed * deltaTime;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -clampAngle, clampAngle);
        state.RawOrientation = Quaternion.Euler(-currentRotation.y, currentRotation.x, 0f);
    }
}

