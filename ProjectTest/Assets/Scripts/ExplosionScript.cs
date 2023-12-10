using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // How long explosion lasts
    private float explosionTime = 2f;
    private float timeLeft = 0f;
    private Vector3 scale;
    // Variable for rigid body
    private Rigidbody explosionRb;

    // Rotating
    private float rotationSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3(0.5f, 0.5f, 0.5f);
        // Set objects
        explosionRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate y-axis rotation in world space
        Quaternion yRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        // Rotate
        explosionRb.MoveRotation(explosionRb.rotation * yRotation);

        if (scale != new Vector3(4, 4, 4))
        {
            scale += new Vector3(0.1f, 0.1f, 0.1f);
            gameObject.transform.localScale = scale;
        }
        
        // Timer for how long explosion lasts
        timeLeft += Time.deltaTime;
        if (timeLeft > explosionTime)
        {
            Destroy(gameObject);
        }
    }
}
