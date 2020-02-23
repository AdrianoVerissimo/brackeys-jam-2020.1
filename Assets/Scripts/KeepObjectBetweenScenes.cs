using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeepObjectBetweenScenes : MonoBehaviour
{
    private static List<KeepObjectBetweenScenes> listDontDestroyObjects = new List<KeepObjectBetweenScenes>();

    // Use this for initialization
    void Start()
    {
        tag = gameObject.tag;
        if (string.IsNullOrEmpty(tag))
        {
            print("É necessário que o objeto tenha uma tag para o script KeepObjectBetweenScenes funcionar.");
            return;
        }

        var objFind = GetInList(this);
        if (objFind != null)
        {
            Destroy(gameObject);
            return;
        }

        GameObject obj = GameObject.FindGameObjectWithTag(tag);
        if (obj != null && obj.GetInstanceID() != gameObject.GetInstanceID())
        {
            if (obj.name != gameObject.name)
            {
                Destroy(obj);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
        SetInList(this);
    }

    public static void SetInList(KeepObjectBetweenScenes current)
    {
        listDontDestroyObjects.Add(current);
    }
    public static KeepObjectBetweenScenes GetInList(KeepObjectBetweenScenes current)
    {
        foreach (var item in listDontDestroyObjects)
        {
            if (item.gameObject.CompareTag(current.gameObject.tag) && item.gameObject.name == current.gameObject.name)
            {
                return item;
            }
        }
        return null;
    }
}
