using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour, ITimeTracker
{
    public static GameState Instance { get; private set; }

    [SerializeField] public int timeLeft;
    public int money;
    public int truckArrive = 0;
    // independent of amount of item that player buy
    public int baseTruckTime = 10;
    public int weightScaleTime = 1;

    private void Awake() {
        // If more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Set the static instance to this instance
            Instance = this;
        }
    }

    private void Start() {
        TimeSystem.Instance.RegisterTracker(this);
    }

    public int getMoney()
    {
        return money;
    }

    public void changeMoney(int change)
    {
        money += change;
        UIManager.Instance.updateMoneyText();
    }

    public int calculateTruckTime(int weight)
    {
        return baseTruckTime + (weight * weightScaleTime);
    }

    public void sendTruckOut(int weight)
    {
        truckArrive = calculateTruckTime(weight);
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        timeLeft--;

        if (truckArrive != 0)
        {
            truckArrive--;
        }
    }
    


}
