using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    // Weapons variables
    private int price = 3;
    private float fireRate = 0.5f;
    public int localMoney;

    // DataManager
    private DataManager dataManager;
    // Player controller
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // Get the datamanager
        playerController = FindObjectOfType<PlayerController>();
        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        localMoney = dataManager.coinsCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Buying shotgun
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
            dataManager.initialAmmunition = 5;
            dataManager.initialMagazine = 5;
            dataManager.ammunition = 5;
            dataManager.reloadTime = 1;
            dataManager.fireRate = fireRate;
            playerController.playerWeapon = "shotgun";
            dataManager.playerWeapon = "shotgun";
            playerController.WeaponCheck();
        }
    }
}
