using System.Collections;
using UnityEngine;

public class GravityManipulation : MonoBehaviour
{
    public Vector3 currentGravity = Vector3.down * 9.81f; // Default gravity
    public float gravityRotationSpeed = 2f; // Speed of rotation

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = currentGravity;
    }

    void Update()
    {
        // Rotate the character before actually changing the gravity
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(RotateToDirection(Vector3.right));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(RotateToDirection(Vector3.left));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(RotateToDirection(Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(RotateToDirection(Vector3.back));
        }
    }

    IEnumerator RotateToDirection(Vector3 newGravityDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(newGravityDirection, Vector3.up);
        Quaternion initialRotation = transform.rotation;

        float elapsedTime = 0f;
        while (elapsedTime < gravityRotationSpeed)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, (elapsedTime / gravityRotationSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        currentGravity = newGravityDirection * 9.81f;
        Physics.gravity = currentGravity;
    }
}
