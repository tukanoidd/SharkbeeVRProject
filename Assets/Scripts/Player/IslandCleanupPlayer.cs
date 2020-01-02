using Monkeys;
using UnityEngine;

namespace Player
{
    public class IslandCleanupPlayer : MinigamesPlayer
    {
        public bool islandCleanupStarted = false;

        private bool inMinigameArea = false;

        private IslandCleanupMonkey islandCleanupMonkey;
        private TutorialMonkey tutorialMonkey;

        protected override void Start()
        {
            base.Start();

            islandCleanupMonkey = monkey.GetComponent<IslandCleanupMonkey>();
            tutorialMonkey = monkey.GetComponent<TutorialMonkey>();
        }

        private void Update()
        {
            if (!islandCleanupDone && tutorialMonkey.teleportedToIslandMinigame)
            {
                CheckMinigameDistance();

                if (inMinigameArea)
                {
                    if (!islandCleanupStarted) islandCleanupStarted = true;
                    if (islandCleanupStarted) IslandCleanupCheck();
                }
            } else if (islandCleanupMonkey.dialogTextObject.activeSelf)
            {
                islandCleanupMonkey.dialogTextObject.SetActive(false);
            }
        }

        void CheckMinigameDistance()
        {
            float playerIslandCleanupMonkeyDistance =
                Vector3.Distance(transform.position, islandCleanupMonkey.transform.position);

            inMinigameArea = playerIslandCleanupMonkeyDistance <= islandCleanupMonkey.minigameAreaDistance;
        }

        void IslandCleanupCheck()
        {
            if (islandCleanupMonkey.currentPhase != IslandCleanupMonkey.IslandCleanupPhases.Cleaning)
            {
                if (OVRInput.GetDown(nextTextButton))
                {
                    IslandCleanupMonkey.IslandCleanupPhase phase = null;

                    switch (islandCleanupMonkey.currentPhase)
                    {
                        case IslandCleanupMonkey.IslandCleanupPhases.Start:
                            phase = islandCleanupMonkey.islandCleanupPhasesInfo.startPhase;
                            if (phase.currentTextIndex >= phase.texts.Length - 1) StartCleaning();
                            else phase.currentTextIndex++;
                            
                            break;
                        case IslandCleanupMonkey.IslandCleanupPhases.End:
                            phase = islandCleanupMonkey.islandCleanupPhasesInfo.endPhase;
                            if (phase.currentTextIndex >= phase.texts.Length - 1) EndMinigame();
                            else phase.currentTextIndex++;
                            
                            break;
                    }
                }

                if (OVRInput.GetDown(backTextButton))
                {
                    IslandCleanupMonkey.IslandCleanupPhase phase = null;

                    switch (islandCleanupMonkey.currentPhase)
                    {
                        case IslandCleanupMonkey.IslandCleanupPhases.Start:
                            phase = islandCleanupMonkey.islandCleanupPhasesInfo.startPhase;
                            break;
                        case IslandCleanupMonkey.IslandCleanupPhases.End:
                            phase = islandCleanupMonkey.islandCleanupPhasesInfo.endPhase;
                            break;
                    }
                    
                    phase.currentTextIndex = Mathf.Clamp(phase.currentTextIndex - 1, 0, phase.texts.Length - 1);
                }
            }

            void StartCleaning()
            {
                islandCleanupMonkey.currentPhase = IslandCleanupMonkey.IslandCleanupPhases.Cleaning;
            }

            void EndMinigame()
            {
                islandCleanupDone = true;
            }
        }
    }
}