using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int currentLevel;

    private void Awake() {
        // If more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Set the static instance to this instance
            Instance = this;
        }
        currentLevel = PlayerPrefs.GetInt("level");
        GameState.Instance.levelHardnessAdjust();
    }

    public void Goto()
    {
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void PlayAgain()
    {
        PlayerPrefs.SetInt("level", 0);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("level", 0);
    }
}
