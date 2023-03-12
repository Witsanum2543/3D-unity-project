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

    // controller that move our player's character
    private CharacterController characterController;
    // Interaction components
    PlayerInteraction playerInteraction;

    [Header ("Pickup component")]
    [SerializeField] public GameObject pickupPivot;
    [SerializeField] public PickupController pickupController;


    // Animation state
    private bool isRunning = false;

    private void Start() {
        characterController = GetComponent<CharacterController>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

    private void Update() {
        Move();
        Interact();
    }

    public void Move() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        // handleGravity(moveDirection); 

        Vector3 velocity = moveSpeed * moveDirection * Time.deltaTime ;

        if (moveDirection.magnitude >= 0.1f) {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

            characterController.Move(velocity);
        }

        isRunning = moveDirection != Vector3.zero;
    }

    void handleGravity(Vector3 moveDirection) {
        if (characterController.isGrounded) {
            Debug.Log("GDS");
            float groundedGravity = -.05f;
            moveDirection.y = groundedGravity;
        } else {
            // Debug.Log("123");
            float gravity = -9.8f;
            moveDirection.y = gravity;
        }
    }

    public void Interact() {
        if (Input.GetButtonDown("Fire1"))
        {
            // If player holding something, player cant interact with other thing
            if (pickupController.isHolding) {
                pickupController.dropItem();
            } else {
                playerInteraction.Interact();
            }
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
