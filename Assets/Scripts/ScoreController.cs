using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance;

    public int scoreGainValue = 10;
    public float secondsToGainScore = 1f;

    public string scoreLabelPrefix = "Score:";
    public Text textScore;

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
        UpdateScore();
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
        print(currentScore);

        if (currentScore < 0)
            currentScore = 0;
    }

    public virtual void UpdateScore()
    {
        textScore.text = scoreLabelPrefix + " " + currentScore;
    }
}
