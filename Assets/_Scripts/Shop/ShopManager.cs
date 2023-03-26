using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour, ITimeTracker
{
    public static ShopManager Instance { get; private set; }

    
    public ShopSlot[] shopSlotList;
    public ItemData[] itemSellingList;
    [Header ("Text")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI totalPriceBuyText;
    public TextMeshProUGUI totalWeightBuyText;

    public Dictionary<ItemData, int> itemBuyingDictionary;
    public Dictionary<ItemData, int> cargoToDeliver;
    public int totalPriceBuy = 0;
    public int totalWeightBuy = 0;
    public int truckCapacity = 20;

    [Header ("Storage area")]
    public StorageSystem storageArea;

    // Spawn animal waypoint
    public GameObject animalSpawnWaypoint;
    private int xPos;
    private int zPos;

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

        float increaseCapacity = UpgradeManager.Instance.findScale(EUpgradeName.DOUBLE_TRUCK_CAPACITY);
        totalPriceBuyText.text = "";
        totalWeightBuyText.text = totalWeightBuy.ToString() + "/" + (truckCapacity * increaseCapacity).ToString();
        moneyText.text = GameState.Instance.money.ToString();
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
        totalPriceBuyText.text = "- " + (totalPriceBuy * UpgradeManager.Instance.findScale(EUpgradeName.SHOP_SALE)).ToString();

        float increaseCapacity = UpgradeManager.Instance.findScale(EUpgradeName.DOUBLE_TRUCK_CAPACITY);
        totalWeightBuyText.text = totalWeightBuy.ToString() + "/" + (truckCapacity * increaseCapacity).ToString();

        if (totalWeightBuy > (truckCapacity * increaseCapacity))
        {
            totalWeightBuyText.color = new Color(200f/255f, 56f/255f, 56f/255f);;
        } else {
            totalWeightBuyText.color = Color.white;
        }

        if (totalPriceBuy == 0)
        {
            totalPriceBuyText.text = "";
        }
    }

    public void buyButton()
    {
        totalPriceBuy = Mathf.RoundToInt(totalPriceBuy * UpgradeManager.Instance.findScale(EUpgradeName.SHOP_SALE));
        float increaseCapacity = UpgradeManager.Instance.findScale(EUpgradeName.DOUBLE_TRUCK_CAPACITY);
        if (totalPriceBuy == 0 ||
            totalPriceBuy > GameState.Instance.money ||
            totalWeightBuy > (truckCapacity * increaseCapacity) ||
            GameState.Instance.truckArrive != 0
        ) {
            AudioManager.Instance.PlaySound("buy_cooldown") ;
            return;
        }
        
        AudioManager.Instance.PlaySound("buy");

        GameState.Instance.changeMoney(-totalPriceBuy);
        GameState.Instance.sendTruckOut(totalWeightBuy);
        
        // Clone dict
        cargoToDeliver = new Dictionary<ItemData, int>(itemBuyingDictionary);
        RenderShop();
    }

    public void RenderShop()
    {
        resetAmountBuy();
        moneyText.text = GameState.Instance.money.ToString();
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
                if (item.Key.itemName == "cow" || item.Key.itemName == "chicken")
                {
                    randomSpawnAnimal(item.Key.gameModel);
                }
                else
                {
                    storageArea.storeNewProduct(item.Key.gameModel);
                }
            }
            
        }
        cargoToDeliver = null;
    }

    private void randomSpawnAnimal(GameObject animal)
    {
        xPos = Random.Range(-15, 15);
        zPos = Random.Range(-4, 4);
        Vector3 spawnPosition = animalSpawnWaypoint.transform.position + new Vector3(xPos, 0.1f, zPos);
        Instantiate(animal, spawnPosition, Quaternion.identity);
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        if (cargoToDeliver != null && GameState.Instance.truckArrive == 0)
        {   
            AudioManager.Instance.PlaySound("car_horn");
            storeItemToStorage();
        }
    }
    
}
