using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //fusch
    public GameObject mainCamera;
    public GameObject resetCam;

    public float speed = 150;
    public float jump = 300;
    public float gameOverHeight = 0f;

    private Boolean shouldJump;
    private float collectedJumpTime;
    private Boolean isGrounded;
    private Vector3 startPosVec;
    private float jumpPressTime = 0;
    private Rigidbody rb;

    public InputAction move;
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
        rb = GetComponent<Rigidbody>();
        startPosVec = new Vector3(0,0.5f,0);
    }
    void Update()
    {
        GameOver();
        //jump
        Vector3 rayCastDirectrion = new Vector3(0,-1,0);
        if (Physics.Raycast(transform.position, rayCastDirectrion, 1f, LayerMask.GetMask("Ground")))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        //control
        Vector2 movement = move.ReadValue<Vector2>();
        rb.AddForce(speed * Time.deltaTime * movement.x, 0, speed * Time.deltaTime * movement.y);

        //jump
        if (shouldJump)
        {
            rb.AddForce(0, jump + collectedJumpTime, 0);
            collectedJumpTime = 0;
            shouldJump = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Finished();
        }
    }
    private void Jump()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            jumpPressTime = jumpPressTime + 0.15f;
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            shouldJump = true;
            if (jumpPressTime < 20)
            {
                jumpPressTime = 0;
            }
            else if (jumpPressTime > 50)
            {
                jumpPressTime = 50;
            }
            collectedJumpTime = jumpPressTime;
            jumpPressTime = 0;
        }
    }
    private void GameOver()
    {
        if (rb.transform.position.y <= gameOverHeight)
        {
            transform.position = startPosVec;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log("Leider verloren");
            
            //Pfusch
            StartCoroutine(resetCam.GetComponent<ResetCameraController>().PlayResetCam());
        }
    }
    private void Finished()
    {
        transform.position = startPosVec;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Debug.Log("Ziel erreicht");
        
        //Pfusch
        //StartCoroutine(PlayResetCam());
    }
    IEnumerator PlayResetCam()
    {
        resetCam.SetActive(true);
        mainCamera.SetActive(false);
        yield return new WaitForSeconds(2);
        resetCam.SetActive(false);
        mainCamera.SetActive(true);
    }
}
