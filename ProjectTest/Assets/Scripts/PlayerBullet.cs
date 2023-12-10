using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 20.0f;
    private Rigidbody rb;

    // Game Boundary
    public float mapBoundX = 40.0f;
    public float mapBoundZ = 40.0f;


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
        if (rb.transform.position.x > mapBoundX || rb.transform.position.x < -mapBoundX ||
            rb.transform.position.z > mapBoundZ || rb.transform.position.z < -mapBoundZ
            && gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            // Destroy the shooter enemy GameObject
            Destroy(gameObject);
        }

        // Shop object being destroyed
        if (other.CompareTag("ShopObject"))
        {
            Destroy(gameObject);
        }
        //Exit and Load new Scene
        if (other.CompareTag("Exit"))
        {
            Destroy(gameObject);
            //Load the Game Again
            SceneManager.LoadScene(1);
        }
    }
}
