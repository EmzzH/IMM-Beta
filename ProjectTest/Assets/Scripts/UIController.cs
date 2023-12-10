using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{

    private Slider timerSlider;
   
    
    
    // Method for hiding text
    public void HideUI(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
    }

    // Method for showing text
    public void ShowUI(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
    }

    // Methods for hiding and showing canvas
    public void HideCanvas(Canvas canvas) 
    { 
        canvas.gameObject.SetActive(false);
    }

    public void ShowCanvas(Canvas canvas) 
    {
        canvas.gameObject.SetActive(true);
    }

}
