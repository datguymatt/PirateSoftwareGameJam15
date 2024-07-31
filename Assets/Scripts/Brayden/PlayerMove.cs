using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float shadowSpeedMultiplier;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpSideForce;
    [SerializeField] float timeBeforeFloating;
    [SerializeField] float totalFloatTime;
    [SerializeField] float gravityScale;
    [SerializeField] float floatGravityScale;
    [SerializeField] float groundCheckRadius;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private SFXPlayerController sFXPlayerController;

    Rigidbody2D rb;
    bool isGrounded;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        //sFXPlayerController = GetComponent<SFXPlayerController>();
    }

    private void OnEnable()
    {
        Actions.OnPlayerModeChange += OnPlayerModeChange;
    }

    private void OnDisable()
    {
        Actions.OnPlayerModeChange -= OnPlayerModeChange;
    }

    void OnPlayerModeChange()
    {
        if (!PlayerInfo.Instance.IsInShadowMode)
        {
            maxSpeed *= shadowSpeedMultiplier;
            acceleration *= shadowSpeedMultiplier;
        }
        else
        {
            maxSpeed /= shadowSpeedMultiplier;
            acceleration /= shadowSpeedMultiplier;
        }

    }

    // Update is called once per frame
    void Update()
    {


        // Check if the character is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Get movement input
        float moveInput = PlayerInput.Instance.GetMovementInput();
        if (moveInput != 0f && isGrounded)
        {
            Debug.Log("walking");
            //sFXPlayerController.StartWalking();
        }
        else if (moveInput == 0f || !isGrounded)
        {
            Debug.Log("stopped walking");
            //sFXPlayerController.StopWalking();
        }

        // Calculate force for horizontal movement
        Vector2 force = new Vector2(moveInput * acceleration, 0) * Time.deltaTime;
        Debug.Log($"{force}");

        // Apply force for horizontal movement in FixedUpdate
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        // Limit the maximum horizontal speed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // Jump if the player presses the jump button and the character is grounded
        if (isGrounded && PlayerInput.Instance.GetJumpInput())
        {
            // Apply jump force while maintaining current horizontal velocity
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
            rb.AddForce(new Vector2(moveInput * jumpSideForce, jumpForce), ForceMode2D.Impulse);
        }

        if (!isGrounded)
        {
            // the horizontal speed slows down alot when jumping, this is to counteract
            rb.AddForce(force * 0.48f, ForceMode2D.Impulse);
        }

        ShadowMovement();
    }

    void ShadowMovement()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0f)
        {
            rb.gravityScale = gravityScale;
            timer = 0f;
        }

        if (isGrounded)
        {
            rb.gravityScale = gravityScale;
        }

        if (PlayerInfo.Instance.IsInShadowMode && !isGrounded)
        {
            if (PlayerInput.Instance.GetFloatInputInitial())
            {
                timer = totalFloatTime;

            }

            if (PlayerInput.Instance.GetFloatInputHeld() && timer > 0f && timer <= (totalFloatTime - timeBeforeFloating))
            {

                rb.gravityScale = floatGravityScale;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }

        }
    }
}
