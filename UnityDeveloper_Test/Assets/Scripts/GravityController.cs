using System.Collections;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float gravityMagnitude = 9.81f;  // The magnitude of the gravity force
    public float rotationSpeed = 2f;        // Speed at which the character rotates to align with the new gravity
    private Rigidbody rb;
    private Vector3 currentGravityDirection = Vector3.down;  // Initial gravity direction
    private bool isRotating = false;        // Flag to check if the player is currently rotating
    // Add a reference to the hologram object
    public GameObject holoObject;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = currentGravityDirection * gravityMagnitude;

        // Ensure the holo object is initially inactive
        if (holoObject != null)
        {
            holoObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isRotating)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                ShowHolo(transform.forward);  // Show hologram in the forward direction
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                ShowHolo(-transform.forward);  // Show hologram in the backward direction
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                ShowHolo(Vector3.right);  // Show hologram in the left direction
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                ShowHolo(Vector3.left);  // Show hologram in the right direction
            }
            else
            {
                HideHolo();  // Hide hologram when no key is pressed
            }

            // Perform gravity change when key is released
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                ChangeGravity(transform.forward);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                ChangeGravity(-transform.forward);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                ChangeGravity(Vector3.right);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                ChangeGravity(Vector3.left);
            }
        }
    }

    void ShowHolo(Vector3 direction)
    {
        if (holoObject != null)
        {
            holoObject.SetActive(true);
            holoObject.transform.position = transform.position + direction.normalized * 1.5f; // Adjust position of holo
            holoObject.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);  // Align holo with direction
        }
    }

    void HideHolo()
    {
        if (holoObject != null)
        {
            holoObject.SetActive(false);
        }
    }

    void ChangeGravity(Vector3 newGravityDirection)
    {
        StartCoroutine(RotatePlayer(newGravityDirection));
    }

    IEnumerator RotatePlayer(Vector3 newGravityDirection)
    {
        isRotating = true;  // Prevent further input while rotating
        Vector3 gravityUp = -newGravityDirection.normalized;  // Calculate the new "up" direction

        // Determine the new forward direction for the player after the rotation
        Vector3 newForward = Vector3.Cross(transform.right, gravityUp);

        // Calculate the target rotation based on the new forward and up directions
        Quaternion targetRotation = Quaternion.LookRotation(newForward, gravityUp);

        // Smoothly rotate the player to the new orientation
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        // Finalize the rotation and set the new gravity
        transform.rotation = targetRotation;
        currentGravityDirection = newGravityDirection.normalized;
        Physics.gravity = currentGravityDirection * gravityMagnitude;
        rb.velocity = Vector3.zero;  // Reset velocity to prevent sliding or unwanted movement
        isRotating = false;  // Allow new input
    }

    void OnCollisionStay(Collision collision)
    {
        // Optional: Add grounding logic here
    }

    void OnCollisionExit(Collision collision)
    {
        // Optional: Add logic for when the character leaves a surface
    }
}
