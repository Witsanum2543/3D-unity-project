using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveSpeed = 1f;
    public float moveTime = 2f;
    public float minRotateTime = 1f;
    public float maxRotateTime = 3f;
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
        animator.SetBool("walk", true);

        Vector3 velocity = transform.forward * moveSpeed;

        float time = 0f;
        while (time < moveTime)
        {
            transform.position += velocity * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        animator.SetBool("walk", false);
        isMoving = false;
    }

    IEnumerator EatGrass()
    {
        isEatingGrass = true;
        animator.SetBool("eat", true);

        yield return new WaitForSeconds(eatGrassTime);

        animator.SetBool("eat", false);
        isEatingGrass = false;
    }
}
