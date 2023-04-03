using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject truckArriveBar;
    public GameObject howToPlayButton;
    public GameObject mainScreen;
    public GameObject upgradeUI;

    public TextMeshProUGUI moneyText;

    public void ToggleUpgradeUI(bool toggle)
    {
        AudioManager.Instance.PlaySound("shop_button");
        if (toggle == true)
        {
            truckArriveBar.SetActive(false);
            howToPlayButton.SetActive(false);
            mainScreen.SetActive(false);
            moneyText.text = GameState.Instance.money.ToString();
            upgradeUI.SetActive(true);
            Time.timeScale = 0;
        }
        else if (toggle == false)
        {
            truckArriveBar.SetActive(true);
            howToPlayButton.SetActive(true);
            mainScreen.SetActive(true);
            upgradeUI.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
