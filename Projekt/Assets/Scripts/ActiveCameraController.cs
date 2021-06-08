using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ActiveCameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private InputAction mouse;
    [SerializeField] private InputAction zoom;
    [SerializeField] private Camera cam;

    private Vector3 previousPos = new Vector3(0,0.51f,0);

    private void OnEnable()
    {
        mouse.Enable();
        zoom.Enable();
    }
    private void OnDisable()
    {
        mouse.Disable();
        zoom.Enable();
    }
    
    private float zoomAmount = -10;
    private float maxZoomIn = -5f;
    private float maxZoomOut = -15f;

    private void LateUpdate()
    {
        cam.transform.position = player.transform.position;
        cam.transform.Translate(new Vector3(0,0,Zoom()));
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            previousPos = cam.ScreenToViewportPoint(mouse.ReadValue<Vector2>());
        }
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 direction = previousPos - cam.ScreenToViewportPoint(mouse.ReadValue<Vector2>());

            //cam.transform.position = new Vector3(0,0.51f,0);
            cam.transform.position = player.transform.position;
            
            cam.transform.Rotate(new Vector3(1,0,0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0,1,0), -direction.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0,0,Zoom()));

            previousPos = cam.ScreenToViewportPoint(mouse.ReadValue<Vector2>());
        }
    }

    private float Zoom()
    {
        float zoomInput = zoom.ReadValue<float>();

        if (zoomInput > 0 && zoomAmount <= maxZoomIn)
        {
            zoomAmount = zoomAmount + zoomInput * Time.deltaTime;
        }
        else if (zoomInput < 0 && zoomAmount >= maxZoomOut)
        {
            zoomAmount = zoomAmount + zoomInput * Time.deltaTime;
        }
        
        Debug.Log(zoomAmount);
        return zoomAmount;
    }
}
