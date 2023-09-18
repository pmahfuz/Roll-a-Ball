using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Speed at which the enemy moves
    public float changeDirectionTime = 2.0f; // Time interval to change direction

    private Vector3 moveDirection;

    void Start()
    {
        // Set an initial random movement direction
        moveDirection = GetRandomDirection().normalized;
        InvokeRepeating("ChangeDirection", 0.0f, changeDirectionTime);
    }

    void Update()
    {
        // Move the enemy
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + moveDirection * moveSpeed * Time.deltaTime;

        // Check if the new position is within bounds
        if (IsPositionValid(newPosition))
        {
            transform.position = newPosition;
        }
        else
        {
            // If hitting a wall, change direction
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        // Generate a new random movement direction
        moveDirection = GetRandomDirection().normalized;
    }

    Vector3 GetRandomDirection()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0; // Set y-component to zero to ensure movement in x and z only
        return randomDirection;
    }

    bool IsPositionValid(Vector3 position)
    {
        // Check if the position is within your defined bounds
        // For example, assuming your play area is from (-10, 0, -10) to (10, 0, 10)
        return position.x >= -9.5 && position.x <= 9.5 && position.z >= -9.5 && position.z <= 9.5;
    }
}