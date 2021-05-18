using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private float zoomAmount;
    public float maxZoomIn = 2.5f;
    public float maxZoomOut = -7f;

    private Vector3 offset;
    private Vector3 staticOffset = new Vector3(0f,10f,-10f);
    public InputAction zoom;

    private void OnEnable()
    {
        zoom.Enable();
    }
    private void OnDisable()
    {
        zoom.Disable();
    }
    void Start()
    {
        staticOffset = player.transform.position + staticOffset;
        offset = staticOffset - player.transform.position;
    }
    void LateUpdate()
    {
        transform.position = player.transform.position + offset - HandleZoom();
    }
    Vector3 HandleZoom()
    {
        float zoomInput = zoom.ReadValue<float>();

        if (zoomInput > 0 && zoomAmount <= maxZoomIn)
        {
            //Scroll In
            zoomAmount = zoomAmount + zoomInput * Time.deltaTime;
        }else if (zoomInput < 0 && zoomAmount >= maxZoomOut)
        {
            //Scroll Out
            zoomAmount = zoomAmount + zoomInput * Time.deltaTime;
        }

        Vector3 zoomVec = new Vector3(0, zoomAmount, -zoomAmount);
        return zoomVec;
    }
}
