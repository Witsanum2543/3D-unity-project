using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    

    [SerializeField] public EObjectType objectType;
    private GameObject playerPickupPivot;
    [SerializeField] public string productName;
    [SerializeField] public int price;

    [SerializeField] public StorageBlock storageBlock;

    // Update is called once per frame
    void Update()
    {

        if (playerPickupPivot != null) {
            transform.position = playerPickupPivot.transform.position;
            transform.rotation = playerPickupPivot.transform.rotation;
        }
    }

    // Receive interact from player to pickup this object
    public void PickUp(GameObject pickupPivot)
    {
        playerPickupPivot = pickupPivot;
        // pass this object to player pickupPivot
        playerPickupPivot.GetComponent<PickupController>().setPickupObject(GetComponent<PickUpObject>());
        enableCollision(false);

        // remove from storage if it in storage
        if (storageBlock != null) {
            storageBlock.remove();
        }
    }

    // Player drop this item
    public void dropped() {
        playerPickupPivot = null;
        enableCollision(true);
    }

    public void enableCollision(bool enable) {
        if (enable) {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<MeshCollider>().enabled = true;
        } else {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<MeshCollider>().enabled = false;
        }
    }
}
