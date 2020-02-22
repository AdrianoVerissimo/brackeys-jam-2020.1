using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanvasGameOver : MonoBehaviour
{
    public static CanvasGameOver Instance;
    public GameObject panel;

    private void Start()
    {
        Instance = this;
    }

    public virtual void PlayAgain()
    {
        GoToSceneController.GoToScene(SceneManager.GetActiveScene().name);
    }
    public virtual void ShowPanel(bool value = true)
    {
        panel.SetActive(value);
    }
    public virtual void HidePanel()
    {
        ShowPanel(false);
    }
}
