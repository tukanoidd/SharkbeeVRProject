using System;
using System.Collections;
using System.Collections.Generic;
using Monkeys;
using UnityEngine;
using TMPro;

public class GrammarMonkey : Monkey
{
    public MonkeyAnswer[] monkey1Answers;
    public MonkeyAnswer[] monkey2Answers;
    private GrammarPlayer grammarPlayer;
    public int currentMonkey1AnswerIndex = 0;
    public int currentMonkey2AnswerIndex = 0;
    public float grammarMonkeyMinigameArea = 10f;

    protected override void Start()
    {
        base.Start();

        grammarPlayer = player.GetComponent<GrammarPlayer>();
    }

    private void Update()
    { 
        if (grammarPlayer.grammarStarted && !grammarPlayer.grammarDone)
        {
            var monkey1Answer =  monkey1Answers[currentMonkey1AnswerIndex];
            dialogText.text = monkey1Answer.texts[monkey1Answer.currentTextIndex];
            var monkey2Answer = monkey2Answers[currentMonkey2AnswerIndex];
            dialogNextText.text = monkey2Answer.texts[monkey2Answer.currentTextIndex];
        }
    }
    
    
    
   [Serializable] 
   public class MonkeyAnswer
        {
            public string [] texts;
            public int currentTextIndex = 0;
            public bool isRight;
            public string explanation;
           
        }

   [Serializable]
   public class CheckingGrammar
   {
       public OVRInput.Axis1D chooseRight = OVRInput.Axis1D.SecondaryIndexTrigger;
       public OVRInput.Axis1D chooseLeft = OVRInput.Axis1D.PrimaryIndexTrigger;
       public bool chosenRight = false;
       public bool chosenLeft = false;

   }
   
   
}
