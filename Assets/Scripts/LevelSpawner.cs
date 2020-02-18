using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour
{
    public LevelConfig[] levelConfig;

    private int currentLevel = 0;
    private Coroutine coroutineCurrentLevel;
    private Coroutine coroutineSpawnObject;

    // Use this for initialization
    void Start()
    {
        coroutineCurrentLevel = StartCoroutine(LevelTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public LevelConfig GetCurrentLevel()
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
    public void FinishLevel()
    {
        StopCoroutine(coroutineSpawnObject);
    }

    public IEnumerator LevelTimer()
    {
        while (true)
        {
            print("starting level: " + currentLevel);

            coroutineSpawnObject = StartCoroutine(GetCurrentLevel().SpawnObject());

            yield return new WaitForSeconds(GetCurrentLevel().levelDuration);

            print("finishing level: " + currentLevel);

            FinishLevel();
            IncreaseLevel();
        }
    }
}

[System.Serializable]
public class LevelConfig
{
    public LevelObject[] listObjects;
    public float levelDuration = 10f;
    public float instantiationInterval = 1f;

    public IEnumerator SpawnObject()
    {
        while (true)
        {
            int rand = Random.Range(0, listObjects.Length);
            LevelObject objSpawn = listObjects[rand];
            GameObject obj = objSpawn.objectToSpawn;

            MonoBehaviour.Instantiate(obj, objSpawn.transformPosition.position, Quaternion.identity);

            yield return new WaitForSeconds(instantiationInterval);
        }
    }
}

[System.Serializable]
public class LevelObject
{
    public Transform transformPosition;
    public GameObject objectToSpawn;
}
