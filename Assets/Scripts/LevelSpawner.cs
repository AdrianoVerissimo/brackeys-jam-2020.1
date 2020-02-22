using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour
{
    public Level[] levels;

    public int amountChunksOnScreen = 2;
    public float spawnX = 0f;

    protected GameObject objPlayer;
    protected float countLimitSpawnChunk = 0f;
    protected List<float> lastChunksLength = new List<float>();

    protected int currentLevelIndex = 0;

    protected Coroutine coroutineCurrentLevelTimer;

    private void Start()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");

        SpawnInitialChunks();

        StartLevel();
    }

    private void Update()
    {
        if (HasReachedLimit())
            SpawnChunk();
    }

    public void SpawnChunk()
    {
        int rand = Random.Range(0, GetCurrentLevel().chunksToSpawn.Length);
        Vector2 spawnPosition = new Vector2(spawnX, 0f);

        GameObject obj = Instantiate(GetCurrentLevel().chunksToSpawn[rand], spawnPosition, Quaternion.identity, transform);
        Chunk chunk = obj.GetComponent<Chunk>();
        spawnX += chunk.GetChunkLength();

        if (lastChunksLength.Count > amountChunksOnScreen)
        {
            lastChunksLength.RemoveAt(0);
        }

        lastChunksLength.Add(chunk.GetChunkLength());
    }

    public void SpawnInitialChunks()
    {
        for (int count = 0; count < amountChunksOnScreen; count++)
        {
            SpawnChunk();
        }
    }

    public float SumLastChunksLength()
    {
        float value = 0f;

        foreach (var item in lastChunksLength)
        {
            value += item;
        }
        return value;
    }

    public bool HasReachedLimit()
    {
        if (objPlayer.transform.position.x > (spawnX - SumLastChunksLength()))
            return true;

        return false;
    }

    public virtual Level GetCurrentLevel()
    {
        return GetLevel(currentLevelIndex);
    }
    public virtual Level GetLevel(int index)
    {
        return levels[index];
    }
    public virtual void AdvanceLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Length)
            currentLevelIndex = levels.Length - 1;
    }
    public IEnumerator LevelTimer()
    {
        yield return new WaitForSeconds(GetCurrentLevel().levelDuration);

        AdvanceLevel();
        StopLevel();
        StartLevel();
    }
    public virtual void StartLevel()
    {
        coroutineCurrentLevelTimer = StartCoroutine(LevelTimer());
    }
    public virtual void StopLevel()
    {
        StopCoroutine(coroutineCurrentLevelTimer);
    }
}

[System.Serializable]
public class Level
{
    public float levelDuration = 10f;
    public GameObject[] chunksToSpawn;
}