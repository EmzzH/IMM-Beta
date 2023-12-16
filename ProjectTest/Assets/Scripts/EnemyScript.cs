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
    public bool isDead;
    public float deadTime = 3.0f;
    // Game manager
    private GameManager gameManager;
    // Get the player
    private Transform player;
    private Transform enemyLocation;
    // Keep enemy in bounds
    private float xRange = 19.5f;
    private float zRange = 19.5f;

    // Get the enemy movement
    EnemyMovement movement = new EnemyMovement();
    // Get the animator
    Animator animator;

    // Sound
    private AudioSource spiderAudio;
    public AudioClip squishAudio;

    void Start()
    {
        // Set the animator
        animator = GetComponent<Animator>();
        // Set objects
        enemyRb = GetComponent<Rigidbody>();
        // Set the player
        player = GameObject.Find("Player").transform;
        // Set look direction
        enemyLocation = transform;
        // Set Game Gamager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Set the spiderAudio
        spiderAudio = GetComponent<AudioSource>();
        // Play the spider audio
        spiderAudio.Play();
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
        // Spider dies
        if (isDead) 
        {
            enemyRb.velocity = new Vector3(0, 0, 0);
            deadTime -= Time.deltaTime;
            DeathAnnimation();
        }
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

    public void DeathAnnimation() 
    {
        if (deadTime <= 1) 
        {
            // Get the position of the enemy
            Vector3 enemyPosition = transform.position;
            // Drop coin
            gameManager.CoinDrop(enemyPosition);
            // Update Score
            gameManager.UpdateEnemiesKilled(1);
            // Destroy the enemy GameObject
            Destroy(gameObject);
        }
    
    }

    // For audio
    public void AudioSetup() 
    {
        spiderAudio.Pause();
        spiderAudio.PlayOneShot(squishAudio, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("Explosion") || other.CompareTag("Player"))
        {
            // Sounds
            AudioSetup();
            // Set as dead
            isDead = true;
            animator.SetBool("isMoving", false);
            animator.Play("Death");
            animator.SetBool("Dead", true);
        }
    }
}
