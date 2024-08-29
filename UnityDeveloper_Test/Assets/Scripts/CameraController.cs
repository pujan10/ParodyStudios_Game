using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform for calculating rotation pivot
    public float mouseSensitivity = 100f; // Mouse sensitivity for camera rotation

    private float pitch = 0f; // X-axis rotation (up and down)
    private float yaw = 0f;   // Y-axis rotation (left and right)

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void RotateCamera()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update yaw and pitch based on mouse input
        yaw += mouseX;
        pitch -= mouseY;

        // Clamp pitch to prevent flipping the camera upside down
        pitch = Mathf.Clamp(pitch, -45f, 75f); // Adjust these values to your preference

        // Apply rotation around the player
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        
    }
}
