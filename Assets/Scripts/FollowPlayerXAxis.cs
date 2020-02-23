using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerXAxis : MonoBehaviour
{

    private RunnerCharacterController2D player;
    public float distanceX = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RunnerCharacterController2D>();
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - distanceX, transform.position.y, 1f);
    }
}
