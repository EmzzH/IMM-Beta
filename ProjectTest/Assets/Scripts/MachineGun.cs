using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    // Weapons variables
    private int price = 5;
    private float fireRate = 0.1f;
    public int localMoney;
    // DataManager
    private DataManager dataManager;
    // Player controller
    public PlayerController playerController;

    void Start()
    {
        // Get the datamanager
        playerController = FindObjectOfType<PlayerController>();
        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();
    }


    void Update()
    {
        // Get the players money
        localMoney = dataManager.coinsCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Buying item
        if (localMoney >= price)
        {
            // Change price
            localMoney -= price;
            // Set the datamanger price
            dataManager.coinsCollected = localMoney;
            Destroy(gameObject);
            // Destroy the player bullet
            if (other.CompareTag("PlayerBullet"))
            {
                Destroy(other.gameObject);
            }

            // Update data manager for weapon
            dataManager.initialAmmunition = 25;
            dataManager.initialMagazine = 25;
            dataManager.ammunition = 25;
            dataManager.reloadTime = 5;
            dataManager.fireRate = fireRate;
            playerController.playerWeapon = "machinegun";
            dataManager.playerWeapon = "machinegun";
            playerController.WeaponCheck();
        }
    }
}
