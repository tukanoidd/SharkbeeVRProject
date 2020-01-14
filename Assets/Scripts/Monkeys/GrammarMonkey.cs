using System;
using System.Collections;
using System.Collections.Generic;
using Monkeys;
using UnityEngine;
using TMPro;

public class GrammarMonkey : Monkey
{
    public float grammarMonkeyMinigameArea = 1f;
    public GameObject answerLeft;
    public GameObject answerRight;
    [HideInInspector] public TextMeshPro leftText;
    [HideInInspector] public TextMeshPro rightText;
    public bool goToNextQuestion = false;
    private bool showAnswersActive = false;
    public int currentMonkeyAnswerIndex = 0;
    [HideInInspector] public GrammarPlayer grammarPlayer;
    public OVRInput.Axis1D chooseRight = OVRInput.Axis1D.SecondaryIndexTrigger;
    public OVRInput.Axis1D chooseLeft = OVRInput.Axis1D.PrimaryIndexTrigger;
    public MonkeyAnswer[] monkeyAnswers;
    [HideInInspector] public MinigamesPlayer minigamesPlayer;
    [HideInInspector] public OVRPlayerController controller;
    

    

    private void Start()
    {
        grammarPlayer = player.GetComponent<GrammarPlayer>();
        minigamesPlayer = player.GetComponent<MinigamesPlayer>();
        leftText = answerLeft.GetComponent<TextMeshPro>();
        rightText = answerRight.GetComponent<TextMeshPro>();
        controller = GetComponent<OVRPlayerController>();
        //grammarMonkeys.grammarPlayer = grammarPlayer;
    }

    private void Update()
    {
        if (grammarPlayer.grammarStarted && !grammarPlayer.grammarDone)
        {
            //answerLeft.SetActive(false);
            //answerRight.SetActive(false);
            if (!showAnswersActive)
            {
                dialogTextObject.SetActive(true);
            }

            var monkeyExplanation =  monkeyAnswers[currentMonkeyAnswerIndex];
            if (monkeyExplanation.currentExplanationIndex >= 0 &&
                monkeyExplanation.explanation.Length > monkeyExplanation.currentExplanationIndex)
            {
               dialogText.text = monkeyExplanation.explanation[monkeyExplanation.currentExplanationIndex];
               changeExplanationIndex();
               if (OVRInput.GetDown(minigamesPlayer.nextTextButton) && 
                   monkeyExplanation.explanation.Length < monkeyExplanation.currentExplanationIndex + 1)
               {
                   showAnswers();
                   if (showAnswersActive)
                   {
                       checkAnswers();
                   }
                   
                   
               }     
            }
            
               
        }
        
        
    }
    private void changeExplanationIndex()
        {
            if (OVRInput.GetDown(minigamesPlayer.nextTextButton))
            {
                var monkeyExplanation = monkeyAnswers[currentMonkeyAnswerIndex];
                monkeyExplanation.currentExplanationIndex++;
                
            }
        }
    private void showAnswers()
    {
        //if (OVRInput.GetDown(minigamesPlayer.nextTextButton))
        //{
        showAnswersActive = true;
            dialogTextObject.SetActive(false);
            var bothTexts = monkeyAnswers[currentMonkeyAnswerIndex];
            if (bothTexts.currentBothIndex >= 0 && bothTexts.leftAnswer.Length > bothTexts.currentBothIndex)
            {
                
                leftText.text = bothTexts.leftAnswer[bothTexts.currentBothIndex];
                rightText.text = bothTexts.rightAnswer[bothTexts.currentBothIndex];
            }
            answerLeft.SetActive(true);
            answerRight.SetActive(true);
    }
    
    private void checkAnswers()
    {
        Debug.Log("JOEJOE");
        var checkWhoCorrect = monkeyAnswers[currentMonkeyAnswerIndex];
        if (OVRInput.Get(chooseRight) > 0)
        {
            if (checkWhoCorrect.rightGood)
            {
                Debug.Log("IT WORKS!!!!!!!!!");
                dialogText.text = "That's correct";
                answerLeft.SetActive(false);
                answerRight.SetActive(false);
                dialogTextObject.SetActive(true);
            }

            if (checkWhoCorrect.leftGood)
            {
                Debug.Log("IT WORKS!!!!!!!!!");
                dialogText.text = "That's wrong";
                answerLeft.SetActive(false);
                answerRight.SetActive(false);
                dialogTextObject.SetActive(true);
            }
        }

        if (OVRInput.Get(chooseLeft) > 0)
        {
            if (checkWhoCorrect.rightGood)
            {
                Debug.Log("IT WORKS!!!!!!!!!");
                dialogText.text = "That's wrong";
                answerLeft.SetActive(false);
                answerRight.SetActive(false);
                dialogTextObject.SetActive(true);
            }

            if (checkWhoCorrect.leftGood)
            {
                Debug.Log("IT WORKS!!!!!!!!!");
                dialogText.text = "That's correct!";
                answerLeft.SetActive(false);
                answerRight.SetActive(false);
                dialogTextObject.SetActive(true);
            }
        }

       /* if (checkWhoCorrect.leftGood && OVRInput.Get(chooseLeft) > 0)
        {
            Debug.Log("IT WORKS!!!!!!!!!");
            dialogText.text = "That's correct!";
            answerLeft.SetActive(false);
            answerRight.SetActive(false);
            dialogTextObject.SetActive(true);
        }

        if (checkWhoCorrect.leftGood && OVRInput.Get(chooseRight) > 0)
        {
            Debug.Log("IT WORKS!!!!!!!!!");
            dialogText.text = "That's wrong";
            answerLeft.SetActive(false);
            answerRight.SetActive(false);
            dialogTextObject.SetActive(true);
        }*/
    }
    
    
    
    [Serializable] 
    public class MonkeyAnswer
    {
        [TextArea]
        public string [] rightAnswer;
        [TextArea]
        public string[] leftAnswer;
        public int currentBothIndex = 0;
        public bool rightGood;
        public bool leftGood;
        //public int currentTextIndex = 0;
        [TextArea]
        public string [] explanation;
        public int currentExplanationIndex = 0;

    }
}
