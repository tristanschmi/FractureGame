using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float touchSensitivity = 0.1f;
    public Transform playerBody;
    private float xRotation = 0f;
    private Vector2 lastTouchPosition;
    private bool isTouching = false;

    void Start()
    {
        // Lock Cursor is not needed on touch devices
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 deltaTouchPosition = touch.deltaPosition;

                float mouseX = deltaTouchPosition.x * touchSensitivity;
                float mouseY = deltaTouchPosition.y * touchSensitivity;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
    }
}
