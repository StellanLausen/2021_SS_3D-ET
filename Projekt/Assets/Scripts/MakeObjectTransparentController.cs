using System.Collections.Generic;
using UnityEngine;

/*
Transparent:	Änderungen vorbehalten. Siehe in "Projekt Datei" für genaue Zeilenangaben.
Name Arbeit:	youtube Video Titel: 	Unity - Make Objects Between Camera and Player Transparent (No Shader Manipulation)
Name Autor:		youtube Channel name: 	FumetsuHito	
URL:			https://www.youtube.com/watch?v=xMFx9HfRknU&t=479s	
Abrufdatum:		07.06.2021
Lizenzmodel:	-
 */
public class MakeObjectTransparentController : MonoBehaviour
{
    //source Transparent start -source code changed
    [SerializeField] private List<SwitchMaterial> currentInTheWay, alreadyTransparent;
    [SerializeField] private Transform player;
    private Transform cameraTrans;
    private void Awake()
    {
        currentInTheWay = new List<SwitchMaterial>();
        alreadyTransparent = new List<SwitchMaterial>();

        cameraTrans = gameObject.transform;
    }
    private void Update()
    {
        GetAllObjectInTheWay();
        MakeObjectTransparent();
        MakeObjectSolid();
    }
    private void GetAllObjectInTheWay()
    {
        //source code changed
        //added function to make child of Parent transparent
        //only using one Raycast
        currentInTheWay.Clear();

        var cameraPos = cameraTrans.position;
        var playerPos = player.position;
        
        var rayCastDir = playerPos - cameraPos;
        var rayDistance = Vector3.Magnitude(cameraPos - playerPos);

        var singleRay = new Ray(cameraPos, rayCastDir);
        var hitRayCast = Physics.RaycastAll(singleRay, rayDistance);

        foreach (var hit in hitRayCast)
        {
            if (!hit.collider.gameObject.TryGetComponent(out SwitchMaterial inTheWay)) continue;
            if (currentInTheWay.Contains(inTheWay)) continue;
            currentInTheWay.Add(inTheWay);
            
            //source EventSystem end
            //check for SwitchMaterialGroup and add all children if available
            if (inTheWay.transform.parent.gameObject.CompareTag("GroupTP"))
            {
                var inTheWayGroup = inTheWay.transform.parent.gameObject;
                foreach (var child in inTheWayGroup.GetComponentsInChildren<SwitchMaterial>())
                {
                    currentInTheWay.Add(child);
                }
            }
        }
    }
    //source Transparent start -source code changed
    private void MakeObjectTransparent()
    {
        foreach (var inTheWay in currentInTheWay)
        {
            if (alreadyTransparent.Contains(inTheWay)) continue;
            inTheWay.ChangeActive();
            alreadyTransparent.Add(inTheWay);
        }
    }
    private void MakeObjectSolid()
    {
        for (var i = alreadyTransparent.Count-1; i >= 0; i--)
        {
            var wasInTheWay = alreadyTransparent[i];

            if (currentInTheWay.Contains(wasInTheWay)) continue;
            wasInTheWay.ChangeActive();
            alreadyTransparent.Remove(wasInTheWay);
        }
    }
    //source EventSystem end
}
