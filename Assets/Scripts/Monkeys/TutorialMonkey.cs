using System;
using System.Linq;
using Player;
using TMPro;
using UnityEngine;

namespace Monkeys
{
    public class TutorialMonkey : Monkey
    {
        public enum TutorialPhases
        {
            Picking,
            Moving,
            ComingBack
        }

        public TutorialPhases currentPhase = TutorialPhases.Picking;
        public TutorialPhasesInfo tutorialPhasesInfo;

        public float tutorialAreaDistance = 10;
        public float nearTutorialMonkeyDistance;

        [SerializeField] private GameObject[] tutorialExitColliders;

        [SerializeField] private Transform islandCleanupMinigamePosition;
        public bool teleportedToIslandMinigame;

        private TutorialPlayer tutorialPlayer;

        private Renderer tutorialMonkeyRenderer;

        protected override void Start()
        {
            base.Start();

            tutorialMonkeyRenderer = GetComponent<Renderer>();

            tutorialPlayer = player.GetComponent<TutorialPlayer>();

            if (tutorialExitColliders != null)
            {
                if (tutorialExitColliders.Length > 0)
                {
                    tutorialAreaDistance = Mathf.Max(tutorialExitColliders.Select(tutorialExitCollider =>
                        Vector3.Distance(tutorialExitCollider.transform.position, transform.position)).ToArray());
                }
            }

            nearTutorialMonkeyDistance = tutorialAreaDistance / 2.5f;
        }

        private void Update()
        {
            if (tutorialPlayer != null && player != null)
            {
                if (tutorialPlayer.tutorialStarted && !player.tutorialDone && !teleportedToIslandMinigame)
                {
                    PhaseInfo phase = null;
                    switch (currentPhase)
                    {
                        case TutorialPhases.Picking:
                            phase = tutorialPhasesInfo.pickingPhaseInfo;
                            break;
                        case TutorialPhases.Moving:
                            phase = tutorialPhasesInfo.movingPhaseInfo;
                            break;
                        case TutorialPhases.ComingBack:
                            phase = tutorialPhasesInfo.comingBackPhaseInfo;
                            break;
                    }

                    if (phase != null)
                    {
                        dialogText.text = phase.texts[phase.currentTextIndex];
                        if (phase.currentTextIndex == phase.texts.Length - 1)
                        {
                            if (phase is ComingBackPhaseInfo) dialogNextText.text = "A: Finish Tutorial";
                            else dialogNextText.text = "A: Next Phase";
                        }
                        else dialogNextText.text = "A: Next";
                    }

                    if (!dialogTextObject.activeSelf)
                    {
                        dialogTextObject.SetActive(true);
                    }
                }
            }
        }

        public void TeleportToMinigame()
        {
            if (!tutorialMonkeyRenderer.isVisible)
            {
                transform.position = islandCleanupMinigamePosition.position;
                teleportedToIslandMinigame = true;

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
        public class Checker
        {
            public OVRInput.Axis1D pickingButton = OVRInput.Axis1D.PrimaryHandTrigger;
            public bool pickingButtonPressed = false;
            public bool picked = false;
            public bool dropped = false;

            public OVRInput.RawAxis2D movingStick = OVRInput.RawAxis2D.LThumbstick;
            public bool movingStickUsed = false;
            public OVRInput.RawAxis2D rotationStick = OVRInput.RawAxis2D.RThumbstick;
            public bool rotationStickUsed = false;

            public bool backNearTutorialMonkey = false;
        }

        [Serializable]
        public class PickingPhaseChecker : Checker
        {
            public bool CheckPhaseDone => pickingButtonPressed && picked && dropped;
        }

        [Serializable]
        public class MovingPhaseChecker : Checker
        {
            public bool CheckPhaseDone =>
                pickingButtonPressed && picked && dropped && movingStickUsed && rotationStickUsed;
        }

        [Serializable]
        public class ComingBackChecker : Checker
        {
            public bool CheckPhaseDone => backNearTutorialMonkey;
        }

        [Serializable]
        public class PhaseInfo
        {
            public string[] texts;
            public int currentTextIndex = 0;
        }

        [Serializable]
        public class PickingPhaseInfo : PhaseInfo
        {
            public PickingPhaseChecker checker;
        }

        [Serializable]
        public class MovingPhaseInfo : PhaseInfo
        {
            public MovingPhaseChecker checker;
        }

        [Serializable]
        public class ComingBackPhaseInfo : PhaseInfo
        {
            public ComingBackChecker checker;
        }

        [Serializable]
        public class TutorialPhasesInfo
        {
            public PickingPhaseInfo pickingPhaseInfo;
            public MovingPhaseInfo movingPhaseInfo;
            public ComingBackPhaseInfo comingBackPhaseInfo;
        }
    }
}