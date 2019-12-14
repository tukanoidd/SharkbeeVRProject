using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TutorialMonkey : MonoBehaviour
{
    public int currentPhase = 0;

    [SerializeField] private Transform miniGamePosition;
    public bool teleported;
    
    [SerializeField] private GameObject[] tutorialExitColliders;

    public TutorialPhase[] tutorialPhases;
    [SerializeField] private GameObject text;

    private Character player;

    public float tutorialAreaDistance = 6;
    public float nearTutorialMonkeyDistance;

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialExitColliders != null)
        {
            if (tutorialExitColliders.Length > 0)
            {
                tutorialAreaDistance = Mathf.Max(tutorialExitColliders.Select(tutorialExitCollider =>
                    Vector3.Distance(tutorialExitCollider.transform.position, transform.position)).ToArray());
            }
        }

        nearTutorialMonkeyDistance = tutorialAreaDistance / 3f;
    }

    void Update()
    {
        if (player.tutorialStarted)
        {
            var currentTutorialPhase = tutorialPhases[currentPhase];
            text.GetComponent<TextMeshPro>().text = currentTutorialPhase.texts[currentTutorialPhase.currentTextIndex];
            
            if (!text.activeSelf)
            {
                text.SetActive(true);
            }
        }
    }

    public void TeleportToMinigame()
    {
        transform.position = miniGamePosition.position;
        teleported = true;

        if (tutorialExitColliders != null)
        {
            foreach (var tutorialExitCollider in tutorialExitColliders)
            {
                tutorialExitCollider.SetActive(false);
            }   
        }
    }
}

[Serializable]
public class TutorialPhase
{
    public string[] texts;
    public int currentTextIndex = 0;
    public TutorialPhaseChecker checker;
    public bool phaseDone => checker.PhaseDone();
}

[Serializable]
public class TutorialPhaseChecker
{
    public PickingUpChecker pickingUpChecker;
    public WalkingChecker walkingChecker;
    public ComeBackChecker comeBackChecker;

    public bool PhaseDone()
    {
        bool pickChecker;
        bool walkChecker;
        bool comeBackCheck;

        pickChecker = pickingUpChecker.includedInPhase ? pickingUpChecker.PickingDone : true;
        walkChecker = walkingChecker.includedInPhase ? walkingChecker.walkingDone : true;
        comeBackCheck = comeBackChecker.includedInPhase ? comeBackChecker.backNearTutorialMonkey : true;

        return pickChecker && walkChecker && comeBackCheck;
    }
}

[Serializable]
public class Checker
{
    public bool includedInPhase;
}

[Serializable]
public class PickingUpChecker : Checker
{
    public OVRInput.Button pickingButton;
    public bool pickingButtonPressed = false;

    public bool picked = false;
    public bool dropped = false;

    public bool PickingDone => pickingButtonPressed && picked && dropped;
}

[Serializable]
public class WalkingChecker : Checker
{
    public OVRInput.Axis2D movingStick;
    public bool movingStickUsed = false;

    public OVRInput.Axis2D rotationStick;
    public bool rotationStickUsed = false;

    public bool walkingDone => movingStickUsed && rotationStickUsed;
}

[Serializable]
public class ComeBackChecker : Checker
{
    public bool backNearTutorialMonkey;
}