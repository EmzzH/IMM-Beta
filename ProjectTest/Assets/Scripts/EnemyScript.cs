using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyScript : MonoBehaviour
{
    // Set variables
    private float speed = 15f;
    private float rotationSpeed = 50f;
    private float maxSpeed = 20f;
    private Rigidbody enemyRb;
    private float distance = 0f;
    // Game manager
    private GameManager gameManager;
    // Get the player
    private Transform player;
    private Transform enemyLocation;
    // Keep enemy in bounds
    private float xRange = 19.5f;
    private float zRange = 19.5f;


    EnemyMovement movement = new EnemyMovement();

    void Start()
    {

        // Set objects
        enemyRb = GetComponent<Rigidbody>();
        // Set the player
        player = GameObject.Find("Player").transform;
        // Set look direction
        enemyLocation = transform;
        // Set Game Gamager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    
    void Update()
    {
        // Keep enemy in boundaries
        EnemyBoundaries(transform.position);
        // Calculate player direction
        Vector3 playerDirection = player.position - transform.position;
        // Set the enemy to look at the player
        enemyLocation.forward = playerDirection.normalized;
        movement.MoveEnemy(enemyRb, speed, rotationSpeed, maxSpeed);
    }

    // Keeep enemies in bounds
    public void EnemyBoundaries(Vector3 playerPosition)
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("Explosion") || other.CompareTag("Player"))
        {
            // Get the position of the enemy
            Vector3 enemyPosition = transform.position;
            // Drop coin
            gameManager.CoinDrop(enemyPosition);

            // Destroy the shooter enemy GameObject
            Destroy(gameObject);
            // Update Score
            gameManager.UpdateEnemiesKilled(1);
        }
    }
}
