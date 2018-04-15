using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyLevel {EASY, MEDIUM, HARD, INSANE};

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    private DifficultyLevel gameDifficulty;

    public void SetGameDifficulty(string difficultySelection)
    {
        switch (difficultySelection)
        {
            case "EASY":
                gameDifficulty = DifficultyLevel.EASY;
                DataManager.Instance.gameDifficultyLevel = gameDifficulty;
                break;
            case "MEDIUM":
                gameDifficulty = DifficultyLevel.MEDIUM;
                DataManager.Instance.gameDifficultyLevel = gameDifficulty;
                break;
            case "HARD":
                gameDifficulty = DifficultyLevel.HARD;
                DataManager.Instance.gameDifficultyLevel = gameDifficulty;
                break;
            case "INSANE":
                gameDifficulty = DifficultyLevel.INSANE;
                DataManager.Instance.gameDifficultyLevel = gameDifficulty;
                break;
            default:
                break;
        }
    }

}
