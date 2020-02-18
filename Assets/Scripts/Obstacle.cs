using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Vector2 velocity;

    private void Start()
    {
        rb2D.velocity = velocity;
    }

    private void Update()
    {
        
    }
}