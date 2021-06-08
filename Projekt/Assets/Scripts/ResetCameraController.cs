using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    
    
    public IEnumerator PlayResetCam()
    {
        gameObject.SetActive(true);
        mainCamera.SetActive(false);
        
        yield return new WaitForSeconds(2);
        
        gameObject.SetActive(false);
        mainCamera.SetActive(true);
    }
}
