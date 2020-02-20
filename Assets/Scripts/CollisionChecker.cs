using UnityEngine;
using System.Collections;

public class CollisionChecker : MonoBehaviour
{
    public LayerMask collisionMask;
    public Rigidbody2D rb2d;
    public Collider2D checkCollider;
    public int collisionLimit = 3;

    protected Collider2D[] arrayCollision; //stores all the collisions that happened
    protected ContactFilter2D collisionFilter; //filter used for the collisions

    protected virtual void Start()
    {
        InitializeCollisionChecker();
    }

    public virtual bool CheckCollision()
    {
        //there's no collision or the collider isn't enabled
        if (checkCollider == null || !checkCollider.enabled)
            return false;

        //check collision
        int collisionCount = checkCollider.OverlapCollider(collisionFilter, arrayCollision);

        //no collision
        if (collisionCount == 0)
            return false;

        return true;
    }

    public virtual void InitializeCollisionChecker()
    {
        collisionFilter = new ContactFilter2D();
        collisionFilter.SetLayerMask(collisionMask);
        collisionFilter.useTriggers = true;
        arrayCollision = new Collider2D[collisionLimit];
    }
}


