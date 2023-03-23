using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingItem : MonoBehaviour
{

    private void OnTriggerStay(Collider other) {
        if (other.tag == "pickupObject") {
            PickUpObject item = other.GetComponent<PickUpObject>();
            if (item.objectType == EObjectType.Sellable) {
                AudioManager.Instance.PlaySound("sell_product");
                Destroy(other.gameObject);
                GameState.Instance.changeMoney(item.price);
                GameState.Instance.updateObjective(item.objectiveType);
            }
        }
        
    }
}
