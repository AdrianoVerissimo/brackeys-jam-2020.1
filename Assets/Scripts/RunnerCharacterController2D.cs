using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    protected override void Start()
    {
        jumpInput = false;
        isJumping = false;

        base.Start();

        base.softGroundMask = SoftGroundMask;
        base.hardGroundMask = HardGroundMask;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
