using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public OVRPlayerController controller;
    
    public OVRInput.Button tutorialBackTextButton = OVRInput.Button.One;
    public OVRInput.Button tutorialNextTextButton = OVRInput.Button.Two;

    void Start()
    {
        controller = GetComponent<OVRPlayerController>();
    }

    
}