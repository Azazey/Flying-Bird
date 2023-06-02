using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData 
{
    public const string Score = "Score";
    public const string HighScore = "HighScore";
    public const string LevelDifficult = "LevelDifficult";
    private const int _resetValue = 0;

    public static void AddScore(int value)
    {
        int currentScore = PlayerPrefs.GetInt(Score);
        currentScore += value;
        PlayerPrefs.SetInt(Score, currentScore);
    }

    public static void SetHighScore()
    {
        if (PlayerPrefs.GetInt(Score) > PlayerPrefs.GetInt(HighScore))
        {
            PlayerPrefs.SetInt(HighScore, PlayerPrefs.GetInt(Score));
        }
    }

    public static void SetLevelDifficult(int difficult)
    {
        if (difficult == 0)
        {
            PlayerPrefs.SetInt(LevelDifficult, 0);
        }
        else if (difficult == 1)
        {
            PlayerPrefs.SetInt(LevelDifficult, 1);
        }
        else if (difficult == 2)
        {
            PlayerPrefs.SetInt(LevelDifficult, 2);
        }
        else
        {
            Debug.Log("Wrong Difficult");
        }
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.SetInt(HighScore, _resetValue);
    }

    public static void OnGameStart()
    {
        PlayerPrefs.SetInt(Score, _resetValue);
    }
}
