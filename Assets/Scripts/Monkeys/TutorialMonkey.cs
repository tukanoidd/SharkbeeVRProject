using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using TMPro;
using UnityEngine;

public class TutorialMonkey : MonoBehaviour
{
    public int currentPhase = 0;

    [SerializeField] private Transform miniGamePosition;
    public bool teleported;

    [SerializeField] private GameObject[] tutorialExitColliders;

    public TutorialPhase[] tutorialPhases;

    public GameObject text;
    [SerializeField] private TextMeshPro textNext;

    [SerializeField] private Character_TutorialBehavior player;

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

        nearTutorialMonkeyDistance = tutorialAreaDistance / 2f;
    }

    void Update()
    {
        if (player != null)
        {
            if (player.tutorialStarted && !player.tutorialDone && !teleported)
            {
                var currentTutorialPhase = tutorialPhases[currentPhase];
                text.GetComponent<TextMeshPro>().text =
                    currentTutorialPhase.texts[currentTutorialPhase.currentTextIndex];

                if (currentTutorialPhase.currentTextIndex == currentTutorialPhase.texts.Length - 1)
                {
                    if (currentPhase == tutorialPhases.Length - 1) textNext.text = "A: Finish Tutorial";
                    else textNext.text = "A: Next Phase";
                }
                else textNext.text = "A: Next";

                if (!text.activeSelf)
                {
                    text.SetActive(true);
                }
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

        text.SetActive(false);
    }
}

[Serializable]
public class TutorialPhase
{
    public string[] texts;
    public int currentTextIndex = 0;
    public TutorialPhaseChecker checker = new TutorialPhaseChecker();
    public bool phaseDone => checker.PhaseDone();
}

[Serializable]
public class TutorialPhaseChecker
{
    public PickingUpChecker pickingUpChecker = new PickingUpChecker();
    public WalkingChecker walkingChecker = new WalkingChecker();
    public ComeBackChecker comeBackChecker = new ComeBackChecker();

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
    public bool includedInPhase = false;
}

[Serializable]
public class PickingUpChecker : Checker
{
    public OVRInput.Axis1D pickingButton = OVRInput.Axis1D.PrimaryHandTrigger;
    public bool pickingButtonPressed = false;

    public bool picked = false;
    public bool dropped = false;

    public bool PickingDone => pickingButtonPressed && picked && dropped;
}

[Serializable]
public class WalkingChecker : Checker
{
    public OVRInput.RawAxis2D movingStick = OVRInput.RawAxis2D.LThumbstick;
    public bool movingStickUsed = false;

    public OVRInput.RawAxis2D rotationStick = OVRInput.RawAxis2D.RThumbstick;
    public bool rotationStickUsed = false;

    public bool walkingDone => movingStickUsed && rotationStickUsed;
}

[Serializable]
public class ComeBackChecker : Checker
{
    public bool backNearTutorialMonkey = false;
}