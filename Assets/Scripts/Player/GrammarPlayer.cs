using System.Collections;
using System.Collections.Generic;
using Monkeys;
using UnityEngine;

public class GrammarPlayer : MinigamesPlayer
{
    public bool inGrammarMinigameArea = false;
    
    public GrammarMonkeys grammarMonkeys;
    public TutorialMonkey tutorialMonkey;
    
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (tutorialMonkey.teleportedToIslandMinigame && !grammarDone)
        {
            CheckGrammarMinigameDistance();

            if (inGrammarMinigameArea)
            {
                
            }
        }    
    }

    void CheckGrammarMinigameDistance()
    {
        var playerGrammarMonkeysDistance = Vector3.Distance(transform.position, grammarMonkeys.transform.position);

        inGrammarMinigameArea = playerGrammarMonkeysDistance <= grammarMonkeys.grammarMonkeyMinigameArea;
    }
}
