using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController playerController;

    Soil selectedSoil;
    PickUpObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,  1)) {
            OnInteractableHit(hit);
        }
    }

    void OnInteractableHit(RaycastHit hit) {
        Collider other = hit.collider;
        Debug.Log(other.tag);
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

        // Unselect the soil if the player is not standing on any soil at the moment
        if (selectedSoil != null)
        {
            selectedSoil.Select(false);
            selectedSoil = null;
        }
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

    public void Interact()
    {
        if (selectedObject != null) {
            selectedObject.Interact(playerController.pickupPivot);
            // After pickup item, deselect that item
            selectedObject = null;
            return;
        }

        if (selectedSoil != null) {
            selectedSoil.Interact();
            return;
        }
    }
}
