using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header ("Movement parameter")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;

    // Receive game input to handle it
    [SerializeField] private GameInput gameInput;
    // controller that move our player's character
    private CharacterController controller;

    // Animation state
    private bool isRunning;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        Move();
    }

    public void Move() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        Vector3 velocity = moveSpeed * moveDirection * Time.deltaTime ;

        if (moveDirection.magnitude >= 0.1f) {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

            controller.Move(velocity);
        }

        isRunning = moveDirection != Vector3.zero;
    }

    public bool IsRunning() {
        return isRunning;
    }
}
