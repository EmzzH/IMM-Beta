
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    public Slider timerSlider; // Reference to your Slider component
    public float gameTime;

    private bool stopTimer; 

    void Start()
    {
        stopTimer = false; 
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
      Debug.Log("Update is being called");
    // ... (rest of your code)

     Debug.Log("Slider value: " + timerSlider.value);

    }

    void Update(){
        float time = gameTime - Time.time;

    if (time<=0){
        stopTimer = true; 
        timerSlider.value = time; 
    }
    }
    

}
