using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManager.Instance.PlaySound("shop_button");
        PlayerPrefs.SetInt("level", 0);
        SceneManager.LoadSceneAsync("Scene1");
    }

    public void Quit()
    {
        AudioManager.Instance.PlaySound("shop_button");
        PlayerPrefs.SetInt("level", 0);
        Application.Quit();
    }
}
