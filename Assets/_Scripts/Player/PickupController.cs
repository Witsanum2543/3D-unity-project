using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public bool isHolding = false;
    private PickUpObject currentHoldingObject;

    public void setPickupObject(PickUpObject pickupObject) {
        currentHoldingObject = pickupObject;
        isHolding = true;
    }

    public PickUpObject getPickUpObject() {
        return currentHoldingObject;
    }

    public void dropItem(){
        isHolding = false;
        currentHoldingObject.dropped();
        currentHoldingObject = null;
    }

    public void destroyHoldingObject() {
        isHolding = false;
        Destroy(currentHoldingObject.gameObject);
    }
}
