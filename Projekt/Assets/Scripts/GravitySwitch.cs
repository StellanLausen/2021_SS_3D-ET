using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public PlayerController playerController;
    
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.gravityChange.AddListener(changeGravity);
    }

    private void changeGravity(int id)
    {
        switch (id)
        {
            case 1 :
                Physics.gravity = new Vector3(-9.8f, 0, 0);
                break;
            case 2 :
                Physics.gravity = new Vector3(+9.8f, 0, 0);
                break;
        }
    }
}
