using UnityEngine;
using System.Collections;

public class SmashTrap : Obstacle
{
    public FallingObstacle fallingObstacle;

    public override void CollisionAction()
    {
        fallingObstacle.ActivateFall();
    }
}
