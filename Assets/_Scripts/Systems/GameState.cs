using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [SerializeField] public int timeLeft;
    public int money;
    public int truckArrive;

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

}
