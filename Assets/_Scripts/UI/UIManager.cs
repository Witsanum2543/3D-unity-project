using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header ("Shop Item slot")]
    public ShopSlot[] shopSlots;

    // The Shop panel
    public GameObject shopPanel;

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

    private void Start() {
        RenderShop();
    }

    public void RenderShop()
    {
        ItemData[] shopItems = ShopManager.Instance.itemList;
        for (int i=0; i<shopSlots.Length; i++)
        {
            shopSlots[i].Display(shopItems[i]);
        }
    }

    public void ToggleShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);

        RenderShop();
    }

    public void updateMoneyText()
    {
        money.text = GameState.Instance.getMoney().ToString();
    }
}
