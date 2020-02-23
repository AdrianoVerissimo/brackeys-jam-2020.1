using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Coil : Obstacle
{
    public Transform destinationMoveY;
    public float moveDuration = 1f;
    public bool resetGravityAfterMove = true;

    [SerializeField] private Animation anim;

    public override void CollisionAction()
    {
        Tweener tween = player.transform.DOMoveY(destinationMoveY.position.y, moveDuration);
        if (resetGravityAfterMove)
        {
            anim.Play();
            tween.OnComplete(
                () => {
                    Vector2 newVel = player.GetComponent<Rigidbody2D>().velocity;
                    newVel.y = 0f;
                    player.GetComponent<Rigidbody2D>().velocity = newVel;
                }
                );
        }
    }
}
