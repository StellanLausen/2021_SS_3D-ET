using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class SwitchController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerControllerObj;
    [SerializeField]
    private GameObject switchBtnPivot;
    [SerializeField]
    private int id;
    private bool active = false;

    private void Start()
    {
        PlayerController PlayerController = playerControllerObj.GetComponent<PlayerController>();
        PlayerController.resetMap.AddListener(Reset);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeActive();
            EventSystem.current.switchChange(id);
        }
    }
    private void ChangeActive()
    {
        if (active)
        {
            switchBtnPivot.transform.Rotate(new Vector3(20f, 0f, 0f));
        }
        else
        {
            switchBtnPivot.transform.Rotate(new Vector3(-20f, 0f, 0f));
        }
        active = !active;
    }
    private void Reset()
    {
        active = false;
    }
}
