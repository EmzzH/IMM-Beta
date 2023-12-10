using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using TMPro;
using UnityEngine;

public class ShooterBossScript : MonoBehaviour
{
    // Variable for rigid body
    public Rigidbody enemyRb;
    // Rotating
    private float rotationSpeed = 50.0f;
    // Firing!
    private Transform player;
    private Transform firePoint;
    public GameObject enemyBullet;
    // Cooldown for shooting
    private float fireRate = 10f;
    private float reload = 1f;
    // Boss health
    private int health = 100;
    // Object for movment
    EnemyMovement movement = new EnemyMovement();
    // Game manager
    private GameManager gameManager;
    // Spawn manager
    private SpawnManager spawnManager;
    // Get fire points
    public Transform firePointFront;
    public Transform firePointBack;
    public Transform firePointLeft;
    public Transform firePointRight;

    // Start is called before the first frame update
    void Start()
    {
        // Set objects
        enemyRb = GetComponent<Rigidbody>();
        // Set the firepoint
        firePoint = transform;

        // Set Game Gamager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Set spawn manager
        spawnManager = FindObjectOfType<SpawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 33) {
            // Spin boss
            movement.MoveBossEnemy(enemyRb, rotationSpeed);
        }
        else if (health < 33) 
        {
            // Spin boss faster
            rotationSpeed = 100f;
            movement.MoveBossEnemy(enemyRb, rotationSpeed);
        }
        // if statement for firing
        if (Time.time >= reload)
        {
            Fire();
            reload = Time.time + 1f / fireRate;
        }
    }

    void Fire()
    {
        if (health <= 25)
        {
            FireBullet(firePointFront);
            FireBullet(firePointBack);
            FireBullet(firePointLeft);
            FireBullet(firePointRight);
        }
        else if (health <= 50)
        {
            FireBullet(firePointFront);
            FireBullet(firePointBack);
            FireBullet(firePointLeft);
        }
        else if (health <= 75)
        {
            FireBullet(firePointFront);
            FireBullet(firePointBack);
        }
        else if (health > 75)
        { 
            FireBullet(firePointFront);
        }
    }

    public void FireBullet(Transform firePoint) 
    {
        // Instantiate a bullet at the fire point's position and rotation
        GameObject bullet = Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
        // Add the bullets to the list in spawn manager
        spawnManager.activeBullets.Add(bullet);
    }

    private void OnTriggerEnter(Collider other)
    {

        // Enemy hit
        if (other.CompareTag("PlayerBullet") || other.CompareTag("Explosion") || other.CompareTag("Player"))
        {
            health--;
            // Change color on hit
            if (health == 0)
            {
                // Get the position of the enemy
                Vector3 enemyPositon = transform.position;
                for (int i = 0; i < 20; i++)
                {
                    // Drop coin
                    gameManager.CoinDrop(enemyPositon);
                }
                // Update Score
                gameManager.UpdateEnemiesKilled(1);
                // Set boss to dead
                gameManager.isBossDead = true;
                // Destroy the shooter enemy GameObject
                Destroy(gameObject);
                
            }
        }
    }
}
