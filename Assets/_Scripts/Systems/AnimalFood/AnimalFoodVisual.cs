using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFoodVisual : MonoBehaviour, ITimeTracker
{
    [SerializeField] public GameObject[] floors;

    [SerializeField] private Mesh dirt;
    [SerializeField] private Mesh dirt_0_25;
    [SerializeField] private Mesh dirt_25_50;
    [SerializeField] private Mesh dirt_50_75;
    [SerializeField] private Mesh dirt_75_100;

    private AnimalFoodArea animalFoodArea;


    // Start is called before the first frame update
    void Start()
    {
        animalFoodArea = GetComponent<AnimalFoodArea>();
        TimeSystem.Instance.RegisterTracker(this);
    }

    public void changeVisualDependOnFoodValue(float foodPercentage)
    {
        if (foodPercentage <= 0) {
            changeAllMeshFilter(dirt);
        }
        else if (foodPercentage <= 25) {
            changeAllMeshFilter(dirt_0_25);
        }
        else if (foodPercentage <= 50) {
            changeAllMeshFilter(dirt_25_50);
        }
        else if (foodPercentage <= 75) {
            changeAllMeshFilter(dirt_50_75);
        }
        else if (foodPercentage <= 100) {
            changeAllMeshFilter(dirt_75_100);
        }
    }

    public void changeAllMeshFilter(Mesh newMesh)
    {
        foreach(GameObject floor in floors) {
            floor.GetComponentInChildren<MeshFilter>().mesh = newMesh;
        }
    }

    public void ClockUpdate(GameTimeStamp timeStamp) 
    {
        Debug.Log(animalFoodArea.calculateFoodPercentage());
        changeVisualDependOnFoodValue(animalFoodArea.calculateFoodPercentage());
    }
}
