using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] chunksToSpawn;

    public int amountChunksOnScreen = 2;
    public float spawnX = 0f;
    public float chunkLength = 13.32f;

    protected GameObject objPlayer;

    private void Start()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");

        SpawnInitialChunks();
    }

    private void Update()
    {
        if (HasReachedLimit())
            SpawnChunk();
    }

    public void SpawnChunk()
    {
        int rand = Random.Range(0, chunksToSpawn.Length);
        Vector2 spawnPosition = new Vector2(spawnX, 0f);

        GameObject obj = Instantiate(chunksToSpawn[rand], spawnPosition, Quaternion.identity, transform);
        Chunk chunk = obj.GetComponent<Chunk>();
        spawnX += chunk.GetChunkLength();
    }

    public void SpawnInitialChunks()
    {
        for (int count = 0; count < amountChunksOnScreen; count++)
        {
            SpawnChunk();
        }
    }

    public bool HasReachedLimit()
    {
        if (objPlayer.transform.position.x > (spawnX - amountChunksOnScreen * chunkLength))
            return true;

        return false;
    }

}