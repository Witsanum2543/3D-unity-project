using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLog : MonoBehaviour
{
    [HideInInspector]
    public int totalTimeUsed;
    public Dictionary<EObjectiveType, int> productSold = new Dictionary<EObjectiveType, int>();
    public int spendMoney;
    public int moneyGain;


    public void addProductSoldDict(EObjectiveType type)
    {
        if (productSold.TryGetValue(type, out int value))
        {
            // Key exists, add 1 to the value
            productSold[type] = value + 1;
        }
        else
        {
            // Key does not exist, set value to 1
            productSold.Add(type, 1);
        }
    }

}
