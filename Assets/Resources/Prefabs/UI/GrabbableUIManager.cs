using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableUIManager : MonoBehaviour
{
    void Start()
    {
        OVRGrabbable[] grabbableItems = FindObjectsOfType<OVRGrabbable>();

        foreach (var grabbableItem in grabbableItems) grabbableItem.gameObject.AddComponent<GrabbableItem>();
    }
}
