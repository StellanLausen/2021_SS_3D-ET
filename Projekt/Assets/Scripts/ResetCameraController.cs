using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCameraController : MonoBehaviour
{
    public GameObject mainCamera;

    public IEnumerator PlayResetCam()
    {
        this.gameObject.SetActive(true);
        mainCamera.SetActive(false);
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
        mainCamera.SetActive(true);
    }
}
