using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private GameObject destroyableCube;
    private void Start()
    {
        for (int i = 0; i <= 7; i++)
        {
            destroyableCube.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            ActivateObj();
            DestroyObj();
        }
    }

    private void DestroyObj()
    { 
        Destroy(gameObject);
    }

    private void ActivateObj()
    {
        for (int i = 0; i <= 7; i++)
        {
            destroyableCube.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
