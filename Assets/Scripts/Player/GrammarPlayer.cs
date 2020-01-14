using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Monkeys;
using UnityEngine;

public class GrammarPlayer : MinigamesPlayer
{
    [HideInInspector] public bool grammarStarted = false;
    private bool inGrammarMinigameArea = false;
    public TutorialMonkey tutorialMonkey;
    public GrammarMonkey grammarMonkey;
    
    

    


    protected override void Start()
    {
        base.Start();
        grammarMonkey = FindObjectOfType<GrammarMonkey>();
    }
    
    private void Update()
    {
        if (grammarMonkey != null)
        {
            if (tutorialMonkey.teleportedToIslandMinigame && !grammarDone)
            {
                CheckGrammarMinigameDistance();

                if (inGrammarMinigameArea && !grammarStarted) grammarStarted = true;

                
                
                /*if (CheckIndexes())
                {
                    grammarDone = true;
                }*/
            
            }   
        }
    }

    bool CheckIndexes()
    {
        return grammarMonkey.currentMonkeyAnswerIndex + 1 >= grammarMonkey.monkeyAnswers.Length;
    }
    
    void CheckGrammarMinigameDistance()
    {
        float playerGrammarMonkeysDistance = Vector3.Distance(transform.position, grammarMonkey.transform.position);

        inGrammarMinigameArea = playerGrammarMonkeysDistance <= grammarMonkey.grammarMonkeyMinigameArea;
    }
}
