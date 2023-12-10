using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Game object for player
    public GameObject player;
    // Game object for shop
    // public GameObject shop;
    // Offset for camera
    private Vector3 offset = new Vector3(0,20,0);
    // Bool for shop
    public bool isInShop = false;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (isInShop == false)
        {
            AbovePlayer();
        }
        else
        {
            MoveCamera();
        }
    }

    public void AbovePlayer() 
    {
        // Offset camera above player
        transform.position = player.transform.position + offset;

    }

    public void MoveCamera()
    {
        // Offset camera above shop
        //transform.position = shop.transform.position + offset;
        transform.position = player.transform.position + offset;
    }
}
