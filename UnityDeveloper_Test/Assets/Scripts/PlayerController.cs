using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    public GameTimer gameTimer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameTimer = FindObjectOfType<GameTimer>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimations();
        if (transform.position.y < -10) // Adjust threshold as needed
        {
                gameTimer.GameOver();
        }
        else if (transform.position.x < -10) // Adjust threshold as needed
        {
            gameTimer.GameOver();
        }
        else if (transform.position.z < -10) // Adjust threshold as needed
        {
            gameTimer.GameOver();
        }
    }

    void HandleMovement()
    {
        Vector3 localMoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        Vector3 globalMoveDirection = transform.TransformDirection(localMoveDirection);

        rb.AddForce(globalMoveDirection * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(-Physics.gravity.normalized * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleAnimations()
    {
            // Set isRunning parameter
            if (rb.velocity.magnitude > 0.1f && isGrounded)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            // Set isFalling parameter
            if (!isGrounded)
            {
                animator.SetBool("isFalling", true);
            }
            else
            {
                animator.SetBool("isFalling", false);
            }
    }
    void OnCollisionEnter(Collision collision)
    {
        // Existing collision logic...

        if (collision.gameObject.CompareTag("Ground"))
        {
            // Player is on the ground
        }
    }
    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
