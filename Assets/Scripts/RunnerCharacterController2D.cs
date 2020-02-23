using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RunnerCharacterController2D : CharacterController2D
{
    public enum MovementDirection
    {
        Right = 1,
        Left = -1,
        Stop = 0
    };
    public MovementDirection startMovingDirection;

    public LayerMask SoftGroundMask;
    public LayerMask HardGroundMask;

    public static RunnerCharacterController2D Instance;

    public float waitAfterDeath = 4f;
    public float distanceToGround = 2f;

    public Transform raycastGroundLeft, raycastGroundMiddle, raycastGroundRight;

    [Header("Audio")]
    public AudioClip audioJump;
    public AudioClip audioDie;
    public AudioClip audioLanded;
    public AudioController audioController;

    protected bool isDead = false;
    protected bool isSliding = false;

    protected float xVelocityMultiplicator = 1f;
    protected bool isGrounded = false;


    protected override void Start()
    {
        jumpInput = false;
        isJumping = false;

        base.Start();

        base.softGroundMask = SoftGroundMask;
        base.hardGroundMask = HardGroundMask;

        Instance = this;
    }

    protected override void Update()
    {
        base.Update();

        float moveHorizontal = (float)startMovingDirection;
        if (moveHorizontal != 0f)
        {
            movementInput = new Vector2(moveHorizontal, 0f);
        }

        UpdateAnimations();
    }

    public virtual void Die()
    {
        print("You died");

        isDead = true;
        controllerRigidbody.bodyType = RigidbodyType2D.Static;
        ScoreController.Instance.StopScoreCounter();

        int score = ScoreController.Instance.GetScore();

        if (DataController.CheckHighscoreBeat(score))
            DataController.SaveHighscore(score);

        MusicController.Instance.PauseMusic();
        audioController.PlayAudio(audioDie);

        StartCoroutine(DieCoroutine());
    }

    public IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(waitAfterDeath);

        MusicController.Instance.UnpauseMusic();
        GoToSceneController.GoToScene(SceneManager.GetActiveScene().name);
    }

    public virtual void ZeroVelocityY()
    {
        Vector2 newVelocity = new Vector2(controllerRigidbody.velocity.x, 0f);
        controllerRigidbody.velocity = newVelocity;
    }

    public virtual void UpdateAnimations()
    {
        if (isDead && !animator.GetBool("Die"))
        {
            animator.SetBool("Die", true);
            return;
        }

        if (isSliding)
        {
            animator.SetBool("Slide", true);
            return;
        }
        else
        {
            animator.SetBool("Slide", false);
        }

        if (isJumping)
        {
            animator.SetBool("JumpUp", controllerRigidbody.velocity.y > 0f);
            animator.SetBool("JumpDown", controllerRigidbody.velocity.y < 0f);
        }
        else
        {
            animator.SetBool("JumpUp", false);
            animator.SetBool("JumpDown", false);
        }
    }

    protected override void UpdateVelocity()
    {
        base.UpdateVelocity();

        Vector2 newVelocity = controllerRigidbody.velocity;
        newVelocity.x = newVelocity.x * xVelocityMultiplicator;
        controllerRigidbody.velocity = newVelocity;
    }

    public virtual void SetXVelocityMultiplicator(float value)
    {
        xVelocityMultiplicator = value;
    }

    public virtual void SetIsSliding(bool value = true)
    {
        isSliding = value;
    }
    public virtual bool GetIsSliding()
    {
        return isSliding;
    }

    public virtual bool IsGrounded()
    {
        Vector3 direction = transform.TransformDirection(Vector2.down) * distanceToGround;
        //Debug.DrawRay(transform.position, direction, Color.green);

        if (
            Physics2D.Raycast(raycastGroundLeft.position, Vector2.down, distanceToGround, softGroundMask) ||
            Physics2D.Raycast(raycastGroundMiddle.position, Vector2.down, distanceToGround, softGroundMask) ||
            Physics2D.Raycast(raycastGroundRight.position, Vector2.down, distanceToGround, softGroundMask)
            )
        {
            return true;
        }

        return false;
    }

    protected override void UpdateJump()
    {
        // Set falling flag
        if (isJumping && controllerRigidbody.velocity.y < 0)
            isFalling = true;

        // Jump
        if (jumpInput && groundType != GroundType.None)
        {
            if (IsGrounded())
            {
                // Jump using impulse force
                controllerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

                // Set animator
                //animator.SetTrigger(animatorJumpTrigger);

                // We've consumed the jump, reset it.
                jumpInput = false;

                // Set jumping flag
                isJumping = true;

                audioController.PlayAudio(audioJump);

                Debug.Log("isJumping");
            }
        }

        // Landed
        else if (isJumping && isFalling && groundType != GroundType.None && IsGrounded())
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

            audioController.PlayAudio(audioLanded);
        }
    }
}
