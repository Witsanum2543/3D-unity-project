using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingItem : MonoBehaviour
{

    private void OnTriggerStay(Collider other) {
        if (other.tag == "pickupObject") {
            PickUpObject item = other.GetComponent<PickUpObject>();
            if (item.objectType == EObjectType.Sellable) {
                GameState.Instance.changeMoney(item.price);
                GameState.Instance.updateObjective(item.objectiveType);
                Destroy(other.gameObject);
                AudioManager.Instance.PlaySound("sell_product");
            }
        }
        
    }
}
