using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/*
PlayerController:	Änderungen vorbehalten. Siehe in "Projekt Datei" für genaue Zeilenangaben.
Name Arbeit:		youtube Video Titel: 	MOVE THE RIGHT WAY! - Moving in Unity - Camera Relative Inputs - 
                                            Intermediate Tutorial Series Part 3
Name Autor:		    youtube Channel name: 	Nimso ny
URL:				https://www.youtube.com/watch?v=ORD7gsuLivE&t=524s	
Abrufdatum:			4.06.2021
Lizenzmodel:		-
 */
public class PlayerController : MonoBehaviour
{
    public class IntEvent : UnityEvent<int> {}
    public InputAction move;
    
    [SerializeField] private GameObject deathParticle, resetCam, mainCam;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MeshRenderer playerMeshRend;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Rigidbody rb;
    //Player Variables
    [SerializeField] private float speed = 300, jumpForceDefault = 300;

    //Jump
    private bool shouldJump, shouldJumpViaTag = true;
    private float collectedJumpForce;
    //DeathAnimation
    private bool ableToMove = true;
    //save Start Position
    private Vector3 startPosVec;

    //Events
    public UnityEvent levelFinished = new UnityEvent();
    public UnityEvent resetMap = new UnityEvent();
    public UnityEvent resetCamToStaticCam = new UnityEvent();
    
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
        startPosVec = transform.position;
    }
    private void Update()
    {
        //jump
        if (!shouldJumpViaTag) return;
        var rayCastDirectrion = new Vector3(0,-1,0);
        if (Physics.Raycast(transform.position, rayCastDirectrion, 1f, groundLayerMask))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        if (!ableToMove) return;
        
        //source playerController start -source code changed
        Vector3 camY = mainCamera.forward;
        Vector3 camX = mainCamera.right;

        camY.y = 0;
        camX.y = 0;

        camY = camY.normalized;
        camX = camX.normalized;
        //source playerController end
        
        //Read wasd into movement
        Vector2 movement = move.ReadValue<Vector2>();

        //if movement multiply it with direction for x and y
        Vector3 y = camY * movement.y;
        Vector3 x = camX * movement.x;

        Vector3 moveDir = x + y;
        rb.AddForce(speed * moveDir.x * Time.deltaTime, 0, speed * moveDir.z * Time.deltaTime);

        //jump
        if (shouldJump)
        {
            rb.AddForce(0, jumpForceDefault + collectedJumpForce, 0);
            collectedJumpForce = 0;
            shouldJump = false;
        }
    }
    private void OnTriggerEnter(Collider triggerEnter)
    {
        var triggerEnterGameObj = triggerEnter.gameObject;
        
        if (triggerEnterGameObj.gameObject.layer == LayerMask.NameToLayer("LevelEnd"))
        {
            LostLive();
        }
        
        if (triggerEnterGameObj.CompareTag("HittingBullet"))
        {
            LostLive();
        }
        
        if (triggerEnter.CompareTag("Coin"))
        {
            Destroy(triggerEnterGameObj);
            gameManager.AddPoint();
        }

        if (triggerEnter.CompareTag("ExtraLive"))
        {
            Destroy(triggerEnterGameObj);
            gameManager.AddLive();
        }
        
        if (triggerEnter.CompareTag("Finish"))
        {
            Finished();
        }
    }
    private void OnCollisionEnter(Collision collisionEnter)
    {
        var collisionEnterGameObj = collisionEnter.gameObject;

        if (collisionEnterGameObj.CompareTag("Trap"))
        {
            LostLive();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var triggerStayGameObj = other.gameObject;
        
        if (triggerStayGameObj.CompareTag("NoJumping"))
        {
            shouldJumpViaTag = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var triggerStayGameObj = other.gameObject;
        
        if (triggerStayGameObj.CompareTag("NoJumping"))
        {
            shouldJumpViaTag = true;
        }
    }
    private void Jump()
    {
        var spaceKey = Keyboard.current.spaceKey;

        if (spaceKey.isPressed)
        {
            collectedJumpForce += 0.15f;
        }

        if (spaceKey.wasReleasedThisFrame)
        {
            shouldJump = true;
            if (collectedJumpForce < 20)
            {
                collectedJumpForce = 0;
            }
            else if (collectedJumpForce > 50)
            {
                collectedJumpForce = 50;
            }
        }
    }
    private void LostLive()
    {
        StartCoroutine(DeathAnimation());
    
        Debug.Log("Leider verloren");
        
        resetMap.Invoke();
        gameManager.RemoveLive();
    }
    private void Finished()
    {
        Debug.Log("Ziel erreicht");
        
        transform.position = startPosVec;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        levelFinished.Invoke();
        gameManager.FinishedLevel();
    }
    private IEnumerator DeathAnimation()
    {
        //position of particle
        deathParticle.transform.position = transform.position;
        
        //Pause and reset player/player movement
        ableToMove = false;
        rb.useGravity = false;
        playerMeshRend.enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //activate particle
        deathParticle.SetActive(true);

        yield return new WaitForSeconds(1);
        
        //reset Camera to static Camera
        resetCamToStaticCam.Invoke();
        //set player to start without being able to move
        rb.useGravity = true;
        playerMeshRend.enabled = true;
        transform.position = startPosVec;
        //deactivate particle
        deathParticle.SetActive(false);

        //change Camera to resetCam
        resetCam.SetActive(true);
        mainCam.SetActive(false);

        yield return new WaitForSeconds(2);
        
        //change Camera to mainCamera
        mainCam.SetActive(true);
        resetCam.SetActive(false);
        
        //activate player movement
        ableToMove = true;
    }
}
