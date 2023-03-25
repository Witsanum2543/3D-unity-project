using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour, ITimeTracker
{
    public static GameState Instance { get; private set; }

    [HideInInspector]
    public int truckArrive = 0;

    [Header ("Game Difficultly")]
    public int timeLeft;
    public int money;
    public int baseTruckTime = 5;
    public int weightScaleTime = 1;
    [Range(1, 5)]
    public int objectiveAmount;
    public int minWork;
    public int maxWork;

    [Header ("Difficultly Scale")]
    public int timeIncrease;
    public int moneyIncrease;
    public int workIncrease;

    [Header ("Objective")]
    public List<ObjectiveData> objectiveList; 
    // using for calculate total work that player need to do

    private void Awake() {
        Time.timeScale = 1;
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

    public void levelHardnessAdjust()
    {
        int currentLevel = LevelManager.Instance.currentLevel;

        timeLeft += currentLevel * timeIncrease; 
        money += currentLevel * moneyIncrease; 
        objectiveAmount += currentLevel;
        minWork += currentLevel * workIncrease;
        maxWork += currentLevel * (workIncrease * 2);

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
        checkWinCondition();
    }

    public void checkWinCondition()
    {
        foreach(ObjectiveData objective in objectiveList)
        {
            if (!objective.isComplete) return;
        }
        Time.timeScale = 0;
        UIManager.Instance.winScreen.SetActive(true);
        return;
    }

    public void checkLose()
    {
        if (timeLeft <= -1)
        {
            Time.timeScale = 0;
            UIManager.Instance.loseScreen.SetActive(true);
            return;
        }
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        timeLeft--;
        checkLose();
        
        if (truckArrive != 0)
        {
            truckArrive--;
        }
    }
    


}
