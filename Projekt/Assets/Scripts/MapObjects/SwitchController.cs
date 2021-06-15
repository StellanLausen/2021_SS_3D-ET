using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject switchBtnPivot;
    [SerializeField] private int id;
    private bool active = false;

    private void Start()
    {
        playerController.resetMap.AddListener(Reset);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeActive();
            EventSystem.Current.SwitchChange(id);
        }
    }
    private void ChangeActive()
    {
        switchBtnPivot.transform.Rotate(active ? new Vector3(20f, 0f, 0f) : new Vector3(-20f, 0f, 0f));
        active = !active;
    }
    private void Reset()
    {
        active = false;
    }
}
