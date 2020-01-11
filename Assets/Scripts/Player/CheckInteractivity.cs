using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OVRTouchSample;
using UnityEngine;

public class CheckInteractivity : MonoBehaviour
{
    private Camera playerCam;
    public float isVisibleDistance = 5;

    private Material outlineMaterial;
    private GameObject lastVisibleObj = null;

    private Hand[] hands;

    void Start()
    {
        playerCam = GetComponentsInChildren<Camera>()
            .Where(cam => cam.CompareTag("MainCamera") && cam.isActiveAndEnabled).ToArray()[0];

        outlineMaterial = Resources.Load<Material>("Materials/Outline");

        hands = GetComponentsInChildren<Hand>();
    }

    void Update()
    {
        CheckGrabbable();
    }

    void CheckGrabbable()
    {
        Ray isVisibleRay = new Ray(playerCam.transform.position, playerCam.transform.forward);
        RaycastHit isVisibleRayHit;

        bool seenObject = Physics.Raycast(isVisibleRay, out isVisibleRayHit, isVisibleDistance);
        GameObject isVisibleObj = seenObject ? isVisibleRayHit.transform.gameObject : null;

        if (seenObject)
        {
            if (lastVisibleObj != isVisibleObj ||
                Vector3.Distance(lastVisibleObj.transform.position, playerCam.transform.position) > isVisibleDistance)
            {
                var isVisibleObjGrabbable = isVisibleObj.GetComponent<OVRGrabbable>();

                if (isVisibleObjGrabbable != null)
                {
                    RemoveOutline();
                    lastVisibleObj = isVisibleObj;

                    if (isVisibleObj.GetComponent<Renderer>() != null)
                    {
                        isVisibleObj.GetComponent<Renderer>().materials = new[]
                            {isVisibleObj.GetComponent<Renderer>().materials[0], outlineMaterial};   
                    }
                }
                else
                {
                    RemoveOutline();
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
        if (lastVisibleObj != null && lastVisibleObj.activeSelf)
        {
            if (lastVisibleObj.GetComponent<Renderer>() != null)
                lastVisibleObj.GetComponent<Renderer>().materials =
                    new[] {lastVisibleObj.GetComponent<Renderer>().materials[0]};

            lastVisibleObj = null;
        }
    }
}