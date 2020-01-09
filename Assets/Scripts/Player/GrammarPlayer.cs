using System;
using System.Collections;
using System.Collections.Generic;
using Monkeys;
using UnityEngine;

public class GrammarPlayer : MinigamesPlayer
{
    [HideInInspector] public bool grammarStarted = false;
    private bool inGrammarMinigameArea = false;
    public TutorialMonkey tutorialMonkey;
    public GrammarMonkeys grammarMonkeys;
    
    
    
    protected override void Start()
    {
        base.Start();
        grammarMonkeys = FindObjectOfType<GrammarMonkeys>();
    }
    
    private void Update()
    {
        if (grammarMonkeys != null)
        {
            if (tutorialMonkey.teleportedToIslandMinigame && !grammarDone)
            {
                CheckGrammarMinigameDistance();

                if (inGrammarMinigameArea && !grammarStarted) grammarStarted = true;

                if (OVRInput.GetDown(nextTextButton))
                {
                    if (grammarMonkeys.CheckIndexes())
                    {
                        grammarDone = true;
                    }
                    else
                    {
                        grammarMonkeys.grammarMonkey1.currentMonkeyAnswerIndex++;
                        grammarMonkeys.grammarMonkey2.currentMonkeyAnswerIndex++;
                    }
                }
            
            }   
        }
    }

    void CheckGrammarMinigameDistance()
    {
        float playerGrammarMonkeysDistance = Vector3.Distance(transform.position, grammarMonkeys.transform.position);

        inGrammarMinigameArea = playerGrammarMonkeysDistance <= grammarMonkeys.grammarMonkeyMinigameArea;
    }
}
