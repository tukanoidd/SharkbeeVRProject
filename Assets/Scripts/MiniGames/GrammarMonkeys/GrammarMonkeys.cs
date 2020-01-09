using System.Collections;
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
}