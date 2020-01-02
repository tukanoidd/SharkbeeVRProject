/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Character_ClockBehaviour : MonoBehaviour
{
    public bool inClockMinigame = false;
    
    public bool minigameStarted = false;
    public bool minigameDone = false;

    [FormerlySerializedAs("clockMonkey")] [SerializeField] private ClockMonkey_old clockMonkeyOld;
    private TutorialMonkey_old _tutorialMonkeyOld;

    private Character character;

    private void Start()
    {
        character = GetComponent<Character>();
        _tutorialMonkeyOld = clockMonkeyOld.GetComponent<TutorialMonkey_old>();
    }

    private void Update()
    {
        if (_tutorialMonkeyOld.teleported && !minigameDone)
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
        float playerMinigameMonkeyDistance = Vector3.Distance(transform.position, clockMonkeyOld.transform.position);

        inClockMinigame = playerMinigameMonkeyDistance <= clockMonkeyOld.minigameAreaDistance;
    }
    
    void MinigameCheck()
    {
        int currentPhaseTextLength = 0;

        switch (clockMonkeyOld.currentPhase)
        {
            case 0:
                currentPhaseTextLength = clockMonkeyOld.texts.startMinigameTexts.Length;
                break;
            case 2:
                currentPhaseTextLength = clockMonkeyOld.texts.endMinigameTexts.Length;
                break;
        }
        
        if (OVRInput.GetDown(character.tutorialNextTextButton))
        {
            if (clockMonkeyOld.currentPhase != 1)
            {
                if (currentPhaseTextLength != 0)
                {
                    if (clockMonkeyOld.currentPhaseTextIndex >= currentPhaseTextLength)
                    {
                        NextPhase();
                    }
                    else
                    {
                        clockMonkeyOld.currentPhaseTextIndex++;
                    }
                }
            }
        }
        
        if (OVRInput.GetDown(character.tutorialBackTextButton))
        {
            if (clockMonkeyOld.currentPhase != 1)
            {
                clockMonkeyOld.currentPhaseTextIndex =
                    Mathf.Clamp(clockMonkeyOld.currentPhaseTextIndex - 1, 0, currentPhaseTextLength - 1);
            }
        }
    }

    void NextPhase()
    {
        if (clockMonkeyOld.currentPhase == 2)
        {
            EndMinigame();
        }
        else
        {
            clockMonkeyOld.currentPhase++;
        }
    }

    void EndMinigame()
    {
        minigameDone = true;
                
        clockMonkeyOld.text.gameObject.SetActive(false);
    }
}
*/
