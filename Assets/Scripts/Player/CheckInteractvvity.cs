using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckInteractvvity : MonoBehaviour
{
    private Camera playerCam;
    public float isVisibleDistance = 5;

    private Material outlineMaterial;

    private GameObject lastVisibleObj = null;

    void Start()
    {
        playerCam = GetComponentInChildren<Camera>();

        outlineMaterial = Resources.Load<Material>("Materials/Outline");
    }

    void Update()
    {
        Ray isVisibleRay = new Ray(playerCam.transform.position, playerCam.transform.forward);
        RaycastHit isVisibleRayHit;
        
        bool seenObject = Physics.Raycast(isVisibleRay, out isVisibleRayHit, isVisibleDistance);
        GameObject isVisibleObj = isVisibleRayHit.transform.gameObject;

        if (seenObject)
        {
            if (lastVisibleObj != isVisibleObj)
            {
                var isVisiblrObjGrabbable = isVisibleObj.GetComponent<OVRGrabbable>();

                if (isVisiblrObjGrabbable != null)
                {
                    RemoveOutline();
                    lastVisibleObj = isVisibleObj;

                    isVisibleObj.GetComponent<Renderer>().materials = new[]
                        {isVisibleObj.GetComponent<Renderer>().materials[0], outlineMaterial};
                }
            }
        }
        else
        {
            RemoveOutline();
        }
    }

    void RemoveOutline()
    {
        if (lastVisibleObj != null)
        {
            lastVisibleObj.GetComponent<Renderer>().materials =
                new[] {lastVisibleObj.GetComponent<Renderer>().materials[0]};
        }
    }
}