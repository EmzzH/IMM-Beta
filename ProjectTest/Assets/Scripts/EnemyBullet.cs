using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10.0f;

    private Rigidbody rb;

    // Game Boundary
    public float mapBoundX = 40.0f;
    public float mapBoundZ = 40.0f;

    

    private void Start()
    {
        // Get the rigid body
        rb = GetComponent<Rigidbody>();
        // Set the initial velocity in the forward direction
        rb.velocity = transform.forward * speed;
        

    }

    private void Update()
    {
        // Destroy out of bounds
        if (rb.transform.position.x > mapBoundX || rb.transform.position.x < -mapBoundX ||
            rb.transform.position.z > mapBoundZ || rb.transform.position.z < -mapBoundZ
            && gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }

    // Destroy bullets
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Cull bullets
    public void CullBullets() 
    {
        
    }
}

