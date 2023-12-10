using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketScript : MonoBehaviour
{
    // Rocket variables
    public float speed = 30.0f;
    private Rigidbody rb;

    // Timers for exploding
    private float detTimer = 0f;
    private float explosionTimer = 1.0f;

    public GameObject explosion;
    Vector3 rocketPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigid body
        rb = GetComponent<Rigidbody>();
        // Set the initial velocity in the forward direction
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Get position
        rocketPosition = transform.position;

        detTimer += Time.deltaTime;

        if (detTimer > explosionTimer ) 
        {
            Instantiate(explosion, rocketPosition, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            // Spawn explosion && destroy object
            Instantiate(explosion, rocketPosition, Quaternion.identity);
            Destroy(gameObject);
        }

        // Shop object being destroyed
        if (other.CompareTag("ShopObject"))
        {
            // Spawn explosion && destroy object
            Instantiate(explosion, rocketPosition, Quaternion.identity);
            Destroy(gameObject);
        }
        //Exit and Load new Scene
        if (other.CompareTag("Exit"))
        {
            // Spawn explosion && destroy object
            Instantiate(explosion, rocketPosition, Quaternion.identity);
            Destroy(gameObject);
            //Load the Game Again
            SceneManager.LoadScene(1);
        }
    }
}
