using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController playerController;
    private PlayerInputHandler inputHandler;

    // Player Movement Values
    public float playerMoveSpeed = 10f;
    public float speedSmoothRate = 50;
    private Vector3 moveDirection;
    private Vector3 velocity;

    // External force (e.g., wind) applied to the player
    private Vector3 externalForce = Vector3.zero;

    private LayerMask ground;
    public GameObject groundCheckObject;
    public float groundCheckRadius = 0.1f;
    public float jumpHeight = 1.5f;
    private bool isGrounded;
    private bool hitTheRoof;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        inputHandler = GetComponent<PlayerInputHandler>();
        ground = LayerMask.GetMask("Ground");
        hitTheRoof = false;
    }

    private void Update()
    {
        Move();
        JumpAndGravity();
    }

    private void Move()
    {
        float targetSpeed = playerMoveSpeed;

        if (inputHandler.moveInput == Vector2.zero)
        {
            targetSpeed = 0.0f;
        }

        float currentSpeed = new Vector3(playerController.velocity.x, 0, playerController.velocity.z).magnitude;
        float smoothSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * speedSmoothRate);

        Vector3 inputDirection = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y).normalized;
        moveDirection = (transform.right * inputDirection.x + transform.forward * inputDirection.z).normalized;

        // Applying movement with external forces (like wind)
        playerController.Move((moveDirection * smoothSpeed + externalForce) * Time.deltaTime);

        // Gradually reduce external force over time
        externalForce = Vector3.Lerp(externalForce, Vector3.zero, Time.deltaTime * 5f);
    }

    private void JumpAndGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheckObject.transform.position, groundCheckRadius, ground);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded && inputHandler.jumpInput)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * GlobalGravity.Instance.gravity);
        }

        if (hitTheRoof)
        {
            velocity.y = -2f;
        }

        velocity.y += GlobalGravity.Instance.gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }

    public void ApplyExternalForce(Vector3 force)
    {
        externalForce += force;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            hitTheRoof = true;
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillBox"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ShowDeadScreen();
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ShowWinScreen();
        }
        if (other.gameObject.CompareTag("StartLine"))
        {
            GameObject.Find("MainPanel").GetComponent<UIController>().timerStart = true;
        }
    }
}
