using UnityEngine;

public class PlayerMovementTouch : MonoBehaviour
{
    public float speed = 5f;
    public Camera mainCamera; // Reference to the main camera
    private Vector3 moveDirection;
    private Vector2 startTouchPosition;
    private bool isMoving = false;
    private const float swipeThreshold = 0.1f; // Minimum swipe distance to register movement

    void Update()
    {
        HandleTouchInput();
        if (isMoving)
        {
            MovePlayer(); // Continue moving as long as touch is held after detecting a swipe
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 1)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startTouchPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        Vector2 currentTouchPosition = touch.position;
                        Vector2 swipeVector = currentTouchPosition - startTouchPosition;

                        if (!isMoving && swipeVector.magnitude >= swipeThreshold * Screen.dpi) // Check for initial swipe
                        {
                            CalculateMoveDirection(swipeVector);
                            isMoving = true; // Only set isMoving to true once a valid swipe is detected
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        isMoving = false;
                        moveDirection = Vector3.zero; // Reset direction when touch ends
                        break;
                }
            }
        }
    }

    void CalculateMoveDirection(Vector2 swipeVector)
    {
        Vector2 movementDirection = swipeVector.normalized;

        // Calculate forward and right vectors relative to the camera
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;

        // Flatten y-components for horizontal movement only
        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // Calculate move direction using camera-relative vectors
        moveDirection = camForward * movementDirection.y + camRight * movementDirection.x;
    }

    void MovePlayer()
    {
        // Apply movement based on calculated direction
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }
}
