using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windmill : MonoBehaviour
{
    private bool isTrigger = false;
    public emily emily;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "pickupObject")
        {
            PickUpObject item = other.GetComponent<PickUpObject>();
            if (item.objectType == EObjectType.Sellable)
            {
                if (item.objectiveType == EObjectiveType.Tomato)
                {
                    if (!isTrigger)
                    {
                        isTrigger = true;
                        Destroy(other.gameObject);
                        emily.tomato = true;
                        emily.checkCondition();
                    }
                }
            }
        }
    }
}
