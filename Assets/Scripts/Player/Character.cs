using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool inTutorial = false;
    private bool inTutorialArea = false;
    public bool tutorialStarted = false;
    private bool tutorialDone = false;

    private TutorialMonkey tutorialMonkey;

    private OVRInput.Button tutorialBackTextButton;
    private OVRInput.Button tutorialNextTextButton;

    private OVRPlayerController controller;

    void Start()
    {
    }

    void Update()
    {
        if (!tutorialDone)
        {
            if (!tutorialStarted)
            {
                tutorialStarted = true;

                CheckTutorialDistance();
            }

            if (inTutorial || inTutorialArea) TutorialCheck();
        }
        else
        {
        }
    }

    void CheckTutorialDistance()
    {
        float playerTutorialMonkeyDistance = Vector3.Distance(transform.position, tutorialMonkey.transform.position);

        inTutorialArea = playerTutorialMonkeyDistance <= tutorialMonkey.tutorialAreaDistance;
        inTutorial = playerTutorialMonkeyDistance <= tutorialMonkey.nearTutorialMonkeyDistance;
    }

    void TutorialCheck()
    {
        CheckTutorialControls();
        UseTutorialCheckers();
    }

    private void CheckTutorialControls()
    {
        var currentPhase = tutorialMonkey.tutorialPhases[tutorialMonkey.currentPhase];

        if (OVRInput.GetDown(tutorialNextTextButton))
        {
            if (currentPhase.currentTextIndex + 1 >= currentPhase.texts.Length)
            {
                if (currentPhase.phaseDone)
                {
                    NextPhase();
                }
            }
            else
            {
                currentPhase.currentTextIndex++;
            }
        }
        else if (OVRInput.GetDown(tutorialBackTextButton))
        {
            currentPhase.currentTextIndex =
                Mathf.Clamp(currentPhase.currentTextIndex, 0, currentPhase.texts.Length - 1);
        }
    }

    void NextPhase()
    {
        if (tutorialMonkey.currentPhase + 1 >= tutorialMonkey.tutorialPhases.Length)
        {
            EndTutorial();
        }
        else
        {
            tutorialMonkey.currentPhase++;
        }
    }

    void UseTutorialCheckers()
    {
        var currentPhaseCheckers = tutorialMonkey.tutorialPhases[tutorialMonkey.currentPhase].checker;

        if (currentPhaseCheckers.pickingUpChecker.includedInPhase)
        {
            if (currentPhaseCheckers.walkingChecker.includedInPhase) controller.lockedMovement = false;
            else controller.lockedMovement = true;

            TutorialPickupCheck(currentPhaseCheckers.pickingUpChecker);
        }

        TutorialWalkCheck(currentPhaseCheckers.walkingChecker);

        TutorialComeBackCheck(currentPhaseCheckers.comeBackChecker);
    }

    void TutorialPickupCheck(PickingUpChecker checker)
    {
        if (OVRInput.GetDown(checker.pickingButton)) checker.pickingButtonPressed = true;

        if (GetComponent<OVRGrabber>().grabbedObject != null) checker.picked = true;
        else if (checker.picked) checker.dropped = true;
    }

    void TutorialWalkCheck(WalkingChecker checker)
    {
        if (checker.includedInPhase)
        {
            controller.lockedMovement = false;
            
            if (OVRInput.Get(checker.movingStick) != Vector2.zero) checker.movingStickUsed = true;
            if (OVRInput.Get(checker.rotationStick) != Vector2.zero) checker.rotationStickUsed = true;
        }
    }

    void TutorialComeBackCheck(ComeBackChecker checker)
    {
        if (checker.includedInPhase)
        {
            controller.lockedMovement = false;
            
            if (inTutorial) checker.backNearTutorialMonkey = true;   
        }
    }

    void EndTutorial()
    {
        tutorialDone = true;
        
        if (!tutorialMonkey.teleported)
        {
            if (!tutorialMonkey.GetComponent<Renderer>().isVisible)
            {
                tutorialMonkey.TeleportToMinigame();
            }
        }

        if (controller.lockedMovement) controller.lockedMovement = false;
    }
}