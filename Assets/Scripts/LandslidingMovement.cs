using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandslidingMovement : MonoBehaviour
{
    public static LandslidingMovement Instance;
    public Rigidbody2D rb2d;

    public float defaultSpeed = 2f;
    [HideInInspector] public float speed;

    protected RigidbodyType2D originalBodyType;

    private void Start()
    {
        Instance = this;
        originalBodyType = rb2d.bodyType;

        speed = defaultSpeed;
    }

    private void FixedUpdate()
    {
        //transform.Translate(Vector3.right * speed * Time.deltaTime);
        rb2d.velocity = Vector3.right * speed * Time.deltaTime;
    }

    public void Pause()
    {
        print("oi");
        rb2d.bodyType = RigidbodyType2D.Static;
    }
    public void Unpause()
    {
        rb2d.bodyType = originalBodyType;
    }

}
