using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
      // Receive game input to handle it
    [SerializeField] private GameInput gameInput;

    [Header ("Movement parameter")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;

    // Interaction components
    PlayerInteraction playerInteraction;

    [Header ("Pickup component")]
    [SerializeField] public GameObject pickupPivot;
    [SerializeField] public PickupController pickupController;

    // Animation state
    private bool isRunning = false;

    private void Start() {
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

    private void Update() {
        Move();
        Interact();
    }

    public void Move() {
        Vector2 inputVector = gameInput.GetMovementVector();
        inputVector = inputVector.normalized;

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        
        isRunning = moveDirection != Vector3.zero;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }


    public void Interact() {
        if (Input.GetButtonDown("Fire1"))
        {
            // If player holding something, player cant interact with other thing
            if (pickupController.isHolding) {
                pickupController.dropItem();
            } else {
                playerInteraction.InteractMouse();
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            playerInteraction.InteractEKey();
        }
    }

    // ************************** ANIMATION SECTION ***********************************************

    public bool IsRunning() {
        return isRunning;
    }

    public bool IsHolding() {
        return GetComponentInChildren<PickupController>().isHolding;
    }

}
