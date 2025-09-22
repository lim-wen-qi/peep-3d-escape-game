using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    private Animator robotAnimator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        robotAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        float inputMagnitude = movement.magnitude;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (robotAnimator != null)
        {
            robotAnimator.SetFloat("Speed", inputMagnitude);
            robotAnimator.SetBool("IsRunning", isRunning);
        }

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        // Move direction relative to camera
        Vector3 move = camForward * movement.y + camRight * movement.x;

        controller.Move(move * Time.deltaTime * currentSpeed);

        // Rotation logic:

        if (move != Vector3.zero)
        {
            float forwardAmount = Mathf.Abs(movement.y);

            if (forwardAmount > 0.1f)
            {
                // Rotate player smoothly towards movement direction when moving forward/backward
                transform.forward = Vector3.Slerp(transform.forward, move, 0.2f);
            }
            else
            {
                // Player strafing sideways — do not rotate here to avoid weird rotation
            }
        }
        else
        {
            // **New:** If player is idle, rotate to face camera forward**
            // Smoothly rotate player to camera's forward direction
            Vector3 cameraFlatForward = cameraTransform.forward;
            cameraFlatForward.y = 0;
            cameraFlatForward.Normalize();

            transform.forward = Vector3.Slerp(transform.forward, cameraFlatForward, 0.1f);
        }

        if (inputManager.PlayerJumpedThisFrame() && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}
