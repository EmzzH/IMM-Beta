using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EnemyMovement
{
    public void MoveEnemy(Rigidbody enemyRb, float speed, float rotationSpeed, float maxSpeed)
    {

        // Set player object
        GameObject player = GameObject.Find("Player");
        
        // Create Vector3 for recording distance
        Vector3 playerDistance = player.transform.position - enemyRb.transform.position;

        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = player.transform.position - enemyRb.transform.position;
        // Rotate them
        enemyRb.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(enemyRb.transform.forward, lookDirection, rotationSpeed * Time.deltaTime, 0.0f));

        // Add the force
        enemyRb.AddForce(lookDirection * (speed * Time.deltaTime));

            // Speed limit for enemies
            if (enemyRb.velocity.magnitude > maxSpeed)
            {
            // enemyRb.velocity = enemyRb.velocity.normalized * maxSpeed;
            enemyRb.velocity = enemyRb.velocity.normalized * maxSpeed;
        }
    }

    public void MoveShooterEnemy(Rigidbody enemyRb, float speed, float maxSpeed, float radius) 
    {
        // Set player object
        GameObject player = GameObject.Find("Player");

        // Calculate the direction to the player
        Vector3 directionToPlayer = player.transform.position - enemyRb.transform.position;

        // Calculate the distance to the player
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if the enemy is outside the desired radius
        if (distanceToPlayer > radius)
        {
            // Normalize the direction vector
            directionToPlayer.Normalize();

            // Move the enemy toward the player
            enemyRb.AddForce(directionToPlayer * speed * Time.deltaTime);

            // Speed limit for enemies
            if (enemyRb.velocity.magnitude > maxSpeed)
            {
                enemyRb.velocity = enemyRb.velocity.normalized * maxSpeed;
            }
        }
        else 
        {
            // Enemy goes backwards if plater gets too close
            Vector3 backwardVelocity = new Vector3(0.0f, 0.0f, -3.0f);
            enemyRb.velocity = backwardVelocity;
        }
    }

    public void MoveBossEnemy(Rigidbody enemyRb, float rotationSpeed)
    {
        // Calculate y-axis rotation in world space
        Quaternion yRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        // Rotate
        enemyRb.MoveRotation(enemyRb.rotation * yRotation);
    }
}

