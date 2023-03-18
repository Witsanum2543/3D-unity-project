using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSystem : MonoBehaviour
{
    [Header("Floor list")]
    [SerializeField] private GameObject[] floors;

    private int maximumProductAmount;
    private int currentProductAmount;
    private int numberContainInEachFloor = 4;

    private void Start() {
        maximumProductAmount = floors.Length * numberContainInEachFloor;
    }

    private void storePosition(Transform storageFloor)
    {
        
    }

    // Finding empty spot in storage to fit in new coming product
    public void storeNewProduct(GameObject comingProduct)
    {
        foreach(GameObject floor in floors) {
            foreach(Transform storageBlock in floor.GetComponent<TileStorage>().eachStoragePosition) {
                StorageBlock storage = storageBlock.GetComponent<StorageBlock>();
                if (storage.isEmpty()) {
                    // store product in the storage
                    storage.store(comingProduct);
                    return;
                }
            }
        }
      
    }

    // when player pick item out reposition
    private void reposition()
    {

    }
}
