using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truck : MonoBehaviour
{
    private bool isTrigger = false;
    public emily emily;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "pickupObject")
        {
            PickUpObject item = other.GetComponent<PickUpObject>();
            if (item.objectType == EObjectType.Sellable)
            {
                if (item.objectiveType == EObjectiveType.Milk)
                {
                    if (!isTrigger)
                    {
                        isTrigger = true;
                        Destroy(other.gameObject);
                        emily.milk = true;
                        emily.checkCondition();
                    }
                }
            }
        }
    }
}
