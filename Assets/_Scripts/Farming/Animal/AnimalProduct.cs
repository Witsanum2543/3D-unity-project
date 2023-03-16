using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalProduct : MonoBehaviour, ITimeTracker
{
    [SerializeField] private GameObject productObject;
    [SerializeField] private StorageSystem storageArea;


    // When producePoints reach maximum product point it will produce product.
    [SerializeField] public int maxProducePoint = 10;
    int producePoint;

    // If food point reach 0 mean that animal are starving.
    [SerializeField] public float maximumFoodPoint = 60;
    public float foodPoint;

    [SerializeField] public AnimalFoodArea animalFoodArea;

    private FoodBar foodBar;

    void Start()
    {
        foodBar = GetComponentInChildren<FoodBar>();
        foodPoint = maximumFoodPoint;
        TimeSystem.Instance.RegisterTracker(this);
    }

    public void Producing()
    {
        // +1 point for each TimeTick
        producePoint++;
        if (producePoint >= maxProducePoint) {
            produceProduct();
            producePoint = 0;
        }
    }

    public void produceProduct() {
        storageArea.storeNewProduct(productObject);
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        if (calculateFoodPercentage() >= 80) {
            Producing();
        }

        foodPoint--;
        // Hungry then Eat
        if (isHungry())
        {
            if (!animalFoodArea.isEmpty()) {
                foodPoint += animalFoodArea.animalEatFood(2);
            }
        }

        if (foodBar != null) {
            foodBar.UpdateFoodBar(calculateFoodPercentage());
        }
        
    }

    public float calculateFoodPercentage()
    {
        return (foodPoint/maximumFoodPoint) * 100;
    }

    public void Died()
    {
        Destroy(this);
    }

    private bool isHungry()
    {
        if (foodPoint < maximumFoodPoint)
        {
            return true;
        }
        return false;
    }

}
