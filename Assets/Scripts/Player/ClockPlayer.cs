using Monkeys;
using UnityEngine;

namespace Player
{
    public class ClockPlayer : MinigamesPlayer
    {
        [HideInInspector] public bool clockStarted = false;

        [HideInInspector] public bool inClockMinigame = false;

        private ClockMonkey clockMonkey;
        [SerializeField] private TutorialMonkey tutorialMonkey;

        protected override void Start()
        {
            base.Start();

            clockMonkey = monkey.GetComponent<ClockMonkey>();
        }

        void Update()
        {
            if (tutorialMonkey.teleportedToIslandMinigame && !clockDone)
            {
                CheckMinigameDistance();

                if (inClockMinigame)
                {
                    if (!clockStarted) clockStarted = true;
                    if (clockStarted)
                    {
                        ClockMinigameCheck();
                    }
                }
            }
        }

        void CheckMinigameDistance()
        {
            float playerClockMonkeyDistance = Vector3.Distance(transform.position, clockMonkey.transform.position);

            inClockMinigame = playerClockMonkeyDistance <= clockMonkey.clockMinigameAreaDistance;
        }

        void ClockMinigameCheck()
        {
            if (OVRInput.GetDown(nextTextButton))
            {
                switch (clockMonkey.currentPhase)
                {
                    case ClockMonkey.ClockMinigamePhases.Start:
                        var startPhase = clockMonkey.phases.startPhase;

                        if (startPhase.currentTextIndex >= startPhase.texts.Length - 1) NextPhase();
                        else startPhase.currentTextIndex++;
                        break;
                    case ClockMonkey.ClockMinigamePhases.End:
                        var endPhase = clockMonkey.phases.endPhase;

                        if (endPhase.currentTextIndex >= endPhase.texts.Length - 1) EndMinigame();
                        else endPhase.currentTextIndex++;
                        
                        break;
                }
            }

            if (OVRInput.GetDown(backTextButton))
            {
                switch (clockMonkey.currentPhase)
                {
                    case ClockMonkey.ClockMinigamePhases.Start:
                        var startPhase = clockMonkey.phases.startPhase;
                        var startCurrentIndex = startPhase.currentTextIndex;

                        clockMonkey.phases.startPhase.currentTextIndex =
                            Mathf.Clamp(startCurrentIndex - 1, 0, startPhase.texts.Length - 1);
                        break;
                    case ClockMonkey.ClockMinigamePhases.End:
                        var endPhase = clockMonkey.phases.endPhase;
                        var endCurrentIndex = endPhase.currentTextIndex;

                        clockMonkey.phases.endPhase.currentTextIndex =
                            Mathf.Clamp(endCurrentIndex - 1, 0, endPhase.texts.Length - 1);
                        break;
                }
            }
        }

        public void NextPhase()
        {
            switch (clockMonkey.currentPhase)
            {
                case ClockMonkey.ClockMinigamePhases.Start:
                    clockMonkey.currentPhase = ClockMonkey.ClockMinigamePhases.Minigame;
                    clockMonkey.MinigameStarted();
                    break;
                case ClockMonkey.ClockMinigamePhases.Minigame:
                    clockMonkey.currentPhase = ClockMonkey.ClockMinigamePhases.End;
                    break;
            }
            if (clockMonkey.currentPhase == ClockMonkey.ClockMinigamePhases.Start)
                clockMonkey.currentPhase = ClockMonkey.ClockMinigamePhases.Minigame;
        }

        void EndMinigame()
        {
            clockDone = true;
        }
    }
}