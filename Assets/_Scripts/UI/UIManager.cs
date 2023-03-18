using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header ("Panel gameobject")]
    public GameObject shopPanel;
    public GameObject mainScreen;

    [Header ("Main Screen")]
    public TextMeshProUGUI money; 


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

    public void ToggleShopPanel()
    {
        ShopManager.Instance.RenderShop();
        shopPanel.SetActive(!shopPanel.activeSelf);
        mainScreen.SetActive(!mainScreen.activeSelf);
        
    }

    public void updateMoneyText()
    {
        money.text = GameState.Instance.getMoney().ToString();
    }
}
