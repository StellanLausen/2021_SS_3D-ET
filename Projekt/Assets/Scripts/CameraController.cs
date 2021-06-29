using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

/*CameraController:	Änderungen vorbehalten. Siehe in "Projekt Datei" für genaue Zeilenangaben.
Name Arbeit:		youtube Video Titel: 	HOW TO ROTATE THE CAMERA AROUND AN OBJECT IN UNITY (EASY TUTORIAL)
Name Autor:		    youtube Channel name: 	Emma Prats
URL:				https://www.youtube.com/watch?v=rDJOilo4Xrg	
Abrufdatum:			04.06.2021
Lizenzmodel:		-
*/
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private InputAction mouse, zoom;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform camTrans;

    private Vector3 previousPos = new Vector3(0,0.51f,0);
    
    private int id = 0;
    private float zoomAmount = -10f;
    private const float MAXZoomIn = -5f, MAXZoomOut = -15f;

    private void Start()
    {
        player.GetComponent<PlayerController>().resetCamToStaticCam.AddListener(ResetCam);
    }
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
        SelectCam();
        if(Keyboard.current.xKey.wasPressedThisFrame) id = 0;
        if (Keyboard.current.cKey.wasPressedThisFrame) id = 1;
        if (Keyboard.current.vKey.wasPressedThisFrame) id = 2;
    }
    private void SelectCam()
    {
        switch (id)
        {
            case 0 :
                StaticCam();
                break;
            case 1 :
                OnClickCam();
                break;
            case 2 :
                MoveCam();
                break;
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
    private void OnClickCam()
    {
        camTrans.position = player.transform.position;
        camTrans.Translate(new Vector3(0,0,HandleZoom()));
        
        //source CameraController start -source code changed
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
            //source Enemy end
        }
    }
    private void MoveCam()
    {
        camTrans.position = player.transform.position;
        camTrans.Translate(new Vector3(0,0,HandleZoom()));
        
        //source CameraController start -source code changed
        Vector3 direction = previousPos - cam.ScreenToViewportPoint(mouse.ReadValue<Vector2>());
        
        camTrans.position = player.transform.position;
        
        camTrans.Rotate(new Vector3(1,0,0), direction.y * 180);
        camTrans.Rotate(new Vector3(0,1,0), -direction.x * 180, Space.World);
        camTrans.Translate(new Vector3(0,0,HandleZoom()));

        previousPos = cam.ScreenToViewportPoint(mouse.ReadValue<Vector2>());
        //source Enemy end
    }
    private float HandleZoom()
    {
        var zoomInput = zoom.ReadValue<float>();

        if (zoomInput > 0 && zoomAmount <= MAXZoomIn)
        {
            zoomAmount = zoomAmount + zoomInput * Time.deltaTime;
        }
        else if (zoomInput < 0 && zoomAmount >= MAXZoomOut)
        {
            zoomAmount = zoomAmount + zoomInput * Time.deltaTime;
        }

        return zoomAmount;
    }
    private void ResetCam()
    {
        id = 0;
        zoomAmount = -10f;
        Debug.Log("reset");
    }
}