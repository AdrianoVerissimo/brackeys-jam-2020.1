using UnityEngine;
using System.Collections;

public class Hole : Obstacle
{

    public GameObject visibleArea;
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip clip;


    public override void CollisionAction()
    {
        ShowSecretArea();
    }

    public virtual void ShowSecretArea(bool value = true)
    {
        anim.Stop();
        anim.clip = clip;
        anim.Play();
    }

    public virtual void ShowVisibleArea()
    {
        ShowSecretArea(false);
    }
}
