using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private GameObject sceneTransition;

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
        AudioManager.Instance.PlaySound("shop_button");
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        sceneTransition.SetActive(false);
        sceneTransition.SetActive(true);
    }
    

    public void PlayAgain()
    {
        AudioManager.Instance.PlaySound("shop_button");
        Reset();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void mainMenu()
    {
        AudioManager.Instance.PlaySound("shop_button");
        Reset();
        SceneManager.LoadSceneAsync("main menu");
    }

    private void OnApplicationQuit()
    {
        Reset();
    }

    private void Reset()
    {
        PlayerPrefs.SetInt("level", 0);
        UpgradeManager.Instance.reset();
    }

}
