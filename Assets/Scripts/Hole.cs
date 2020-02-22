using UnityEngine;
using System.Collections;

public class Hole : Obstacle
{

    public GameObject visibleArea;
    //public GameObject secretArea;

    public override void CollisionAction()
    {
        ShowSecretArea();
    }

    public virtual void ShowSecretArea(bool value = true)
    {
        //secretArea.SetActive(value);
        visibleArea.SetActive(!value);
    }
    public virtual void ShowVisibleArea()
    {
        ShowSecretArea(false);
    }
}
