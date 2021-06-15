using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Analytics;

public class Transparent : MonoBehaviour
{
    [SerializeField] private List<SwitchMaterial> currentInTheWay;
    [SerializeField] private List<SwitchMaterial> alreadyTransparent;
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
        currentInTheWay.Clear();

        var cameraPos = cameraTrans.position;
        var playerPos = player.position;
        
        var rayCastDir = playerPos - cameraPos;
        var rayDistance = Vector3.Magnitude(cameraPos - playerPos);

        var singleRay = new Ray(cameraPos, rayCastDir);
        var hitRayCast = Physics.RaycastAll(singleRay, rayDistance);

        foreach (var hit in hitRayCast)
        {
            if (hit.collider.gameObject.TryGetComponent(out SwitchMaterial inTheWay))
            {
                if (!currentInTheWay.Contains(inTheWay))
                {
                    currentInTheWay.Add(inTheWay);
                    //check for SwitchMaterialGroup and add all children if available
                    if (inTheWay.transform.parent.gameObject.TryGetComponent(out SwitchMaterialGroup inTheWayGroup)) 
                        //(inTheWay.transform.parent.gameObject.CompareTag("GroupTP"));
                    {
                        foreach (var child in inTheWayGroup.GetComponentsInChildren<SwitchMaterial>())
                        {
                            currentInTheWay.Add(child);
                        }
                    }
                }
            }
        }
    }
    private void MakeObjectTransparent()
    {
        foreach (var inTheWay in currentInTheWay)
        {
            if (!alreadyTransparent.Contains(inTheWay))
            {
                inTheWay.ChangeActive();
                alreadyTransparent.Add(inTheWay);
            }
        }
    }
    private void MakeObjectSolid()
    {
        for (int i = alreadyTransparent.Count-1; i >= 0; i--)
        {
            var wasInTheWay = alreadyTransparent[i];

            if (currentInTheWay.Contains(wasInTheWay)) continue;
            wasInTheWay.ChangeActive();
            alreadyTransparent.Remove(wasInTheWay);
        }
    }
}
