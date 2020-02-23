using UnityEngine;
using System.Collections;

public class CanvasMenu : MonoBehaviour
{
    public string firstScene;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoToSceneController.GoToScene(firstScene);
        }
    }
}
