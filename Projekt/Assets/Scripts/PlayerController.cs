using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public class IntEvent : UnityEvent<int> {}
    
    [SerializeField] private GameObject resetCamObj;
    private ResetCameraController resetCamContr;
    [SerializeField] private Transform mainCamera;

    //[SerializeField]
    //private GameObject gameManagerObj;
    private GameManager gameManager;

    //OnCollisionStay is another speed = 150
    [SerializeField] private float speed = 250;
    [SerializeField] private float jump = 300;
    [SerializeField] private float gameOverHeight = 0f;

    //Jump
    private bool shouldJump;
    private float collectedJumpForce;
    private float jumpForce = 0;
    
    private Vector3 startPosVec;
    private Rigidbody rb;

    private LayerMask groundLayerMask;

    public InputAction move;
    
    //Events
    public UnityEvent LevelFinished = new UnityEvent();
    public UnityEvent resetMap = new UnityEvent();
    public IntEvent gravityChange = new IntEvent();
    
    private void OnEnable()
    {
        move.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
    }
    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        resetCamContr = resetCamObj.GetComponent<ResetCameraController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        
        rb = GetComponent<Rigidbody>();
        startPosVec = new Vector3(0,0.5f,0);
    }
    void Update()
    {
        if (rb.transform.position.y <= gameOverHeight)
        {
            GameOver();
        }

        //jump
        Vector3 rayCastDirectrion = new Vector3(0,-1,0);
        if (Physics.Raycast(transform.position, rayCastDirectrion, 1f, groundLayerMask))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        Vector3 camF = mainCamera.forward;
        Vector3 camR = mainCamera.right;

        camF.y = 0;
        camR.y = 0;

        camF = camF.normalized;
        camR = camR.normalized;

        //control
        Vector2 movement = move.ReadValue<Vector2>();
        Vector3 Vec = new Vector3(movement.x, 0, movement.y);

        Vector3 x = camF * movement.y;
        Vector3 y = camR * movement.x;

        Vector3 dir = x + y;

        rb.AddForce(speed * dir.x * Time.deltaTime, 0, speed * dir.z * Time.deltaTime);
        //rb.AddForce(speed * Time.deltaTime * movement.x, 0, speed * Time.deltaTime * movement.y);

        //jump
        if (shouldJump)
        {
            rb.AddForce(0, jump + collectedJumpForce, 0);
            collectedJumpForce = 0;
            shouldJump = false;
        }
    }
    private void OnCollisionStay(Collision other)  
    {
        GameObject collisionStay = other.gameObject;
        
        if (collisionStay.CompareTag("Finish"))
        {
            Finished();
        }

        if (collisionStay.CompareTag("Slow"))
        {
            speed = 125;
        }
        else
        {
            speed = 250;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject triggerEnter = other.gameObject;
        
        if (triggerEnter.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            AddPoint();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        GameObject collisionEnter = other.gameObject;
        
        if (collisionEnter.CompareTag("leftGravity"))
        {
            gravityChange.Invoke(1);
        }

        if (collisionEnter.CompareTag("bottomGravity"))
        {
            gravityChange.Invoke(2);
        }

        if (collisionEnter.CompareTag("Trap"))
        {
            GameOver();
        }

        if (collisionEnter.CompareTag("Enemy"))
        {
            GameOver();
        }
    }
    private void Jump()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            jumpForce = jumpForce + 0.15f;
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            shouldJump = true;
            if (jumpForce < 20)
            {
                jumpForce = 0;
            }
            else if (jumpForce > 50)
            {
                jumpForce = 50;
            }
            collectedJumpForce = jumpForce;
            jumpForce = 0;
        }
    }
    private void GameOver()
    {
            transform.position = startPosVec;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            
            Debug.Log("Leider verloren");
            //ResetMap
            resetMap.Invoke();
            //ResetCam
            StartCoroutine(resetCamContr.PlayResetCam());
            
            //Delete one Live
            gameManager.RemoveLive();
    }
    private void Finished()
    {
        Debug.Log("Ziel erreicht");
        
        transform.position = startPosVec;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //ResetCam
        StartCoroutine(resetCamContr.GetComponent<ResetCameraController>().PlayResetCam());
        
        //Finish
        LevelFinished.Invoke();
    }
    private void AddPoint()
    {
        gameManager.AddPoint();
    }
}
