using UnityEngine;
using System.Collections;

public class DataController : MonoBehaviour
{
    protected static int highscore;
    protected static string highscoreDataLabel = "highscore";

    public static int LoadHighscore()
    {
        return PlayerPrefs.GetInt(highscoreDataLabel, 0);
    }
    public static void SaveHighscore(int value)
    {
        PlayerPrefs.SetInt(highscoreDataLabel, value);
    }
    public static bool CheckHighscoreBeat(int value)
    {
        if (value > LoadHighscore())
            return true;

        return false;
    }
}
