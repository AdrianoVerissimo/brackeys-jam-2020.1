using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour
{
    public enum Dificulty
    {
        SuperEasy,
        Easy,
        Normal,
        Hard,
        SuperHard
    }

    public float chunkLength = 13.32f;
    public Dificulty dificulty;

    public virtual float GetChunkLength()
    {
        return chunkLength;
    }
}
