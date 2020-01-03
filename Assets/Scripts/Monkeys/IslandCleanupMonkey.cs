using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using TMPro;
using UnityEngine;

namespace Monkeys
{
    public class IslandCleanupMonkey : Monkey
    {
        public enum IslandCleanupPhases
        {
            Start,
            Cleaning,
            End
        }

        [HideInInspector] public IslandCleanupPhases currentPhase = IslandCleanupPhases.Start;
        public IslandCleanupPhasesInfo islandCleanupPhasesInfo;

        [HideInInspector] public Dictionary<string, bool> items = new Dictionary<string, bool>();
        public GameObject itemsListTextObject;
        public TextMeshPro itemsListText;

        public float minigameAreaDistance = 10;

        private TutorialMonkey tutorialMonkey;
        private IslandCleanupPlayer islandCleanupPlayer;

        protected override void Start()
        {
            base.Start();

            tutorialMonkey = GetComponent<TutorialMonkey>();
            islandCleanupPlayer = player.GetComponent<IslandCleanupPlayer>();

            foreach (var item in FindObjectsOfType<IslandCleanupItem>())
            {
                items.Add(item.name, false);
            }
        }

        private void Update()
        {
            if (islandCleanupPlayer != null && player != null)
            {
                if (islandCleanupPlayer.islandCleanupStarted && !islandCleanupPlayer.islandCleanupDone &&
                    tutorialMonkey.teleportedToIslandMinigame)
                {
                    if (!dialogTextObject.activeSelf) dialogTextObject.SetActive(true);
                    if (!itemsListTextObject.activeSelf) itemsListTextObject.SetActive(true);
                    
                    var nextText = "A: Next";
                    var nextBackTextsActive = true;
                    switch (currentPhase)
                    {
                        case IslandCleanupPhases.Start:
                            var startPhase = islandCleanupPhasesInfo.startPhase;
                            var startTexts = startPhase.texts;
                            var startCurrentTextIndex = startPhase.currentTextIndex;
                            
                            if (startCurrentTextIndex == startTexts.Length - 1) nextText = "A: Start Cleaning";

                            dialogText.text = startTexts[startCurrentTextIndex];
                            break;
                        case IslandCleanupPhases.Cleaning:
                            nextBackTextsActive = false;
                            break;
                        case IslandCleanupPhases.End:
                            var endPhase = islandCleanupPhasesInfo.endPhase;
                            var endTexts = endPhase.texts;
                            var endCurrentTextIndex = endPhase.currentTextIndex;
                            
                            if (endCurrentTextIndex == endTexts.Length - 1) nextText = "A: Finish Minigame";

                            dialogText.text = endTexts[endCurrentTextIndex];
                            break;
                    }
                    
                    dialogBackTextObject.SetActive(nextBackTextsActive);
                    dialogNextTextObject.SetActive(nextBackTextsActive);

                    dialogNextText.text = nextText;

                    CheckItemsList();
                }
                else if (dialogTextObject.activeSelf && islandCleanupPlayer.islandCleanupDone)
                {
                    dialogTextObject.SetActive(false);
                }
            }
        }

        void CheckItemsList()
        {
            if (itemsListTextObject != null && items != null)
            {
                if (items.Keys.Count > 0)
                {
                    itemsListText.text = itemsListText.text = String.Join("\n", items.Keys.Select(item =>
                        (items[item] ? "<color=green>" : "<color=white>") + item + "</color>").ToArray());
                }
            }
        }

        public void CheckItems()
        {
            if (items.Values.Count(cleaned => cleaned) == items.Count) currentPhase = IslandCleanupPhases.End;
        }
        
        [Serializable]
        public class IslandCleanupPhase
        {
            public string[] texts;
            public int currentTextIndex;
        }

        [Serializable]
        public class IslandCleanupPhasesInfo
        {
            public IslandCleanupPhase startPhase;
            public IslandCleanupPhase cleaningPhase;
            public IslandCleanupPhase endPhase;
        }
    }
}