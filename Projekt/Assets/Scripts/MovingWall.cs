using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{

    public GameObject PlayerController;
    
    private Vector3 startPos = new Vector3(1.5f, 2.005f, 5.5f);
    private Vector3 rightPos = new Vector3(3f, 2.005f, 5.5f);

    private Boolean active = false;

    void Start()
    {
        PlayerController playerController = PlayerController.GetComponent<PlayerController>();
        playerController.movingWallChanged.AddListener(changeActive);
        playerController.resetMap.AddListener(reset);
    }
    
    void Update()
    {
        if (active)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPos, 1f * Time.deltaTime);
        }

        if (!active)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 1f * Time.deltaTime);
        }
    }

    private void changeActive()
    {
        active = !active;
    }

    private void reset()
    {
        transform.position = startPos;
        active = false;
    }
}
