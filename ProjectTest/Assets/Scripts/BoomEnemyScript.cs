using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemyScript : MonoBehaviour
{
    // Set variables
    private float speed = 15f;
    private float rotationSpeed = 50f;
    private float maxSpeed = 20f;
    private Rigidbody enemyRb;
    // Game manager
    private GameManager gameManager;
    // Explosion
    public GameObject explosion;
    // Get the player
    private Transform player;
    private Transform enemyLocation;
    // Keep enemy in bounds
    private float xRange = 19.5f;
    private float zRange = 19.5f;

    EnemyMovement movement = new EnemyMovement();

    // Death animation
    public bool isDead;
    public float deadTime = 3.0f;
    Animator animator;

    void Start()
    {
        // Set the animator
        animator = GetComponent<Animator>();
        // Set objects
        enemyRb = GetComponent<Rigidbody>();
        // Set Game Gamager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Set the player
        player = GameObject.Find("Player").transform;
        // Set look direction
        enemyLocation = transform;
    }

    
    void Update()
    {
        // Keep enemy in boundaries
        EnemyBoundaries(transform.position);
        if (!isDead)
        {
            // Calculate player direction
            Vector3 playerDirection = player.position - transform.position;
            // Set the enemy to look at the player
            enemyLocation.forward = playerDirection.normalized;
            movement.MoveEnemy(enemyRb, speed, rotationSpeed, maxSpeed);
        }
        if (isDead)
        {
            enemyRb.velocity = new Vector3(0, 0, 0);
            deadTime -= Time.deltaTime;
            DeathAnnimation();
        }
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
    public void DeathAnnimation()
    {
        if (deadTime <= 1)
        {
            // Update Score
            gameManager.UpdateEnemiesKilled(1);
            // Get the position of the enemy
            Vector3 enemyPositon = transform.position;
            // Drop coin
            gameManager.CoinDrop(enemyPositon);
            Instantiate(explosion, enemyPositon, Quaternion.identity);
            // Destroy the enemy GameObject
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("Explosion") || other.CompareTag("Player"))
        {
            // Set as dead
            isDead = true;
            animator.SetBool("isMoving", false);
            animator.Play("Death");
            animator.SetBool("Dead", true);
        }
    }
}
