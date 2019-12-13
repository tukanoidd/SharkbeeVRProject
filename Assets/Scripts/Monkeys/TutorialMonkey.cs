using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMonkey : MonoBehaviour
{
    public bool tutorialStarted = false;
    public int currentPhase = 0; 
    
    [SerializeField] private Transform miniGamePosition;
    [SerializeField] private GameObject[] tutorialExitColliders;

    [SerializeField] private TutorialPhase[] tutorialPhases;
    [SerializeField] private GameObject text;

    private Character player; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
class TutorialPhase
{
    public string[] texts;
    public TutorialPhaseChecker checker;
    public bool phaseDone => checker.PhaseDone();
    public bool tutorialDone;
    
    public bool restrictMovementOnPhaseStart;
    public bool restrictMovementOnPhaseEnd;
}

[Serializable]
class TutorialPhaseChecker
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
        comeBackCheck = comeBackChecker.includedInPhase ? comeBackChecker.backInTutorialCollider : true; 

        return pickChecker && walkChecker && comeBackCheck;
    }
}

[Serializable]
class PickingUpChecker
{
    public bool includedInPhase;
    
    public OVRInput.Button pickingButton;
    public bool pickingButtonPressed = false;
    
    public bool picked = false;
    public bool dropped = false;

    public bool PickingDone => pickingButtonPressed && picked && dropped;
}

[Serializable]
class WalkingChecker
{
    public bool includedInPhase;
    
    public OVRInput.Axis2D movingStick;
    public bool movingStickUsed = false;
    
    public OVRInput.Axis2D rotationStick;
    public bool rotationStickUsed = false;

    public bool walkingDone => movingStickUsed && rotationStickUsed;
}

[Serializable]
class ComeBackChecker
{
    public bool includedInPhase;
    
    public bool backInTutorialCollider;
}