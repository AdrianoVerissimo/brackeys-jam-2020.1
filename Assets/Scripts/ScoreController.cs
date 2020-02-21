using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance;

    public int scoreGainValue = 10;
    public float secondsToGainScore = 1f;

    protected int currentScore = 0;
    protected int highscore = 0;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        StartCoroutine(ScoreTick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ScoreTick()
    {
        yield return new WaitForSeconds(secondsToGainScore);

        EarnScore(scoreGainValue);
        StartCoroutine(ScoreTick());
    }

    public virtual void EarnScore(int value)
    {
        currentScore += value;

        if (currentScore < 0)
            currentScore = 0;
    }
}
