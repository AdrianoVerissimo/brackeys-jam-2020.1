using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandslidingMovement : MonoBehaviour
{
    public float defaultSpeed = 2f;
    [HideInInspector] public float speed;

    private void Start()
    {
        speed = defaultSpeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

}
