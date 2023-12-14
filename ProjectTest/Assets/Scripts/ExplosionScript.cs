using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    
    // How long explosion lasts
    private float explosionTime = 0.3f;
    private float timeLeft = 0f;
    

    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime;
        if (timeLeft > explosionTime)
        {
            Destroy(gameObject);
        }
    }
    
}
