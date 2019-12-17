using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public OVRPlayerController controller;

    void Start()
    {
        controller = GetComponent<OVRPlayerController>();
    }

    
}