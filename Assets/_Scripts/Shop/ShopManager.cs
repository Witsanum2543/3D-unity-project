using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

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

    public ItemData[] itemList;

    public ItemData[] itemBuyingList;
    public int totalPrice;
    public int totalWeight;
    public int truckCapacity = 20;
    
}
