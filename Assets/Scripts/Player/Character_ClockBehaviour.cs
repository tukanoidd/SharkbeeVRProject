using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_ClockBehaviour : MonoBehaviour
{
    public bool inClockMinigame = false;
    
    public bool minigameStarted = false;
    public bool minigameDone = false;

    [SerializeField] private ClockMonkey clockMonkey;

    private Character character;

    private void Start()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if (!minigameDone)
        {
            CheckMinigameDistance();

            if (inClockMinigame && !minigameStarted)
            {
                minigameStarted = true;
            }

            if (inClockMinigame && minigameStarted)
            {
                MinigameCheck();
            }
        }
    }

    void CheckMinigameDistance()
    {
        float playerMinigameMonkeyDistance = Vector3.Distance(transform.position, clockMonkey.transform.position);

        inClockMinigame = playerMinigameMonkeyDistance <= clockMonkey.minigameAreaDistance;
    }
    
    void MinigameCheck()
    {
        int currentPhaseTextLength = 0;

        switch (clockMonkey.currentPhase)
        {
            case 0:
                currentPhaseTextLength = clockMonkey.texts.startMinigameTexts.Length;
                break;
            case 2:
                currentPhaseTextLength = clockMonkey.texts.endMinigameTexts.Length;
                break;
        }
        
        if (OVRInput.GetDown(character.tutorialNextTextButton))
        {
            if (clockMonkey.currentPhase != 1)
            {
                if (currentPhaseTextLength != 0)
                {
                    if (clockMonkey.currentPhaseTextIndex >= currentPhaseTextLength)
                    {
                        NextPhase();
                    }
                    else
                    {
                        clockMonkey.currentPhaseTextIndex++;
                    }
                }
            }
        }
        
        if (OVRInput.GetDown(character.tutorialBackTextButton))
        {
            if (clockMonkey.currentPhase != 1)
            {
                clockMonkey.currentPhaseTextIndex =
                    Mathf.Clamp(clockMonkey.currentPhaseTextIndex - 1, 0, currentPhaseTextLength - 1);
            }
        }
    }

    void NextPhase()
    {
        if (clockMonkey.currentPhase == 2)
        {
            EndMinigame();
        }
        else
        {
            clockMonkey.currentPhase++;
        }
    }

    void EndMinigame()
    {
        minigameDone = true;
                
        clockMonkey.text.gameObject.SetActive(false);
    }
}
