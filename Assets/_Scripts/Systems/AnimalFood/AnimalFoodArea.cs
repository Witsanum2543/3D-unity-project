using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFoodArea : MonoBehaviour
{
    [SerializeField] public GameObject[] floors;

    [SerializeField] public int foodPerGrassPack = 100;

    [SerializeField] public float maxFoodPoint = 1000;
    public float foodPoint;

    private AnimalFoodVisual foodVisual;

    // Start is called before the first frame update
    void Start()
    {
        foodVisual = GetComponent<AnimalFoodVisual>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        if (other.GetComponent<PickUpObject>().objectType == EObjectType.Feed) {
            Destroy(other.gameObject);
            foodPoint += foodPerGrassPack;
            if (foodPoint > maxFoodPoint) {
                foodPoint = maxFoodPoint;
            }
            foodVisual.changeVisualDependOnFoodValue(calculateFoodPercentage());
        }
    }

    public bool isEmpty()
    {
        if (foodPoint > 0) {
            return false;
        }
        return true;
    }

    public int animalEatFood(int foodAte)
    {
        // not enough food to ate
        if (foodPoint - foodAte < 0) {
            return 0;
        }
        else 
        {
            foodPoint -= foodAte;
            return foodAte;
        }
    }

    public float calculateFoodPercentage()
    {
        return (foodPoint/maxFoodPoint) * 100;
    }

}
