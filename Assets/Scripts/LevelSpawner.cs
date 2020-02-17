using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour
{
    public Level[] levelConfig;

    private int currentLevel = 0;
    private Coroutine coroutineCurrentLevel;

    // Use this for initialization
    void Start()
    {
        coroutineCurrentLevel = StartCoroutine(LevelTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public Level GetCurrentLevel()
    {
        return levelConfig[currentLevel];
    }
    public void SetCurrentLevel(int value)
    {
        currentLevel = value;
    }
    public void IncreaseLevel()
    {
        if (currentLevel + 1 >= levelConfig.Length)
            return;

        SetCurrentLevel(currentLevel + 1);
    }
    public void DecreaseLevel()
    {
        if (currentLevel - 1 <= 0)
            return;

        SetCurrentLevel(currentLevel - 1);
    }

    public IEnumerator LevelTimer()
    {
        while (true)
        {
            print("starting level: " + currentLevel);
            yield return new WaitForSeconds(GetCurrentLevel().levelDuration);
            print("finishing level: " + currentLevel);

            IncreaseLevel();
        }
    }
}

[Serializable]
public class Level
{
    public GameObject[] objectsToSpawn;
    public float levelDuration = 10f;
    public float instantiationInterval = 1f;
}
