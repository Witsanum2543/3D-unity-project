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
                item.price = Mathf.RoundToInt(item.price * UpgradeManager.Instance.findScale(EUpgradeName.INCREASE_PRODUCT_PRICE));
                GameState.Instance.gameLog.moneyGain += item.price;
                GameState.Instance.gameLog.addProductSoldDict(item.objectiveType);
                GameState.Instance.changeMoney(item.price);
                GameState.Instance.updateObjective(item.objectiveType);
            }
        }
        
    }
}
