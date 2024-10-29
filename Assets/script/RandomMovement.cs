using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementWithRotation : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float directionChangeInterval = 2f;
    private Vector3 movementDirection;

    void Start()
    {
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        MoveInRandomDirection();
        RotateTowardsMovementDirection();
    }

    void MoveInRandomDirection()
    {
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(directionChangeInterval);
            movementDirection = GetRandomDirection();
        }
    }

    Vector3 GetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        return new Vector3(randomX, 0f, randomZ).normalized;
    }

    void RotateTowardsMovementDirection()
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
