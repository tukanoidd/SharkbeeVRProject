using System.Linq;
using UnityEngine;

namespace Player
{
    public class Character_TutorialBehavior : MonoBehaviour
    {
        private bool inTutorial = false;
        private bool inTutorialArea = false;
        public bool tutorialStarted = false;
        public bool tutorialDone = false;

        [SerializeField] private TutorialMonkey tutorialMonkey;

        [SerializeField] private OVRInput.Button tutorialBackTextButton = OVRInput.Button.One;
        [SerializeField] private OVRInput.Button tutorialNextTextButton = OVRInput.Button.Two;

        private Character character;

        void Start()
        {
            character = GetComponent<Character>();
        }

        void Update()
        {
            if (!tutorialDone)
            {
                CheckTutorialDistance();

                if (inTutorial && !tutorialStarted)
                {
                    tutorialStarted = true;
                }

                if ((tutorialMonkey.currentPhase > 0 && (inTutorial || inTutorialArea)) ||
                    (tutorialMonkey.currentPhase == 0 && inTutorial))
                {
                    TutorialCheck();
                }
            }
            else
            {
                if (!tutorialMonkey.teleported)
                {
                    NextPhase();
                }
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

            if (OVRInput.GetDown(tutorialBackTextButton))
            {
                Debug.Log("back button pressed");
                currentPhase.currentTextIndex =
                    Mathf.Clamp(currentPhase.currentTextIndex - 1, 0, currentPhase.texts.Length - 1);
            }

            UseTutorialCheckers();
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
                if (currentPhaseCheckers.walkingChecker.includedInPhase) character.controller.lockedMovement = false;
                else character.controller.lockedMovement = true;

                TutorialPickupCheck(currentPhaseCheckers.pickingUpChecker);
            }

            TutorialWalkCheck(currentPhaseCheckers.walkingChecker);

            TutorialComeBackCheck(currentPhaseCheckers.comeBackChecker);
        }

        void TutorialPickupCheck(PickingUpChecker checker)
        {
            if (OVRInput.Get(checker.pickingButton) > 0) checker.pickingButtonPressed = true;

            if (GetComponentsInChildren<OVRGrabber>().Where(grabber => grabber.grabbedObject != null).ToArray().Length >
                0)
                checker.picked = true;
            else if (checker.picked) checker.dropped = true;
        }

        void TutorialWalkCheck(WalkingChecker checker)
        {
            if (checker.includedInPhase)
            {
                character.controller.lockedMovement = false;

                if (OVRInput.Get(checker.movingStick) != Vector2.zero) checker.movingStickUsed = true;
                if (OVRInput.Get(checker.rotationStick) != Vector2.zero) checker.rotationStickUsed = true;
            }
        }

        void TutorialComeBackCheck(ComeBackChecker checker)
        {
            if (checker.includedInPhase)
            {
                if (!inTutorial)
                {
                    character.controller.lockedMovement = false;
                }
                else
                {
                    checker.backNearTutorialMonkey = true;
                    var currentPhase = tutorialMonkey.tutorialPhases[tutorialMonkey.currentPhase];
                    if (currentPhase.currentTextIndex == 0) currentPhase.currentTextIndex++;
                }
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

            if (character.controller.lockedMovement) character.controller.lockedMovement = false;
            tutorialMonkey.text.gameObject.SetActive(false);
        }
    }
}