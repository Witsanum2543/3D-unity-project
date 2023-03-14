using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalProduct : MonoBehaviour, ITimeTracker
{
    [SerializeField] private GameObject productObject;
    [SerializeField] private StorageSystem storageArea;

    // The points of animal before producing product
    int producePoint;
    // When producePoints reach maximum product point it will produce product.
    [SerializeField] public int maxProducePoint = 10;

    void Start()
    {
        TimeSystem.Instance.RegisterTracker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Producing();
    }


}
