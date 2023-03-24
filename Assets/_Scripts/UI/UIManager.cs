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

    [Header ("Truck Arrive Bar")]
    public TruckArriveBar truckArriveBar;
    public TextMeshProUGUI truckArriveTimeText; 

    [Header ("Win Lose Screen")]
    public GameObject winScreen;
    public GameObject loseScreen;

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
        truckArriveTimeText.text = "Ready";
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

    public void startTruck(int totalTime)
    {
        // Prevent Text delay due to waiting clockupdate
        truckArriveTimeText.text = GameState.Instance.truckArrive.ToString();
        truckArriveBar.startDrive(totalTime);
    }



    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        truckArriveTimeText.text = GameState.Instance.truckArrive.ToString();
        if (GameState.Instance.truckArrive == 0)
        {
            truckArriveTimeText.text = "Ready";
        }
    }
}
