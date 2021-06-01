using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    
    public Material[] material;
    public GameObject PlayerController;
    Renderer rend;

    private Boolean active = false;
    
    void Start()
    {
        PlayerController playerController = PlayerController.GetComponent<PlayerController>();
        playerController.movingWallChanged.AddListener(changeActive);
        playerController.resetMap.AddListener(reset);
        
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }
    
    private void changeActive()
    {
        active = !active;
        if (active)
        {
            rend.sharedMaterial = material[1];
        }
        else
        {
            rend.sharedMaterial = material[0];
        }
    }

    private void reset()
    {
        active = false;
        rend.sharedMaterial = material[0];
    }
}
