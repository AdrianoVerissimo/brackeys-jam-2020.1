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

    public string highscoreLabelPrefix = "Highscore:";
    public Text textHighscore;

    protected int currentScore = 0;
    protected int highscore = 0;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        highscore = DataController.LoadHighscore();
        UpdateHighscore();

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
        SetScore(value);
    }

    public virtual void UpdateScore()
    {
        textScore.text = scoreLabelPrefix + " " + currentScore;
    }
    public virtual int GetScore()
    {
        return currentScore;
    }
    public virtual void SetScore(int value)
    {
        currentScore += value;

        if (currentScore < 0)
            currentScore = 0;
    }


    public virtual int GetHighscore()
    {
        return highscore;
    }
    public virtual void SetHighScore(int value)
    {
        highscore = value;

        if (highscore < 0)
            highscore = 0;
    }
    public virtual void UpdateHighscore()
    {
        textHighscore.text = highscoreLabelPrefix + " " + highscore;
    }
}
