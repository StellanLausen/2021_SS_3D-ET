using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private InputAction mouse;
    [SerializeField] private InputAction zoom;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform camTrans;

    private Vector3 previousPos = new Vector3(0,0.51f,0);
    private bool activeCam = true;

    private float zoomAmount = -10f;
    private const float MAXZoomIn = -5f;
    private const float MAXZoomOut = -15f;

    private void OnEnable()
    {
        mouse.Enable();
        zoom.Enable();
    }
    private void OnDisable()
    {
        mouse.Disable();
        zoom.Disable();
    }
    private void LateUpdate()
    {
        if (Keyboard.current.cKey.wasReleasedThisFrame) activeCam = !activeCam;

        if (activeCam)
        {
            StaticCam();
        }
        else
        {
            MoveCam();
        }
    }
    private void StaticCam()
    {
        //reset angle
        camTrans.eulerAngles = new Vector3(45f, 0, 0);
        //set camera pos
        camTrans.position = player.transform.position;
        camTrans.Translate(new Vector3(0,0,HandleZoom()));
    }
    private void MoveCam()
    {
        camTrans.position = player.transform.position;
        camTrans.Translate(new Vector3(0,0,HandleZoom()));
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
               previousPos = cam.ScreenToViewportPoint(mouse.ReadValue<Vector2>());
        }
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 direction = previousPos - cam.ScreenToViewportPoint(mouse.ReadValue<Vector2>());
            
            camTrans.position = player.transform.position;
            
            camTrans.Rotate(new Vector3(1,0,0), direction.y * 180);
            camTrans.Rotate(new Vector3(0,1,0), -direction.x * 180, Space.World);
            camTrans.Translate(new Vector3(0,0,HandleZoom()));

            previousPos = cam.ScreenToViewportPoint(mouse.ReadValue<Vector2>());
        }
    }
    private float HandleZoom()
    {
        float zoomInput = zoom.ReadValue<float>();

        if (zoomInput > 0 && zoomAmount <= MAXZoomIn)
        {
            zoomAmount = zoomAmount + zoomInput * Time.deltaTime;
        }
        else if (zoomInput < 0 && zoomAmount >= MAXZoomOut)
        {
            zoomAmount = zoomAmount + zoomInput * Time.deltaTime;
        }

        ActiveCameraController.zoomAmount = zoomAmount;
        return zoomAmount;
    }
}