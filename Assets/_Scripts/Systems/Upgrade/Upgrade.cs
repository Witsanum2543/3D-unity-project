using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public GameObject upgradeButton;
    public TextMeshProUGUI priceText;
    public UpgradeData data;

    private void Start() {
        priceText.text = data.price.ToString();
        if (data.isUpgrade)
        {
            upgradeButton.SetActive(false);
        } else {
            upgradeButton.SetActive(true);
        }
    }

    public void upgrade()
    {
        if (GameState.Instance.money >= data.price)
        {
            AudioManager.Instance.PlaySound("buy");
            data.upgrading();
            GameState.Instance.changeMoney(-data.price);
            UpgradeUI.Instance.moneyText.text = GameState.Instance.money.ToString();
            upgradeButton.SetActive(false);
            return;
        }
        AudioManager.Instance.PlaySound("buy_cooldown");
    }

    public void reset()
    {
        data.reset();
    }
}
