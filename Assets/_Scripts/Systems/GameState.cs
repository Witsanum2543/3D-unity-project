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

    [Header ("Objective")]
    public List<ObjectiveData> objectiveList; 
    // using for calculate total work that player need to do
    public int minWork;
    public int maxWork;
    [Range(1, 5)]
    public int objectiveAmount;

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
        randomizeObjective();
        ObjectiveManager.Instance.RenderObjective();
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
        UIManager.Instance.startTruck(truckArrive);
    }

    private void randomizeObjective()
    {
        // Randomly select object by remove objective so that it meet totalObjective amount
        int numToRemove = objectiveList.Count - objectiveAmount;
        for (int i = 0; i < numToRemove; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, objectiveList.Count);
            objectiveList.RemoveAt(randomIndex);
        }

        foreach(ObjectiveData objective in objectiveList)
        {
            objective.Reset();
            objective.requireAmount = Random.Range(minWork, maxWork+1);
        }
    }

    public List<ObjectiveData> getObjectives()
    {
        return objectiveList;
    }

    public void updateObjective(EObjectiveType type)
    {
        foreach(ObjectiveData objective in objectiveList)
        {
            if (objective.objectiveType == type)
            {
                objective.currentAmount += 1;
                if (objective.currentAmount >= objective.requireAmount)
                {
                    AudioManager.Instance.PlaySound("quest_complete");
                    objective.isComplete = true;
                }
                ObjectiveManager.Instance.RenderObjective();
            }
        }
    }

    public void winCondition()
    {

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
