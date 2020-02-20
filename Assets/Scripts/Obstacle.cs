using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    protected RunnerCharacterController2D player;
    public string[] playerTags;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RunnerCharacterController2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("oi");
        if (CheckPlayerTag(collision.tag))
        {
            CollisionAction();
        }
    }

    public bool CheckPlayerTag(string value)
    {
        foreach (var item in playerTags)
        {
            if (value == item)
            {
                print("collision");
                return true;
            }
        }

        return false;
    }

    public virtual void CollisionAction()
    {
        print("die");
        player.Die();
    }
}