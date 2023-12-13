 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    // DataManager
    private DataManager dataManager;

    public string destinationSceneName;
    public string currentScene;
    public int difficulty = 3;

    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M)) // Change this to the desired trigger key or condition
        //{
        //SceneManager.LoadScene(destinationSceneName);
        //}
    }

    public void LoadScene(string sceneName)
    {
        print("Scene change");
        print(sceneName);
        //this.currentScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    public void SetDataRound(int roundCounter) 
    { 
        dataManager.roundCounter = roundCounter;
    }

    public void SetDataSkip(bool isSkippedTutorial) 
    { 
        dataManager.isSkippedTutorial = isSkippedTutorial;
    }

    // To avoid DatManager missing when player dies and reloads menu scene
    public void SetDifficulty(int difficulty) 
    { 
        this.difficulty = difficulty;
        dataManager.difficulty = this.difficulty;
    }

}

