using UnityEngine;
using System.Collections;

public class FallingObstacle : Obstacle
{
    public float fallingStrength;
    public Rigidbody2D rb2d;

    public virtual void ActivateFall()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.AddForce(Vector2.down * fallingStrength);
    }
}
