using UnityEngine;
using System.Collections;

public class LandslideStopper : MonoBehaviour
{
    public string[] playerTags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckPlayerTag(collision.tag))
        {
            LandslidingMovement.Instance.Pause();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CheckPlayerTag(collision.tag))
        {
            LandslidingMovement.Instance.Unpause();
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
