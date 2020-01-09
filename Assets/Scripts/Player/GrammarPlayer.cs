using System;
using System.Collections;
using System.Collections.Generic;
using Monkeys;
using UnityEngine;

public class GrammarPlayer : MinigamesPlayer
{
    [HideInInspector] public bool grammarStarted = false;
    private bool inGrammarMinigameArea = false;
    public GrammarMonkey grammarMonkey;
    public TutorialMonkey tutorialMonkey;
    public GrammarMonkeys grammarMonkeys;
    
    
    
    protected override void Start()
    {
        base.Start();
        grammarMonkeys = monkey.GetComponent<GrammarMonkeys>();
    }
    
    private void Update()
    {
        if (tutorialMonkey.teleportedToIslandMinigame && !grammarDone)
        {
            CheckGrammarMinigameDistance();

            if (inGrammarMinigameArea && !grammarStarted) grammarStarted = true;

            if (OVRInput.GetDown(nextTextButton))
            {
                if (grammarMonkey.currentMonkey1AnswerIndex + 1 >= grammarMonkey.monkey1Answers.Length)
                {
                    grammarDone = true;
                }
                else
                {
                    grammarMonkey.currentMonkey1AnswerIndex++;
                    grammarMonkey.currentMonkey2AnswerIndex++;
                }
            }
            
        }    
    }

    void CheckGrammarMinigameDistance()
    {
        float playerGrammarMonkeysDistance = Vector3.Distance(transform.position, grammarMonkeys.transform.position);

        inGrammarMinigameArea = playerGrammarMonkeysDistance <= grammarMonkey.grammarMonkeyMinigameArea;
    }
}
