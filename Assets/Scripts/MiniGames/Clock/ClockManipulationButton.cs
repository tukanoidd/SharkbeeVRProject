using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManipulationButton : MonoBehaviour
{
    private bool isOkButton = false;
    
    private String type;
    private bool forward;
    [SerializeField] private GameObject targetArrowPivotObject;
    [SerializeField] private GameObject hourArrowPivotObject;
    [SerializeField] private GameObject minuteArrowPivotObject;

    private Vector3 startPosition;
    
    private bool pressed = false;
    private bool released = false;
    private bool timeChecked = false;
    
    [SerializeField] private ClockManipulationSettings settings;

    [SerializeField] private Character_ClockBehaviour player;
    [SerializeField] private ClockMonkey clockMonkey;

    [SerializeField] private int minuteMaxDiff = 3; 

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
            timeChecked = false;
        }

        if (pressed)
        {
            if (isOkButton)
            {
                CheckTime();
            }
            else
            {
                TurnArrow();
            }
        }
    }

    void CheckTime()
    {
        if (!timeChecked)
        {
            if (player.inClockMinigame && player.minigameStarted && !player.minigameDone)
            {
                if (clockMonkey.currentPoints < clockMonkey.neededPoints)
                {
                    KeyValuePair<int, int> time = GetTime();

                    if (time.Key == clockMonkey.currentTime.hour && Mathf.Abs(time.Value - clockMonkey.currentTime.minute) <= minuteMaxDiff)
                    {
                        clockMonkey.currentPoints++;
                    }

                    clockMonkey.currentTime = ClockMonkey.ClockRandomizer.RandomizeTime();
                    timeChecked = true;
                }   
            }   
        }
    }

    KeyValuePair<int, int> GetTime()
    {
        return new KeyValuePair<int, int>(ConvertArrow(), ConvertArrow("minutes"));
    }

    //todo add conversion
    int ConvertArrow(string mode = "hours")
    {
        if (mode == "hours")
        {
            return (int) (hourArrowPivotObject.transform.eulerAngles.z);
        }
        else
        {
            return (int) (minuteArrowPivotObject.transform.eulerAngles.z);
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