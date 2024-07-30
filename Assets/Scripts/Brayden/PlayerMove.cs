using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundCheckRadius;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private SFXPlayerController sFXPlayerController;

    Rigidbody2D rb;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sFXPlayerController = GetComponent<SFXPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the character is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Move the character left and right
        float moveInput = PlayerInput.Instance.GetMovementInput();
        if (moveInput != 0f && isGrounded)
        {
            Debug.Log("walking");
            sFXPlayerController.StartWalking();
        }
        else if (moveInput == 0f || !isGrounded)
        {
            Debug.Log("stopped walking");
            sFXPlayerController.StopWalking();
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jump if the player presses the jump button and the character is grounded
        if (isGrounded && PlayerInput.Instance.GetJumpInput())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
