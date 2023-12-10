using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeadScene : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // DataManager
    private DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();

        // Set the high score
        scoreText.text = "Score: " + dataManager.highScore;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
