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

        
        List<RaycastHit> validHits = new List<RaycastHit>();
        foreach (RaycastHit hit in hits) {
            if (hit.collider.CompareTag("pickupObject")) {
                validHits.Add(hit);
            }
        }
        
        if (validHits.Count > 0) {
            validHits.Sort((x, y) => {
                // Sort by distance.
                return x.distance.CompareTo(y.distance);
            });

            OnInteractableHit(validHits[0]);
        } else {
            // If Raycast didn't hit anything interactable, it means that we must reset selectedSoil and selectedObject
            if (selectedSoil != null) {
                selectedSoil.Select(false);
                selectedSoil = null;
            }
            selectedObject = null;
        }

        // For soil interaction we use RayCast because using BoxCast will have 0 distance weird behaviour
        RaycastHit soilHit;
        if (Physics.Raycast(transform.position, Vector3.down, out soilHit, 2, layerMaskToInteract))
        {
            OnInteractableHit(soilHit);
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
            AudioManager.Instance.PlaySound("pickup_item");
            // After pickup item, deselect that item
            selectedObject = null;
            return;
        }

        if (selectedSoil != null) {
            if (playerPickupController.getPickUpObject() == null) {
                GameObject product = selectedSoil.TryHarvesting();
                if (product != null) {
                    AudioManager.Instance.PlaySound("pickup_item");
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
                    AudioManager.Instance.PlaySound("watering");
                    selectedSoil.Watering();
                }
                // Check if player holding seed while press E ?
                if (playerPickupController.getPickUpObject().objectType == EObjectType.Seed) {

                    SeedData seedData = playerPickupController.getPickUpObject().GetComponent<SeedData>();

                    if (selectedSoil.Seeding(seedData) == true) {
                        AudioManager.Instance.PlaySound("seeding");
                        playerPickupController.destroyHoldingObject();
                    }
                }
            }
        }
    }
}
