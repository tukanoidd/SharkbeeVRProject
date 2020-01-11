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
   // public GrammarMonkey grammarMonkey;

    


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

                if (OVRInput.GetDown(nextTextButton) && !grammarMonkeys.CheckIndexes())
                {
                    grammarMonkeys.grammarMonkey1.currentMonkeyAnswerIndex = 0;
                    grammarMonkeys.grammarMonkey2.currentMonkeyAnswerIndex = 0;
                }

               /* for (grammarMonkey.currentMonkeyAnswerIndex i = 0; i < 10; i ++)
                {
                    (if (grammarMonkeys.grammarMonkey1.isRight = true && OVR Get trigger Down)) 
                    {
                        Monkey1:
                        Congrat!;
                        ReadKey();
                        i++;
                    }
                    (if monkey1.right = false && OVR get trigger down)
                    {
                        monkey2 show explanation;
                        ReadKey();

                    }
                    (if monkey2.right = true && OVR Get other trigger Down) 
                    {
                        Monkey2: Congrat!
                        ReadKey()
                        i++
                    }
                    (if monkey2.right = false && OVR get other trigger down)
                    {
                        monkey1 show explanation
                        ReadKey()
                    }	
                }
                */


                if (OVRInput.GetDown(nextTextButton) && grammarMonkeys.CheckIndexes())
                {
                    grammarDone = true;
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
