using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour
{
    public enum Dificulty
    {
        SuperEasy = 1,
        Easy = 2,
        Normal = 3,
        Hard = 4,
        SuperHard = 5
    }

    public float chunkLength = 13.32f;
    public Dificulty dificulty;
    public int scoreMultiplicator = 100;

    protected bool chunkFinished = false;
    protected Renderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        TryDestroyChunk();

        if (!GetChunkFinished())
        {
            if (HasPlayerPassedChunk())
            {
                SetChunkFinished(true);

                ScoreController.Instance.EarnScore(CalculateScore());
            }
        }
    }

    public virtual float GetChunkLength()
    {
        return chunkLength;
    }

    public virtual int CalculateScore()
    {
        return scoreMultiplicator * (int)dificulty;
    }
    public virtual bool HasPlayerPassedChunk()
    {
        float playerPosX = RunnerCharacterController2D.Instance.transform.position.x;
        if (playerPosX >= transform.position.x + GetChunkLength())
        {
            return true;
        }
        return false;
    }

    public virtual void SetChunkFinished(bool value)
    {
        chunkFinished = value;
    }
    public virtual bool GetChunkFinished()
    {
        return chunkFinished;
    }

    public void TryDestroyChunk()
    {
        Vector2 pos = transform.position;
        pos.x = pos.x + (chunkLength * 2);
        pos = Camera.main.WorldToViewportPoint(pos);
        if (pos.x < 0f)
            Destroy(gameObject);
    }

}
