using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Monkeys
{
    public class ClockMonkey : Monkey
    {
        public enum ClockMinigamePhases
        {
            Start,
            Minigame,
            End
        }

        [HideInInspector] public ClockMinigamePhases currentPhase = ClockMinigamePhases.Start;
        public ClockPhasesInfo phases;
        
        public float clockMinigameAreaDistance = 10;

        public int neededPoints = 3;
        [HideInInspector] public int currentPoints = 0;

        private ClockPlayer clockPlayer;
        [SerializeField] private TutorialMonkey tutorialMonkey;

        protected override void Start()
        {
            base.Start();

            clockPlayer = player.GetComponent<ClockPlayer>();
        }

        private void Update()
        {
            if (clockPlayer != null && player != null)
            {
                if (tutorialMonkey.teleportedToIslandMinigame && clockPlayer.clockStarted && !clockPlayer.clockDone)
                {   
                    if (!dialogTextObject.activeSelf) dialogTextObject.SetActive(true);
                    
                    var nextText = "A: Next";
                    var nextBackTextsActive = true;
                    
                    switch (currentPhase)
                    {
                        case ClockMinigamePhases.Start:
                            var startPhase = phases.startPhase;
                            var startTexts = startPhase.texts;
                            var startCurrentTextIndex = startPhase.currentTextIndex;
                            
                            dialogText.text = startTexts[startCurrentTextIndex];

                            if (startCurrentTextIndex == startTexts.Length - 1) nextText = "A: Start Minigame";
                            break;
                        case ClockMinigamePhases.Minigame:
                            nextBackTextsActive = false;
                            break;
                        case ClockMinigamePhases.End:
                            var endPhase = phases.endPhase;
                            var endTexts = endPhase.texts;
                            var endCurrentTextIndex = endPhase.currentTextIndex;

                            dialogText.text = endTexts[endCurrentTextIndex];

                            if (endCurrentTextIndex == endTexts.Length - 1) nextText = "A: Finish Minigame";
                            break;
                    }

                    dialogNextText.text = nextText;
                    dialogNextTextObject.SetActive(nextBackTextsActive);
                    dialogBackTextObject.SetActive(nextBackTextsActive);
                    
                }
                else if (dialogTextObject.activeSelf && clockPlayer.clockDone) dialogTextObject.SetActive(false);
            }
        }

        public void MinigameStarted()
        {
            var minigamePhase = phases.minigamePhase;
            dialogText.text = minigamePhase.times[minigamePhase.currentTimeIndex].pronunciation;
        }
        
        public void RightTime(int currentTimeIndex, TimeInfo currentTime)
        {
            currentPoints++;
            
            if (currentPoints == neededPoints) clockPlayer.NextPhase();
            else
            {
                phases.minigamePhase.times.RemoveAt(currentTimeIndex);
                
                var newTimeIndex = Random.Range(0, phases.minigamePhase.times.Count);
                phases.minigamePhase.currentTimeIndex = newTimeIndex;

                dialogText.text = currentTime.rightPronunciation + " " + phases.minigamePhase.times[newTimeIndex].pronunciation;
            }
        }

        public void WrongTime(TimeInfo currentTime)
        {
            dialogText.text = currentTime.wrongPronunciation;
        }

        [Serializable]
        public class TimeInfo
        {
            public int hours;
            public int minutes;
            
            public string pronunciation;
            public string rightPronunciation;
            public string wrongPronunciation;
        }

        [Serializable]
        public class MinigamePhaseInfo
        {
            public List<TimeInfo> times;
            public int currentTimeIndex;
        }
        
        [Serializable]
        public class ClockPhaseInfo
        {
            public string[] texts;
            public int currentTextIndex;
        } 

        [Serializable]
        public class ClockPhasesInfo
        {
            public ClockPhaseInfo startPhase;
            public MinigamePhaseInfo minigamePhase;
            public ClockPhaseInfo endPhase;
        }
    }
}