using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject howToPlayCanvas;
    public GameObject truckArriveBar;
    public GameObject howToPlayButton;
    public GameObject upgradeButton;
    public GameObject mainScreen;

    public GameObject currentPage;

    public void ToggleHowToPlayCanvas(bool toggle)
    {
        AudioManager.Instance.PlaySound("shop_button");
        if (toggle == true)
        {
            truckArriveBar.SetActive(false);
            howToPlayButton.SetActive(false);
            mainScreen.SetActive(false);
            upgradeButton.SetActive(false);
            howToPlayCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else if (toggle == false)
        {
            truckArriveBar.SetActive(true);
            howToPlayButton.SetActive(true);
            mainScreen.SetActive(true);
            upgradeButton.SetActive(true);
            howToPlayCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void TogglePages(GameObject page)
    {
        AudioManager.Instance.PlaySound("shop_button");
        if (currentPage != null)
        {
            currentPage.SetActive(false);
            currentPage = page;
            currentPage.SetActive(true);
        }
        currentPage = page;
        currentPage.SetActive(true);
    }



}
