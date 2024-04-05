using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] GameObject soundSystem;
    [SerializeField] public static Difficulty difficulty;

    public void moveToScene(int sceneIndex)
    {
        
        SceneManager.LoadScene(sceneIndex);
    }

    public void SetDifficultyHelper(string difficulty)
    {
        switch (difficulty)
        {
            case "easy": SetDifficulty(Difficulty.easy); break;
            case "medium": SetDifficulty(Difficulty.medium); break;
            case "hard": SetDifficulty(Difficulty.hard); break;
            default: break;
        }
    }
    void SetDifficulty(Difficulty diff)
    {
        difficulty = diff;
    }
}
