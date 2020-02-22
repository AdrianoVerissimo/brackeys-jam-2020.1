using UnityEngine;
using System.Collections;

public class Hole : Obstacle
{

    public GameObject visibleArea;
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip clip;
    private bool alreadyPlayed = false;

    public override void CollisionAction()
    {
        ShowSecretArea();
    }

    public virtual void ShowSecretArea(bool value = true)
    {
        if(!alreadyPlayed)
        {
            anim.Stop();
            anim.clip = clip;
            anim.Play();
            alreadyPlayed = true;
        }
    }

    public virtual void ShowVisibleArea()
    {
        ShowSecretArea(false);
    }
}
