using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchMaterial : MonoBehaviour
{
    //Reference
    [SerializeField] private Material[] material;
    
    private Renderer rend;
    private bool active;
    
    private void Awake()
    {
        active = false;

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }
    public void ChangeActive()
    {
        rend.sharedMaterial = active ? material[0] : material[1];
        active = !active;
    }
}
