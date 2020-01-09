using System;
using System.Collections;
using System.Collections.Generic;
using Monkeys;
using UnityEngine;
using TMPro;

public class GrammarMonkey : Monkey
{
    public int currentMonkeyAnswerIndex = 0;
    [HideInInspector] public GrammarPlayer grammarPlayer;
    private GrammarMonkeys grammarMonkeys;
    
    public MonkeyAnswer[] monkeyAnswers;

    private void Start()
    {
        grammarPlayer = player.GetComponent<GrammarPlayer>();
        grammarMonkeys = FindObjectOfType<GrammarMonkeys>();
        grammarMonkeys.grammarPlayer = grammarPlayer;
    }

    private void Update()
    {
        if (grammarPlayer.grammarStarted && !grammarPlayer.grammarDone)
        {
            var monkeyAnswer =  monkeyAnswers[currentMonkeyAnswerIndex];
            dialogText.text = monkeyAnswer.texts[monkeyAnswer.currentTextIndex];
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
}
