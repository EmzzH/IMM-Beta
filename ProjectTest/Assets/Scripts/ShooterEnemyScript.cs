using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ShooterEnemyScript : MonoBehaviour
{
    // Set variables for movment
    private float speed = 40f;
    private float maxSpeed = 50f;
    private float radius = 10f;

    // Variable for rigid body
    public Rigidbody enemyRb;

    // Object for movment
    EnemyMovement movement = new EnemyMovement();
    // Looking at player
    private Transform enemyLocation;
    // Firing!
    private Transform player;
    private Transform firePoint;
    public GameObject enemyBullet;
    // Cooldown for shooting
    private float fireRate = 1f;
    private float reload = 1f;

    // Game manager
    private GameManager gameManager;
    // Spawn manager
    private SpawnManager spawnManager;
    // Keep enemy in bounds
    private float xRange = 19.5f;
    private float zRange = 19.5f;

    void Start()
    {
        // Set objects
        enemyRb = GetComponent<Rigidbody>();
        // Set the firepoint
        firePoint = transform;
        // Set the player
        player = GameObject.Find("Player").transform;
        // Set Game Gamager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Set spawn manager
        spawnManager = FindObjectOfType<SpawnManager>();
        // Set the look direction
        enemyLocation = transform;
    }


    void Update()
    {
        // Keep enemy in boundaries
        EnemyBoundaries(transform.position);
        // Calculate player direction
        Vector3 playerDirection = player.position - transform.position;
        // Set the enemy to look at the player
        enemyLocation.forward = playerDirection.normalized;

        movement.MoveShooterEnemy(enemyRb, speed, maxSpeed, radius);
        // if statement for firing
        if (Time.time >= reload)
        {
            Fire();
            reload = Time.time + 1f/fireRate;
        }
    }

    void Fire() 
    {
        // Calculate player direction
        Vector3 fireDirection = player.position - firePoint.position;

        // Rotate the firePoint to face the player
        firePoint.forward = fireDirection.normalized;

        // Instantiate a bullet at the fire point's position and rotation
        GameObject bullet = Instantiate(enemyBullet, firePoint.position, Quaternion.LookRotation(fireDirection));

        // Add the bullets to the list in spawn manager
        spawnManager.activeBullets.Add(bullet);
    }

    // Keep enemy in bounds
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
        // Enemy die
        if (other.CompareTag("PlayerBullet") || other.CompareTag("Explosion") || other.CompareTag("Player"))
        {
            // Get the position of the enemy
            Vector3 enemyPositon = transform.position;
            // Drop coin
            gameManager.CoinDrop(enemyPositon);
            // Update Score
            gameManager.UpdateEnemiesKilled(1);
            // Destroy the shooter enemy GameObject
            Destroy(gameObject);
            
        }
    }
}
