using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header ("Movement parameter")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private GameInput gameInput;

    // Animation state
    private bool isRunning;

    private void Start() {

    }

    private void Update() {
        Move();
    }

    public void Move() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        isRunning = moveDirection != Vector3.zero;
    }

    public bool IsRunning() {
        return isRunning;
    }
}
