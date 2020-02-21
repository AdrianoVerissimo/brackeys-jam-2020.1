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
    }

    public virtual void Die()
    {
        print("You died");

        int score = ScoreController.Instance.GetScore();

        if (DataController.CheckHighscoreBeat(score))
            DataController.SaveHighscore(score);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public virtual void ZeroVelocityY()
    {
        Vector2 newVelocity = new Vector2(controllerRigidbody.velocity.x, 0f);
        controllerRigidbody.velocity = newVelocity;
    }
}
