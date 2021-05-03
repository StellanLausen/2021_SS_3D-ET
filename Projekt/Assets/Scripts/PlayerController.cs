using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    public float jump = 300;
    public float GameOverHeight = 0f;

    private Boolean isGrounded;
    private Vector3 startPosVec;
    private float jumpPressTime;
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
        startPosVec = transform.position;
    }

    void Update()
    {
        GameOver();
    }

    private void FixedUpdate()
    {
        //control
        Vector2 movement = move.ReadValue<Vector2>();
        rb.AddForce(speed * Time.deltaTime * movement.x, 0, speed * Time.deltaTime * movement.y);
        
        //jump
        Vector3 RayCastDirectrion = new Vector3(0,-1,0);
        if (Physics.Raycast(transform.position, RayCastDirectrion, 1f, LayerMask.GetMask("Ground")))
        {
            Jump();
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
            if (jumpPressTime < 20)
            {
                jumpPressTime = 0;
            }
            else if (jumpPressTime > 50)
            {
                jumpPressTime = 50;
            }
            rb.AddForce(0, jump + jumpPressTime, 0);
            jumpPressTime = 0;
        }
    }
    private void GameOver()
    {
        if (rb.transform.position.y <= GameOverHeight)
        {
            rb.transform.position = startPosVec;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log("Leider verloren");
        }
    }
    private void Finished()
    {
        transform.position = startPosVec;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Debug.Log("Ziel erreicht");
    }
}
