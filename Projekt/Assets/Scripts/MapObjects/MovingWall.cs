using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerController;
    
    private readonly Vector3 startPos = new Vector3(1.5f, 2.005f, 5.5f);
    private readonly Vector3 finishPos = new Vector3(3f, 2.005f, 5.5f);

    private Boolean active = false;

    private void Start()
    {
        EventSystem.Current.ONSwitchChange += ChangeActive;
        
        
        PlayerController playerController = PlayerController.GetComponent<PlayerController>();
        playerController.resetMap.AddListener(Reset);
        
    }
    
    private void Update()
    {
        switch (active)
        {
            case true:
                transform.position = Vector3.MoveTowards(transform.position, finishPos, 1f * Time.deltaTime);
                break;
            case false:
                transform.position = Vector3.MoveTowards(transform.position, startPos, 1f * Time.deltaTime);
                break;
        }
    }
    private void ChangeActive(int id)
    {
        if (id == 1) active = !active;
        }
    private void Reset()
    {
        transform.position = startPos;
        active = false;
    }
}
