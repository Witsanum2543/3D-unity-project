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
}
