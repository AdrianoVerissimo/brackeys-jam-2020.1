using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour
{
    public string exitButtonName = "Exit";

    void Update()
    {
        if (Input.GetButtonDown(exitButtonName))
        {
            Exit();
        }
    }

    public virtual void Exit()
    {
        Application.Quit();
    }
}
