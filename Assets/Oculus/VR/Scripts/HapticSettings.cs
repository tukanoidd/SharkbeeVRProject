using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticSettings
{
    public float frequency = 1;
    public float amplitude = 1;

    HapticSettings(float frequency, float amplitude)
    {
        this.frequency = frequency;
        this.amplitude = amplitude;
    }
    
    public void PlayHapticFeedback()
    {
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.GetActiveController());
    }
}
