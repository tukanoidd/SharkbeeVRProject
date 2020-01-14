/*using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Monkeys;
using UnityEngine;

// for now in GrammarMonkeys the code for the first argument is made
public class GrammarMonkeys : MonoBehaviour
{
    [HideInInspector] public GrammarPlayer grammarPlayer;
    public float grammarMonkeyMinigameArea = 10f;

    public GrammarMonkey grammarMonkey1;
    public GrammarMonkey grammarMonkey2;

    private void Update()
    {
        
    }


   

   
    public bool CheckEndReplicaTexts(GrammarMonkey.MonkeyAnswer monkeyAnswer)
    {
        return monkeyAnswer.currentTextIndex + 1 >= monkeyAnswer.texts.Length;
    }
    
    public bool CheckEndReplicaExplanation(GrammarMonkey.MonkeyAnswer monkeyExplanation)
    {
        return monkeyExplanation.currentExplanationIndex + 1 >= monkeyExplanation.explanation.Length;
    }
   
   /*public bool CheckEndReplicaExplanation(GrammarMonkey.MonkeyAnswer monkeyExplanation)
   {
       if(monkeyExplanation.currentExplanationIndex + 1 >= monkeyExplanation.explanation.Length)
       {
           return true;
       }
       else
       {
           return false;
       }
   }
    
    public bool CheckIndexes()
    {
        return CheckMonkeyAnswerIndex(grammarMonkey1) && CheckMonkeyAnswerIndex(grammarMonkey2);
    }

    bool CheckMonkeyAnswerIndex(GrammarMonkey monkey)
    {
        return monkey.currentMonkeyAnswerIndex + 1 >= monkey.monkeyAnswers.Length;
    }


   [Serializable]
    public class CheckingGrammar
    {
        public OVRInput.Axis1D chooseRight = OVRInput.Axis1D.SecondaryIndexTrigger;
        public OVRInput.Axis1D chooseLeft = OVRInput.Axis1D.PrimaryIndexTrigger;
        public bool chosenRight = false;
        public bool chosenLeft = false;
    }
}*/