using UnityEngine;
//using UnityEngine.Experimental.U2D.Animation;
//using UnityEngine.InputSystem;

public enum GroundType
{
    None,
    Soft,
    Hard
}

public class CharacterController2D : MonoBehaviour
{
    readonly Vector3 flippedScale = new Vector3(-1, 1, 1);
    readonly Quaternion flippedRotation = new Quaternion(0, 0, 1, 0);

    [Header("Character")]
    [SerializeField] protected Animator animator = null; //animation
    [SerializeField] protected Transform puppet = null;
    public Collider2D controllerCollider;

    [Header("Movement")]
    [SerializeField] float acceleration = 0.0f;
    [SerializeField] float maxSpeed = 0.0f;
    [SerializeField] float jumpForce = 0.0f;
    [SerializeField] float minFlipSpeed = 0.1f;
    [SerializeField] float jumpGravityScale = 1.0f;
    [SerializeField] float fallGravityScale = 1.0f;
    [SerializeField] float groundedGravityScale = 1.0f;
    [SerializeField] bool resetSpeedOnLand = false;

    protected Rigidbody2D controllerRigidbody;
    
    protected LayerMask softGroundMask;
    protected LayerMask hardGroundMask;

    protected Vector2 movementInput;
    [SerializeField] protected bool jumpInput;

    protected Vector2 prevVelocity;
    protected GroundType groundType;
    protected bool isFlipped;
    protected bool isJumping;
    protected bool isFalling;

    //private int animatorGroundedBool; //animation
    //private int animatorRunningSpeed; //animation
    //private int animatorJumpTrigger; //animation

    public bool CanMove { get; set; }

    protected virtual void Start()
    {
        controllerRigidbody = GetComponent<Rigidbody2D>();
        //controllerCollider = GetComponent<Collider2D>();
        softGroundMask = LayerMask.GetMask("Ground Soft");
        hardGroundMask = LayerMask.GetMask("Ground Hard");

        //animatorGroundedBool = Animator.StringToHash("Grounded"); //animation
        //animatorRunningSpeed = Animator.StringToHash("RunningSpeed"); //animation
        //animatorJumpTrigger = Animator.StringToHash("Jump"); //animation


        CanMove = true;
    }

    protected virtual void Update()
    {
        if (!CanMove)
            return;

        // Horizontal movement
        float moveHorizontal = 0.0f;

        if (Input.GetAxis("Horizontal") < 0f)
            moveHorizontal = -1.0f;
        else if (Input.GetAxis("Horizontal") > 0f)
            moveHorizontal = 1.0f;

        movementInput = new Vector2(moveHorizontal, 0);

        // Jumping input
        if (!isJumping && Input.GetButtonDown("Jump"))
            jumpInput = true;
    }

    protected virtual void FixedUpdate()
    {
        UpdateGrounding();
        UpdateVelocity();
        UpdateDirection();
        UpdateJump();
        UpdateGravityScale();

        prevVelocity = controllerRigidbody.velocity;
    }

    protected virtual void UpdateGrounding()
    {
        // Use character collider to check if touching ground layers
        if (controllerCollider.IsTouchingLayers(softGroundMask))
            groundType = GroundType.Soft;
        else if (controllerCollider.IsTouchingLayers(hardGroundMask))
            groundType = GroundType.Hard;
        else
            groundType = GroundType.None;

        // Update animator
        //animator.SetBool(animatorGroundedBool, groundType != GroundType.None); //animation
    }

    protected virtual void UpdateVelocity()
    {
        Vector2 velocity = controllerRigidbody.velocity;

        // Apply acceleration directly as we'll want to clamp
        // prior to assigning back to the body.
        velocity += movementInput * acceleration * Time.fixedDeltaTime;

        // We've consumed the movement, reset it.
        movementInput = Vector2.zero;

        // Clamp horizontal speed.
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        // Assign back to the body.
        controllerRigidbody.velocity = velocity;

        // Update animator running speed
        var horizontalSpeedNormalized = Mathf.Abs(velocity.x) / maxSpeed;
        //animator.SetFloat(animatorRunningSpeed, horizontalSpeedNormalized);

    }

    protected virtual void UpdateJump()
    {
        // Set falling flag
        if (isJumping && controllerRigidbody.velocity.y < 0)
            isFalling = true;

        // Jump
        if (jumpInput && groundType != GroundType.None)
        {
            // Jump using impulse force
            controllerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            // Set animator
            //animator.SetTrigger(animatorJumpTrigger);

            // We've consumed the jump, reset it.
            jumpInput = false;

            // Set jumping flag
            isJumping = true;
            Debug.Log("isJumping");
        }

        // Landed
        else if (isJumping && isFalling && groundType != GroundType.None)
        {
            // Since collision with ground stops rigidbody, reset velocity
            if (resetSpeedOnLand)
            {
                prevVelocity.y = controllerRigidbody.velocity.y;
                controllerRigidbody.velocity = prevVelocity;
            }

            // Reset jumping flags
            isJumping = false;
            isFalling = false;

        }
    }

    protected virtual void UpdateDirection()
    {
        // Use scale to flip character depending on direction
        if (controllerRigidbody.velocity.x > minFlipSpeed && isFlipped)
        {
            isFlipped = false;
            puppet.localScale = Vector3.one;
        }
        else if (controllerRigidbody.velocity.x < -minFlipSpeed && !isFlipped)
        {
            isFlipped = true;
            puppet.localScale = flippedScale;
        }
    }


    protected virtual void UpdateGravityScale()
    {
        // Use grounded gravity scale by default.
        var gravityScale = groundedGravityScale;

        if (groundType == GroundType.None)
        {
            // If not grounded then set the gravity scale according to upwards (jump) or downwards (falling) motion.
            gravityScale = controllerRigidbody.velocity.y > 0.0f ? jumpGravityScale : fallGravityScale;           
        }

        controllerRigidbody.gravityScale = gravityScale;
    }


}
