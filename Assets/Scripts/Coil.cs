using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Coil : MonoBehaviour
{
    public Transform destinationMoveY;
    public float moveDuration = 1f;
    public string[] playerTags;
    public bool resetGravityAfterMove = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckPlayerTag(collision.tag))
        {
            Tweener tween = collision.transform.DOMoveY(destinationMoveY.position.y, moveDuration);
            if (resetGravityAfterMove)
            {
                tween.OnComplete(
                    () => {
                        Vector2 newVel = collision.GetComponent<Rigidbody2D>().velocity;
                        newVel.y = 0f;
                        collision.GetComponent<Rigidbody2D>().velocity = newVel;
                        }
                    );
            }
        }
    }

    public bool CheckPlayerTag(string value)
    {
        foreach (var item in playerTags)
        {
            if (value == item)
            {
                return true;
            }
        }

        return false;
    }
}
