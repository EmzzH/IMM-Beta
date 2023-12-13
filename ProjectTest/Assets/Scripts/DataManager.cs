using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // tutorial: https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#634f8281edbc2a65c86270ca
    public static DataManager Instance;
    // Declare variables
    public int enemiesKilled;
    public int coinsCollected;
    public int roundCounter;
    public int playerHealth;
    public bool isSkippedTutorial;
    public int highScore;
    public float roundTime;
    // Difficulty setting 1 - Easy, 2 - Med, 3 - hard
    public int difficulty = 2;
    // Weapon data
    public int initialMagazine;
    public int initialAmmunition;

    public int magazineSize;
    public int ammunition;
    
    public float fireRate;
    public float reloadTime;

    public string playerWeapon;

    public bool hasMine;
    public int maxMines = 3;
    public int mineCount = 0;
    public float mineLayTime = 2f;

    void Start()
    {
        // Resets the weapon when object loads - eg. player dies
        ResetWeapon();
    }

    private void Awake()
    {
        // Keep the datamanager alive through rounds
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Resets weapon variable back to pistol
    public void ResetWeapon()
    {
        playerWeapon = "pistol";
        initialAmmunition = 10;
        ammunition = 10;

        reloadTime = 2f;

        fireRate = 0.3f;
    }

    // Saves the round data at crucial points
    public void SaveData(int enemiesKilled, int coinsCollected, int roundCounter, int playerHealth)
    {
        DataManager.Instance.enemiesKilled = enemiesKilled;
        DataManager.Instance.coinsCollected = coinsCollected;
        DataManager.Instance.roundCounter = roundCounter;
        DataManager.Instance.playerHealth = playerHealth;
        // Calculate the score for rounds outside of the tutorial
        if (roundCounter > 3)
        {
            highScore = (enemiesKilled * roundCounter) * difficulty;
        }
    }

    // Saves the time that will increase
    public void SaveTime(float roundTime) 
    {
        DataManager.Instance.roundTime = roundTime;
    }

    // For when the player skips the tutorial
    public void SetSkippedTutorial(bool skippedTutorial)
    {
        this.isSkippedTutorial = skippedTutorial;
    }

    // Get the skipped tutorial bool
    public bool GetSkippedTutorial()
    {
        return this.isSkippedTutorial;
    }

    // Sets the round counter
    public void SetRound(int roundCounter)
    {
        this.roundCounter = roundCounter;
    }

    // Set difficulty data
    public void setDifficulty(int difficulty) 
    { 
        this.difficulty = difficulty;
    }
}


