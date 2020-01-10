using System.Linq;
using Monkeys;
using UnityEngine;

namespace Player
{
    public class TutorialPlayer : MinigamesPlayer
    {
        [HideInInspector] public bool tutorialStarted = false;

        private bool inTutorial = false;
        public bool inTutorialArea = false;

        private TutorialMonkey tutorialMonkey;

        private OVRGrabber[] grabbers;

        protected override void Start()
        {
            base.Start();

            tutorialMonkey = monkey.GetComponent<TutorialMonkey>();
            grabbers = GetComponentsInChildren<OVRGrabber>();
        }

        void Update()
        {
            if (!tutorialDone)
            {
                CheckTutorialDistance();

                if (inTutorial && !tutorialStarted) tutorialStarted = true;

                TutorialMonkey.TutorialPhases phase = tutorialMonkey.currentPhase;
                if ((phase != TutorialMonkey.TutorialPhases.Picking && (inTutorial || inTutorialArea)) ||
                    (phase == TutorialMonkey.TutorialPhases.Picking && inTutorial))
                {
                    TutorialCheck();
                }
            }
            else if (!tutorialMonkey.teleportedToIslandMinigame)
            {
                tutorialMonkey.TeleportToMinigame();
            }
        }

        void CheckTutorialDistance()
        {
            float playerTutorialMonkeyDistance =
                Vector3.Distance(transform.position, tutorialMonkey.transform.position);

            inTutorialArea = playerTutorialMonkeyDistance <= tutorialMonkey.tutorialAreaDistance;
            inTutorial = playerTutorialMonkeyDistance <= tutorialMonkey.nearTutorialMonkeyDistance;
        }

        void TutorialCheck()
        {
            switch (tutorialMonkey.currentPhase)
            {
                case TutorialMonkey.TutorialPhases.Picking:
                    var pickingPhase = tutorialMonkey.tutorialPhasesInfo.pickingPhaseInfo;
                    CheckDialogControls(pickingPhase);
                    break;
                case TutorialMonkey.TutorialPhases.Moving:
                    var movingPhase = tutorialMonkey.tutorialPhasesInfo.movingPhaseInfo;
                    CheckDialogControls(movingPhase);
                    break;
                case TutorialMonkey.TutorialPhases.ComingBack:
                    var comingBackPhase = tutorialMonkey.tutorialPhasesInfo.comingBackPhaseInfo;
                    CheckDialogControls(comingBackPhase);
                    break;
            }

            UseTutorialCheckers();
        }

        void CheckDialogControls(TutorialMonkey.PickingPhaseInfo phase)
        {
            if (OVRInput.GetDown(nextTextButton))
            {
                if (phase.currentTextIndex + 1 >= phase.texts.Length)
                {
                    if (phase.checker.CheckPhaseDone)
                    {
                        NextPhase();
                    }
                }
                else
                {
                    phase.currentTextIndex++;
                }
            }

            if (OVRInput.GetDown(backTextButton))
            {
                phase.currentTextIndex =
                    Mathf.Clamp(phase.currentTextIndex - 1, 0, phase.texts.Length - 1);
            }
        }

        void CheckDialogControls(TutorialMonkey.MovingPhaseInfo phase)
        {
            if (OVRInput.GetDown(nextTextButton))
            {
                if (phase.currentTextIndex + 1 >= phase.texts.Length)
                {
                    if (phase.checker.CheckPhaseDone)
                    {
                        NextPhase();
                    }
                }
                else
                {
                    phase.currentTextIndex++;
                }
            }

            if (OVRInput.GetDown(backTextButton))
            {
                phase.currentTextIndex =
                    Mathf.Clamp(phase.currentTextIndex - 1, 0, phase.texts.Length - 1);
            }
        }

        void CheckDialogControls(TutorialMonkey.ComingBackPhaseInfo phase)
        {
            if (inTutorial)
            {
                if (OVRInput.GetDown(nextTextButton))
                {
                    if (phase.currentTextIndex + 1 >= phase.texts.Length && phase.checker.CheckPhaseDone) NextPhase();
                    else phase.currentTextIndex++;
                }

                if (OVRInput.GetDown(backTextButton))
                {
                    phase.currentTextIndex =
                        Mathf.Clamp(phase.currentTextIndex - 1, 0, phase.texts.Length - 1);
                }
            }
        }

        void NextPhase()
        {
            if (tutorialMonkey.currentPhase == TutorialMonkey.TutorialPhases.ComingBack) EndTutorial();
            else
            {
                switch (tutorialMonkey.currentPhase)
                {
                    case TutorialMonkey.TutorialPhases.Picking:
                        tutorialMonkey.currentPhase = TutorialMonkey.TutorialPhases.Moving;
                        break;
                    case TutorialMonkey.TutorialPhases.Moving:
                        tutorialMonkey.currentPhase = TutorialMonkey.TutorialPhases.ComingBack;
                        break;
                }
            }
        }

        void EndTutorial()
        {
            tutorialDone = true;
            if (controller.lockedMovement) controller.lockedMovement = false;
            tutorialMonkey.dialogTextObject.SetActive(false);
        }

        void UseTutorialCheckers()
        {
            switch (tutorialMonkey.currentPhase)
            {
                case TutorialMonkey.TutorialPhases.Picking:
                    controller.lockedMovement = true;
                    TutorialPickupCheck(tutorialMonkey.tutorialPhasesInfo.pickingPhaseInfo.checker);
                    break;
                case TutorialMonkey.TutorialPhases.Moving:
                    controller.lockedMovement = false;
                    TutorialMovingCheck(tutorialMonkey.tutorialPhasesInfo.movingPhaseInfo.checker);
                    break;
                case TutorialMonkey.TutorialPhases.ComingBack:
                    controller.lockedMovement = false;
                    var phase = tutorialMonkey.tutorialPhasesInfo.comingBackPhaseInfo;
                    TutorialComingBackCheck(phase, phase.checker);
                    break;
            }
        }

        void TutorialPickupCheck(TutorialMonkey.PickingPhaseChecker checker)
        {
            CheckPicking(checker);
        }

        void TutorialMovingCheck(TutorialMonkey.MovingPhaseChecker checker)
        {
            CheckMoving(checker);
            CheckPicking(checker);
        }

        void TutorialComingBackCheck(TutorialMonkey.ComingBackPhaseInfo phase, TutorialMonkey.ComingBackChecker checker)
        {
            if (inTutorial) checker.backNearTutorialMonkey = true;
            else checker.backNearTutorialMonkey = false;
        }

        void CheckPicking(TutorialMonkey.Checker checker)
        {
            if (OVRInput.Get(checker.pickingButtonL) > 0 || OVRInput.Get(checker.pickingButtonR) > 0)
                checker.pickingButtonPressed = true;

            if (grabbers.Any(grabber => grabber.grabbedObject != null)) checker.picked = true;
            else if (checker.picked) checker.dropped = true;
        }

        void CheckMoving(TutorialMonkey.Checker checker)
        {
            if (OVRInput.Get(checker.movingStick) != Vector2.zero) checker.movingStickUsed = true;
            if (OVRInput.Get(checker.rotationStick) != Vector2.zero) checker.rotationStickUsed = true;
        }
    }
}