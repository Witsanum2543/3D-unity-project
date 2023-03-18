using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingItem : MonoBehaviour
{
    GameState gameState;

    private void Start() {
        gameState = GameState.Instance;
    }


    private void OnTriggerStay(Collider other) {
        if (other.tag == "pickupObject") {
            PickUpObject item = other.GetComponent<PickUpObject>();
            if (item.objectType == EObjectType.Sellable) {
                gameState.changeMoney(item.price);
                Destroy(other.gameObject);
                AudioManager.Instance.PlaySound("sell_product");
            }
        }
        
    }
}
