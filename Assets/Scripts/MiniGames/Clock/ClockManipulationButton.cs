using System;
using System.Collections;
using System.Collections.Generic;
using Monkeys;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ClockManipulationButton : MonoBehaviour
{
    [SerializeField] private bool isOkButton = false;

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

    [SerializeField] private ClockPlayer clockPlayer;
    [SerializeField] private ClockMonkey clockMonkey;

    [SerializeField] private int minuteMaxDiff = 3;

    private ClockMonkey.MinigamePhaseInfo minigamePhase = null;

    void Start()
    {
        startPosition = transform.localPosition;

        type = name.Contains("Hours") ? "Hours" : "Minutes";
        forward = name.Contains("Forward");

        if (clockMonkey != null) minigamePhase = clockMonkey.phases.minigamePhase;
    }

    private void Update()
    {
        released = false;

        Vector3 localPos = transform.localPosition;
        if (settings.lockX) localPos.x = startPosition.x;
        if (settings.lockY) localPos.y = startPosition.y;
        if (settings.lockZ) localPos.z = startPosition.z;
        transform.localPosition = localPos;

        transform.localPosition =
            Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * settings.returnSpeed);

        Vector3 allDistances = transform.localPosition - startPosition;
        float distance = 0.6f;
        if (!settings.lockX) distance = Math.Abs(allDistances.x);
        if (!settings.lockY) distance = Math.Abs(allDistances.y);
        if (!settings.lockZ) distance = Math.Abs(allDistances.z);
        float pressComplete = Mathf.Clamp(1 / settings.activationDistance * distance, 0f, 1f);

        if (pressComplete >= 0.3f)
        {
            Debug.Log(pressComplete);   
        }

        if (pressComplete >= 0.6f && !pressed)
        {
            pressed = true;
        }
        else if (pressComplete <= 0.2f && pressed)
        {
            pressed = false;
            released = true;
            timeChecked = false;
        }

        if (pressed && !isOkButton) TurnArrow();

        if (released && isOkButton) CheckTime();
    }

    void CheckTime()
    {
        if (!timeChecked && clockPlayer != null && clockMonkey != null && minigamePhase != null)
        {
            if (clockPlayer.inClockMinigame && clockPlayer.clockStarted && !clockPlayer.clockDone)
            {
                if (clockMonkey.currentPoints < clockMonkey.neededPoints)
                {
                    var times = minigamePhase.times;
                    var currentTimeIndex = minigamePhase.currentTimeIndex;
                    var currentTime = times[currentTimeIndex];

                    KeyValuePair<int, int> time = GetTime();

                    QuestDebug.Instance.Log("Hours: " + time.Key + "/" + currentTime.hours);
                    QuestDebug.Instance.Log("Minutes: " + time.Value + "/" + currentTime.minutes, true);

                    Debug.Log("Hours: " + time.Key + "/" + currentTime.hours);
                    Debug.Log("Minutes: " + time.Value + "/" + currentTime.minutes);

                    if (time.Key == (currentTime.hours % 12) &&
                        Mathf.Abs(time.Value - (currentTime.minutes % 60)) <= minuteMaxDiff)
                    {
                        timeChecked = true;
                        clockMonkey.RightTime(currentTimeIndex, currentTime);
                    }
                    else
                    {
                        timeChecked = false;
                        clockMonkey.WrongTime(currentTime);
                    }
                }
            }
        }
    }

    KeyValuePair<int, int> GetTime()
    {
        return new KeyValuePair<int, int>(ConvertArrow("hours"), ConvertArrow("minutes"));
    }

    //todo add conversion
    int ConvertArrow(string mode)
    {
        if (mode == "hours")
        {
            return ((((int) hourArrowPivotObject.transform.eulerAngles.z) % 360) / 30) % 12;
        }
        else
        {
            return ((((int) minuteArrowPivotObject.transform.eulerAngles.z) % 360) / 6) % 60;
        }
    }

    void TurnArrow()
    {
        Debug.Log("checkturn");
        targetArrowPivotObject.transform.Rotate(0, 0,
            type == "Hours"
                ? (forward ? settings.hourDegreeStep : -settings.hourDegreeStep) * Time.deltaTime
                : (forward ? settings.minuteDegreeStep : -settings.minuteDegreeStep) * Time.deltaTime);
    }
}