using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBlock : MonoBehaviour
{
    public GameObject productObject;

    public bool isEmpty()
    {
        if (productObject == null) {
            return true;
        }
        return false;
    }

    public void store(GameObject comingProduct)
    {
        // Instantiate product to the storage position
        GameObject product = Instantiate(comingProduct, transform);
        product.transform.position = transform.position;
        productObject = product;

        // store storage information to the product
        productObject.GetComponent<PickUpObject>().storageBlock = this;
    }

    public void remove()
    {
        productObject.GetComponent<PickUpObject>().storageBlock = null;
        productObject = null;
    }
}
