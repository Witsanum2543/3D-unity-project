using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour, ITimeTracker
{
    public static UIManager Instance { get; private set; }

    [Header ("Panel gameobject")]
    public GameObject shopPanel;
    public GameObject mainScreen;

    [Header ("Main Screen")]
    public TextMeshProUGUI money;
    public TextMeshProUGUI truckArriveTime; 


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

    private void Start() {
        TimeSystem.Instance.RegisterTracker(this);

        // Initialize All Text
        truckArriveTime.text = "Truck : Ready";
        money.text = GameState.Instance.getMoney().ToString();
    }

    public void ToggleShopPanel()
    {
        AudioManager.Instance.PlaySound("shop_button");
        ShopManager.Instance.RenderShop();
        shopPanel.SetActive(!shopPanel.activeSelf);
        mainScreen.SetActive(!mainScreen.activeSelf);
    }

    public void updateMoneyText()
    {
        money.text = GameState.Instance.getMoney().ToString();
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        truckArriveTime.text = "Truck : " + GameState.Instance.truckArrive;
        if (GameState.Instance.truckArrive == 0)
        {
            truckArriveTime.text = "Truck : Ready";
        }
    }
}
