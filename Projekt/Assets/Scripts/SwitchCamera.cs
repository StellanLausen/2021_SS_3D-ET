using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private InputAction switchCam;
    [SerializeField] private CameraController staticCamCon;
    [SerializeField] private ActiveCameraController activeCamCon;
    
    private bool activeCam = true;
    private void OnEnable()
    {
        switchCam.Enable();
    }
    private void OnDisable()
    {
        switchCam.Disable();
    }
    private void Start()
    {
        staticCamCon.enabled = activeCam;
        activeCamCon.enabled = !activeCam;
    }
    private void Update()
    {
        if (Keyboard.current.cKey.wasReleasedThisFrame)
        {
            activeCam = !activeCam;
            staticCamCon.enabled = activeCam;
            activeCamCon.enabled = !activeCam;
        }
    }
}
