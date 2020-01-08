using System.Collections;
using System.Collections.Generic;
using Monkeys;
using UnityEngine;

public class GrammarPlayer : MinigamesPlayer
{
    public bool inGrammarMinigameArea = false;
    
    public GrammarMonkeys grammarMonkeys;
    public TutorialMonkey tutorialMonkey;
    public float grammarMonkeyMinigameArea = 10f;
    
    protected override void Start()
    {
        GameObject.FindWithTag ("DialogueMonkey1").SetActive(false);
        base.Start();
    }

    private void Update()
    {
        if (tutorialMonkey.teleportedToIslandMinigame && !grammarDone)
        {
            CheckGrammarMinigameDistance();

            if (inGrammarMinigameArea)
            {
                GameObject.FindWithTag( "DialogueMonkey1").SetActive(true);
            }
        }    
    }

    void CheckGrammarMinigameDistance()
    {
        var playerGrammarMonkeysDistance = Vector3.Distance(transform.position, grammarMonkeys.transform.position);

        inGrammarMinigameArea = playerGrammarMonkeysDistance <= grammarMonkeyMinigameArea;
    }
}
