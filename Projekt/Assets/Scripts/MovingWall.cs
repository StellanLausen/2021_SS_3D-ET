using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerController;
    
    private Vector3 startPos = new Vector3(1.5f, 2.005f, 5.5f);
    private Vector3 finishPos = new Vector3(3f, 2.005f, 5.5f);

    private Boolean active = false;

    private void Start()
    {
        EventSystem.current.onSwitchChange += ChangeActive;
        
        
        PlayerController playerController = PlayerController.GetComponent<PlayerController>();
        playerController.resetMap.AddListener(Reset);
        
    }
    
    private void Update()
    {
        if (active)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPos, 1f * Time.deltaTime);
        }

        if (!active)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 1f * Time.deltaTime);
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
