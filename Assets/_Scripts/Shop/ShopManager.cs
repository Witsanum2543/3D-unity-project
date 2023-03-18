using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour, ITimeTracker
{
    public static ShopManager Instance { get; private set; }

    public ShopSlot[] shopSlotList;
    public ItemData[] itemSellingList;
    public TextMeshProUGUI money;
    public TextMeshProUGUI totalPriceBuyText;
    public TextMeshProUGUI totalWeightBuyText;
    public TextMeshProUGUI truckArriveTime;

    public Dictionary<ItemData, int> itemBuyingDictionary;
    public Dictionary<ItemData, int> cargoToDeliver;
    public int totalPriceBuy = 0;
    public int totalWeightBuy = 0;
    public int truckCapacity = 20;

    [Header ("Storage area")]
    public StorageSystem storageArea;

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
        itemBuyingDictionary = new Dictionary<ItemData, int>();

        // Initialize All Text
        truckArriveTime.text = "Truck : Ready";
    }

    public void resetAmountBuy()
    {
        // Reset amount buy that store in each shopslot 
        foreach (ShopSlot shopSlot in shopSlotList)
        {
            shopSlot.resetAmount();
        }

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
        AudioManager.Instance.PlaySound("buy");
        if (totalPriceBuy == 0) return;
        if (totalPriceBuy > GameState.Instance.money) return;
        if (totalWeightBuy > truckCapacity) return;
        if (GameState.Instance.truckArrive != 0) return;
        
        GameState.Instance.changeMoney(-totalPriceBuy);
        GameState.Instance.sendTruckOut(totalWeightBuy);
        // Prevent Text display delay
        truckArriveTime.text = "Truck : " + GameState.Instance.truckArrive;
        // Clone dict
        cargoToDeliver = new Dictionary<ItemData, int>(itemBuyingDictionary);
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

    private void storeItemToStorage()
    {
        foreach (KeyValuePair<ItemData, int> item in cargoToDeliver)
        {
            for (int i=0; i<item.Value; i++)
            {
                storageArea.storeNewProduct(item.Key.gameModel);
            }
        }
        cargoToDeliver = null;
    }   

    public void ClockUpdate(GameTimeStamp timeStamp)
    {

        truckArriveTime.text = "Truck : " + GameState.Instance.truckArrive;
        if (GameState.Instance.truckArrive == 0)
        {
            truckArriveTime.text = "Truck : Ready";
        }
        if (cargoToDeliver != null && GameState.Instance.truckArrive == 0)
        {
            storeItemToStorage();
        }
    }
    
}
