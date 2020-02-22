using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Tunnel : Obstacle
{
    public Transform[] pathsToFollow;
    public float followDuration = 2f;
    public bool resetGravityAfterMove = true;

    protected Vector3[] pathsPositions;

    // Use this for initialization
    void OnEnable()
    {
        ConvertPathsToVector();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void CollisionAction()
    {
        Tweener tween = player.transform.DOPath(pathsPositions, followDuration, PathType.CatmullRom, PathMode.Sidescroller2D);
        player.SetIsSliding();

        if (resetGravityAfterMove)
        {
            tween.OnComplete(
                () => {
                    player.ZeroVelocityY();
                    player.SetIsSliding(false);
                }
                );
        }
    }

    public virtual void ConvertPathsToVector()
    {
        pathsPositions = new Vector3[pathsToFollow.Length];
        for (int i = 0; i < pathsToFollow.Length; i++)
        {
            Transform item = (Transform)pathsToFollow[i];
            pathsPositions[i] = item.position;
        }
    }
}
