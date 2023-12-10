using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject player;

     // Shop Prefabs
    public GameObject[] shopPrefabs; 
    private float shopSpawnPosX = 4.0f;
    private float shopSpawnPosZ = 3.0f;
    // y position
    private float yPos = 1;
    // DataManager
    private DataManager dataManager;
    // Shop UI
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI playerHealthText;


    // Start is called before the first frame update
    void Start()
    {
        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();

        dataManager.ammunition = dataManager.initialAmmunition;
        // Display for round 1 shop tutoiral screen
        if (dataManager.roundCounter == 2) 
        {
            tutorialText.text = "Welcome to the shop, shoot items to buy. Shoot the exit to leave.";
        }
        //Add Shop Prefab Items        
        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                Vector3 tempSpawnPos = new Vector3(shopSpawnPosX, yPos, shopSpawnPosZ);
                Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                
            }
            else if (i == 1)
            {
                Vector3 tempSpawnPos = new Vector3(-shopSpawnPosX, yPos, -shopSpawnPosZ);
                Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                
            }
            else if (i == 2)
            {
                Vector3 tempSpawnPos = new Vector3(-shopSpawnPosX, yPos, shopSpawnPosZ);
                Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                
            }
            else if (i == 3)
            {
                Vector3 tempSpawnPos = new Vector3(shopSpawnPosX, yPos, -shopSpawnPosZ);
                Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
               
            }
           
        }

    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "" + dataManager.coinsCollected;
        playerHealthText.text = "Health: " + dataManager.playerHealth;
    }
}
