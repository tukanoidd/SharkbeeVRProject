using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManipulationButton : MonoBehaviour
{
    private String type;
    private bool forward;
    [SerializeField] private GameObject targetArrowPivotObject;

    private Vector3 startPosition;
    
    protected bool pressed = false;
    protected bool released = false;

    [SerializeField] private ClockManipulationSettings settings;

    void Start()
    {
        startPosition = transform.localPosition;
        
        type = name.Contains("Hours") ? "Hours" : "Minutes";
        forward = name.Contains("Forward");
    }

    private void Update()
    {
        released = false;

        Vector3 localPos = transform.localPosition;
        if (settings.lockX) localPos.x = startPosition.x;
        if (settings.lockY) localPos.y = startPosition.y;
        if (settings.lockZ) localPos.z = startPosition.z;
        transform.localPosition = localPos;
        
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * settings.returnSpeed);
        
        Vector3 allDistances = transform.localPosition - startPosition;
        float distance = 1f;
        if (!settings.lockX) distance = Math.Abs(allDistances.x);
        if (!settings.lockY) distance = Math.Abs(allDistances.y);
        if (!settings.lockZ) distance = Math.Abs(allDistances.z);
        
        float pressComplete = Mathf.Clamp(1 / settings.activationDistance * distance, 0f, 1f);

        if (pressComplete >= 0.7f && !pressed)
        {
            pressed = true;
        }
        else if (pressComplete <= 0.2f && pressed)
        {
            pressed = false;
            released = true;
        }

        if (pressed)
        {
            TurnArrow();
        }
    }

    void TurnArrow()
    {
            targetArrowPivotObject.transform.Rotate(0, 0,
                type == "Hours"
                    ? (forward ? settings.hourDegreeStep : -settings.hourDegreeStep) * Time.deltaTime
                    : (forward ? settings.minuteDegreeStep : -settings.minuteDegreeStep) * Time.deltaTime);
    }
}