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

    protected bool isDead = false;
    protected bool isSliding = false;

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

        StartCoroutine(DieCoroutine());
    }

    public IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(waitAfterDeath);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public virtual void SetIsSliding(bool value = true)
    {
        isSliding = value;
    }
    public virtual bool GetIsSliding()
    {
        return isSliding;
    }
}
