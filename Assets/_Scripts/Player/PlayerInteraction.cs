using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    [Header ("Raycast Parameter")]
    [SerializeField] Vector3 boxSize = new Vector3(0.5f, 1f, 0.5f);
    [SerializeField] float maxDistance = 1f;
    [SerializeField] String layerMaskToInteractName = "Interact Raycast";

    PlayerController playerController;
    PickupController playerPickupController;

    Soil selectedSoil;
    PickUpObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.GetComponentInParent<PlayerController>();
        playerPickupController = playerController.pickupPivot.GetComponent<PickupController>();
    }


    void Update()
    {
        processingRaycast();
    }

    /* 
        This function will Raycast "Interact Raycast" layer to let player interact with it
        It sorting distance also the tag to give specific behaviour that we want. 
    */
    void processingRaycast() {
        // ignore everything else except "Interact Raycast"
        int layerMaskToInteract = LayerMask.GetMask(layerMaskToInteractName);

        RaycastHit[] hits = Physics.BoxCastAll(transform.position, boxSize, transform.forward, transform.rotation, maxDistance, layerMaskToInteract);

        Array.Sort(hits, (x, y) => {
            // Compare the tags first.
            int tagCompare = x.collider.tag.CompareTo(y.collider.tag);
            if (tagCompare != 0) {
                // If the tags are different, prioritize the "pickupObject" tag.
                if (x.collider.tag == "pickupObject") {
                    return -1; // x should come before y
                } else {
                    return 1; // y should come before x
                }
            } else {
                // If the tags are the same, sort by distance.
                return x.distance.CompareTo(y.distance);
            }
        });
       
       if (hits.Length != 0) {
            OnInteractableHit(hits[0]);
       } else {
            // If Raycast didn't hit anything interactable, it mean that we must reset selectedSoil and selectedObject
            if (selectedSoil != null)
            {
                selectedSoil.Select(false);
                selectedSoil = null;
            }
             selectedObject = null;
            
        }

       
    }

    private void OnDrawGizmos() {
    // Draw the wireframe cube for the raycast.
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + transform.forward * maxDistance, boxSize);
    }

    void OnInteractableHit(RaycastHit hit) {
        Collider other = hit.collider;

        // Check if the player is going to interact with soil
        if (other.tag == "soil")
        {
            Soil soil = other.GetComponent<Soil>();
            HandleSoilSelection(soil);
            return;
        }

        // Check if the player is going to interact with pickupObject
        if (other.tag == "pickupObject")
        {
            if (!playerController.pickupController.isHolding) {
                selectedObject = other.GetComponent<PickUpObject>();
            }
        } 
    }

    IEnumerator HandleDeselection() {
        yield return new WaitForSeconds(2f);

        selectedSoil.Select(false);
        selectedSoil = null;
        selectedObject = null;
    }

    // Handle selection process
    void HandleSoilSelection(Soil soil)
    {
        // Set previously selected soil to be deactive
        if (selectedSoil != null) 
        {  
            selectedSoil.Select(false);
        }

        // Set new selected soil to be active
        selectedSoil = soil;
        soil.Select(true);
    }

    public void InteractMouse()
    {
        if (selectedObject != null) {
            selectedObject.PickUp(playerController.pickupPivot);
            // After pickup item, deselect that item
            selectedObject = null;
            return;
        }

        if (selectedSoil != null) {
            if (playerPickupController.getPickUpObject() == null) {
                GameObject product = selectedSoil.TryHarvesting();
                if (product != null) {
                    playerPickupController.setPickupObject(product.GetComponent<PickUpObject>());
                    product.GetComponent<PickUpObject>().PickUp(playerPickupController.gameObject);
                }
            }
        }
    }

    public void InteractEKey()
    {
        if (selectedSoil != null) {      
            if (playerPickupController.isHolding) {
                // Check if player holding watercan while press E ?
                if (playerPickupController.getPickUpObject().objectType == EObjectType.Bucket) {
                    selectedSoil.Watering();
                }
                // Check if player holding seed while press E ?
                if (playerPickupController.getPickUpObject().objectType == EObjectType.Seed) {

                    SeedData seedData = playerPickupController.getPickUpObject().GetComponent<SeedData>();

                    if (selectedSoil.Seeding(seedData) == true) {
                        playerPickupController.destroyHoldingObject();
                    }
                }
            }
        }
    }
}
