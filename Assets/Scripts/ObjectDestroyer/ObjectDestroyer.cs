using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    void CheckIfGrabbable(Collider other)
    {
        var grabbable = other.GetComponent<OVRGrabbable>();
        if (grabbable != null)
        {
            if (!grabbable.isGrabbed)
            {
                QuestDebug.Instance.Log(grabbable.name, true);
                Destroy(grabbable.gameObject);   
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        CheckIfGrabbable(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckIfGrabbable(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckIfGrabbable(other.collider);
    }

    private void OnCollisionStay(Collision other)
    {
        CheckIfGrabbable(other.collider);
    }
}
