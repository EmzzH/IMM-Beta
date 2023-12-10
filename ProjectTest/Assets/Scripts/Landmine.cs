using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grenade : MonoBehaviour
{
    // Weapons variables
    private int price = 3;
    public int localMoney;
    // DataManager
    private DataManager dataManager;
    // Player controller
    public PlayerController playerController;
    // Explosion object
    public GameObject explosion;
    // Landmine location
    Vector3 landminePosition;

    void Start()
    {
        // Get the datamanager
        playerController = FindObjectOfType<PlayerController>();
        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();
        // Decrease round count on dataManager
        dataManager.mineCount++;
    }


    void Update()
    {
        // Get landmine location
        landminePosition = transform.position;
        // Get the players money
        localMoney = dataManager.coinsCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Buying item
        if (other.CompareTag("PlayerBullet") && localMoney >= price && SceneManager.GetActiveScene().buildIndex == 2)
        {
            // Change price
            localMoney -= price;
            // Set the datamanger price
            dataManager.coinsCollected = localMoney;
            Destroy(gameObject);
            Destroy(other.gameObject);

            // Update datamanager for mine
            dataManager.hasMine = true;
        }

        // Landmine interaction in game
        if ((other.CompareTag("PlayerBullet") || other.CompareTag("Enemy") || other.CompareTag("EnemyBullet") || other.CompareTag("Explosion")) && SceneManager.GetActiveScene().buildIndex == 1) 
        {
            // Spawn explosion && destroy object
            Instantiate(explosion, landminePosition, Quaternion.identity);
            dataManager.mineCount--;
            Destroy(gameObject);
            // Destroy the player bullet
            if (other.CompareTag("PlayerBullet"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
