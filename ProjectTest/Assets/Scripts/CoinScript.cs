using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CoinScript : MonoBehaviour
{
    // Game manager
    private GameManager gameManager;
    public float spinSpeed = 180f;
    // Set rotation of coin spawning
    private Quaternion initialRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        // Set Game Gamager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Set the initial rotation
        transform.rotation = initialRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin the coin
        transform.Rotate(Vector3.right * spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player collects coin
        if (other.CompareTag("Player"))
        {
            gameManager.UpdateCoinCollected(1);
            Destroy(gameObject);
        }
    }
}
