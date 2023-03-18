using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public ShopSlot[] shopSlotList;
    public ItemData[] itemSellingList;
    public TextMeshProUGUI money;
    public TextMeshProUGUI totalPriceBuyText;
    public TextMeshProUGUI totalWeightBuyText;

    public Dictionary<ItemData, int> itemBuyingDictionary;
    public int totalPriceBuy = 0;
    public int totalWeightBuy = 0;
    public int truckCapacity = 20;

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
        itemBuyingDictionary = new Dictionary<ItemData, int>();
    }

    public void resetAmountBuy()
    {
        // Reset amount buy that store in each shopslot 
        foreach (ShopSlot shopSlot in shopSlotList)
        {
            shopSlot.resetAmount();
        }

        // Reset dictionary
        itemBuyingDictionary.Clear();
        totalPriceBuy = 0;
        totalWeightBuy = 0;

        totalPriceBuyText.text = "";
        totalWeightBuyText.text = totalWeightBuy.ToString() + "/" + truckCapacity.ToString();
        money.text = "$ " + GameState.Instance.getMoney().ToString();
    }

    public void addItem(ItemData item)
    {
        if (itemBuyingDictionary.ContainsKey(item))
        {
            itemBuyingDictionary[item]++;
        }
        else
        {
            itemBuyingDictionary[item] = 1;
        }
        calculateTotalPriceAndWeightBuy();
    }

    public void substractItem(ItemData item)
    {
        if (itemBuyingDictionary.ContainsKey(item))
        {
            itemBuyingDictionary[item]--;
        }
        else
        {
            itemBuyingDictionary.Remove(item);
        }
        calculateTotalPriceAndWeightBuy();
    }

    private void calculateTotalPriceAndWeightBuy()
    {
        totalPriceBuy = 0;
        totalWeightBuy = 0;
        foreach (KeyValuePair<ItemData, int> item in itemBuyingDictionary)
        {
            totalPriceBuy += item.Key.price * item.Value;
            totalWeightBuy += item.Key.weight * item.Value;
        }
        totalPriceBuyText.text = "- " + totalPriceBuy.ToString();
        totalWeightBuyText.text = totalWeightBuy.ToString() + "/" + truckCapacity.ToString();

        if (totalPriceBuy == 0)
        {
            totalPriceBuyText.text = "";
        }
    }

    public void buyButton()
    {
        if (totalPriceBuy > GameState.Instance.money) return;
        if (totalWeightBuy > truckCapacity) return;
        
        GameState.Instance.changeMoney(-totalPriceBuy);
        RenderShop();
    }

    public void RenderShop()
    {
        resetAmountBuy();
        money.text = "$ " + GameState.Instance.getMoney().ToString();
        for (int i=0; i<shopSlotList.Length; i++)
        {
            shopSlotList[i].Display(itemSellingList[i]);
        }
    }
    
}
