using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ClockManipulationSettings : ScriptableObject
{
    public float minuteDegreeStep = 6;
    public float hourDegreeStep = 30;
    
    public bool lockX = true;
    public bool lockY = false;
    public bool lockZ = true;
    
    public float returnSpeed = 2;
    public float activationDistance = 0.3f;
}
