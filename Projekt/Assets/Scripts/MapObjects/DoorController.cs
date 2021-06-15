using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft;
    [SerializeField] private GameObject doorRight;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.impulse);
        if (other.impulse.x < -4)
        {
        doorLeft.transform.rotation = Quaternion.Euler(0,-90,0);
        doorRight.transform.rotation = Quaternion.Euler(0,90,0);
        }
    }
}
