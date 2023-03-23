using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective")]
public class ObjectiveData : ScriptableObject
{
    public EObjectiveType objectiveType;
    public Sprite objectiveIcon;
    public int requireAmount;
    public int currentAmount;
    public bool isComplete;

    // Reset the values to their default state
    public void Reset()
    {
        currentAmount = 0;
        isComplete = false;
    }
}
