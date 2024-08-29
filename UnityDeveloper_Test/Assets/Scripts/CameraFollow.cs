using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player transform
    public Vector3 offset; // Offset from the player position

    void Start()
    {
        // Initialize offset if not set
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Follow the player
        transform.position = player.position + offset;

        // Optionally, you can add smoothness
        // transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * smoothSpeed);

        // Look at the player
        transform.LookAt(player);
    }
}
