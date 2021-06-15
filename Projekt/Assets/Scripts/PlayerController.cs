using System;
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
    public UnityEvent levelFinished = new UnityEvent();
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
    private void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        resetCamContr = resetCamObj.GetComponent<ResetCameraController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        rb = GetComponent<Rigidbody>();
        startPosVec = transform.position;
    }
    private void Update()
    {
        if (rb.transform.position.y <= gameOverHeight)
        {
            GameOver();
        }

        //jump
        var rayCastDirectrion = new Vector3(0,-1,0);
        if (Physics.Raycast(transform.position, rayCastDirectrion, 1f, groundLayerMask))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        Vector3 camY = mainCamera.forward;
        Vector3 camX = mainCamera.right;
        
        camY.y = 0;
        camX.y = 0;
        
        //Vector shows only direction without y value:
        camY = camY.normalized;
        camX = camX.normalized;

        //Read wasd into movement
        Vector2 movement = move.ReadValue<Vector2>();

        //if movement multiply it with direction for x and y
        Vector3 y = camY * movement.y;
        Vector3 x = camX * movement.x;

        /*
        if "w" and "d" pressed:
        
        camForward =    0.7,0,0.7
        camRight =      0.7,0,-0.7 
        
        moveDir =       1,0,0
        */

        Vector3 moveDir = x + y;
        rb.AddForce(speed * moveDir.x * Time.deltaTime, 0, speed * moveDir.z * Time.deltaTime);

        //jump
        if (shouldJump)
        {
            rb.AddForce(0, jump + collectedJumpForce, 0);
            collectedJumpForce = 0;
            shouldJump = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            AddPoint();
        }

        if (other.CompareTag("Finish"))
        {
            Finished();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        GameObject collisionEnter = other.gameObject;

        if (collisionEnter.CompareTag("Trap"))
        {
            GameOver();
        }

        if (collisionEnter.CompareTag("Enemy"))
        {
            GameOver();
        }
    }
    private void OnCollisionStay(Collision other)  
    {
        GameObject collisionStay = other.gameObject;

        while (collisionStay.CompareTag("Slow"))
        {
            speed = 125;
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
        levelFinished.Invoke();
    }
    private void AddPoint()
    {
        gameManager.AddPoint();
    }
}
