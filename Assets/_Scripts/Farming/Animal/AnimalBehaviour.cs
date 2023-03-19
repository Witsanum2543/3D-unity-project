using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float movementSpeed = 10f;
    public float minRotateTime = 1f;
    public float maxRotateTime = 3f;
    public float minMoveTime = 1f;
    public float maxMoveTime = 3f;
    public float eatGrassTime = 3f;
    public Animator animator;

    private bool isRotating = false;
    private bool isMoving = false;
    private bool isEatingGrass = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            Update();
            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds before the next update
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRotating && !isMoving && !isEatingGrass)
        {
            float randomValue = Random.value;
            if (randomValue < 0.33f)
            {
                StartCoroutine(Rotate());
            }
            else if (randomValue < 0.67f)
            {
                StartCoroutine(Move());
            }
            else
            {
                StartCoroutine(EatGrass());
            }
        }
    }

    IEnumerator Rotate()
    {
        isRotating = true;

        float rotateTime = Random.Range(minRotateTime, maxRotateTime);
        float rotateAngle = Random.Range(-180f, 180f);

        Quaternion fromRotation = transform.rotation;
        Quaternion toRotation = Quaternion.Euler(transform.eulerAngles + Vector3.up * rotateAngle);

        float elapsed = 0f;
        while (elapsed < rotateTime)
        {
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, elapsed / rotateTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = toRotation;
        isRotating = false;
    }

    IEnumerator Move()
    {
        isMoving = true;

        float moveTime = Random.Range(minMoveTime, maxMoveTime);
        float moveForce = Random.Range(10f, 20f);

        rb.AddForce(transform.forward * moveForce, ForceMode.VelocityChange);

        float elapsed = 0f;
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        isMoving = false;
    }

    IEnumerator EatGrass()
    {
        isEatingGrass = true;

        // animator.SetTrigger("EatGrass");

        yield return new WaitForSeconds(eatGrassTime);

        // animator.SetTrigger("StopEatingGrass");
        isEatingGrass = false;
    }
}
