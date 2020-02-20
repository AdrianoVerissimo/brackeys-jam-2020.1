using UnityEngine;
using System.Collections;

public class RunnerCharacterController2D : CharacterController2D
{
    public enum MovementDirection
    {
        Right = 1,
        Left = -1,
        Stop = 0
    };

    public MovementDirection startMovingDirection;

    protected override void Update()
    {
        base.Update();

        float moveHorizontal = (float)startMovingDirection;
        if (moveHorizontal != 0f)
        {
            movementInput = new Vector2(moveHorizontal, movementInput.y);
        }
    }
}
