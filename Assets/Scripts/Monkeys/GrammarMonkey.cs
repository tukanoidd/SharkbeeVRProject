using System;
using System.Collections;
using System.Collections.Generic;
using Monkeys;
using UnityEngine;
using TMPro;

public class GrammarMonkey : Monkey
{
    public float grammarMonkeyMinigameArea = 10f;
    public GameObject answerLeft;
    public GameObject answerRight;
    [HideInInspector] public TextMeshPro leftText;

    [HideInInspector] public TextMeshPro rightText;

    //public bool goToNextQuestion = false;
    private bool showAnswersActive = false;
    public int currentMonkeyAnswerIndex = 0;

    [HideInInspector] public GrammarPlayer grammarPlayer;

    //public OVRInput.RawAxis1D chooseRight = OVRInput.RawAxis1D.RIndexTrigger;
    //public OVRInput.RawAxis1D chooseLeft = OVRInput.RawAxis1D.LIndexTrigger;
    public MonkeyAnswer[] monkeyAnswers;

    [HideInInspector] public OVRPlayerController controller;

    //private bool pickingRight = false;
    //private bool pickingLeft = false;
    private bool feedbackActive = false;

    protected override void Start()
    {
        base.Start();

        grammarPlayer = player.GetComponent<GrammarPlayer>();
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

            var monkeyExplanation = monkeyAnswers[currentMonkeyAnswerIndex];
            if (monkeyExplanation.currentExplanationIndex >= 0 &&
                monkeyExplanation.explanation.Length > monkeyExplanation.currentExplanationIndex)
            {
                dialogText.text = monkeyExplanation.explanation[monkeyExplanation.currentExplanationIndex];
                changeExplanationIndex();
                if (OVRInput.GetDown(player.nextTextButton) &&
                    monkeyExplanation.explanation.Length < monkeyExplanation.currentExplanationIndex + 1)
                {
                    showAnswers();
                }
            }

            if (showAnswersActive)
            {
                checkAnswers();
            }

            if (feedbackActive)
            {
                goToNextPart();
            }
        }

        if (grammarPlayer.grammarDone)
        {
            answerLeft.SetActive(false);
            answerRight.SetActive(false);
        }
    }

    private void changeExplanationIndex()
    {
        if (OVRInput.GetDown(player.nextTextButton))
        {
            var monkeyExplanation = monkeyAnswers[currentMonkeyAnswerIndex];
            monkeyExplanation.currentExplanationIndex++;
        }
    }

    private void showAnswers()
    {
        //if (OVRInput.GetDown(minigamesPlayer.nextTextButton))
        //{

        dialogTextObject.SetActive(false);
        var bothTexts = monkeyAnswers[currentMonkeyAnswerIndex];
        if (bothTexts.currentBothIndex >= 0 && bothTexts.leftAnswer.Length > bothTexts.currentBothIndex)
        {
            leftText.text = bothTexts.leftAnswer[bothTexts.currentBothIndex];
            rightText.text = bothTexts.rightAnswer[bothTexts.currentBothIndex];
        }

        answerLeft.SetActive(true);
        answerRight.SetActive(true);
        showAnswersActive = true;
    }


    private void checkAnswers()
    {
        var checkWhoCorrect = monkeyAnswers[currentMonkeyAnswerIndex];
        //showAnswersActive = true;
        if (showAnswersActive)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) >= 0.1)
            {
                if (checkWhoCorrect.leftGood)
                {
                    dialogText.text = "That's correct";
                    answerLeft.SetActive(false);
                    answerRight.SetActive(false);
                    dialogTextObject.SetActive(true);
                    feedbackActive = true;
                    showAnswersActive = false;
                }

                if (checkWhoCorrect.rightGood)
                {
                    dialogText.text = "That's wrong";
                    answerLeft.SetActive(false);
                    answerRight.SetActive(false);
                    dialogTextObject.SetActive(true);
                    feedbackActive = true;
                    showAnswersActive = false;
                }
            }

            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) >= 0.1)
            {
                if (checkWhoCorrect.rightGood)
                {
                    dialogText.text = "That's correct";
                    answerLeft.SetActive(false);
                    answerRight.SetActive(false);
                    dialogTextObject.SetActive(true);
                    feedbackActive = true;
                    showAnswersActive = false;
                }

                if (checkWhoCorrect.leftGood)
                {
                    dialogText.text = "That's wrong";
                    answerLeft.SetActive(false);
                    answerRight.SetActive(false);
                    dialogTextObject.SetActive(true);
                    feedbackActive = true;
                    showAnswersActive = false;
                }
            }
        }
    }

    private void goToNextPart()
    {
        if (OVRInput.GetDown(player.nextTextButton))
        {
            showAnswersActive = false;
            currentMonkeyAnswerIndex++;
            feedbackActive = false;
        }
    }


    [Serializable]
    public class MonkeyAnswer
    {
        [TextArea] public string[] explanation;
        public int currentExplanationIndex = 0;
        [TextArea] public string[] leftAnswer;
        [TextArea] public string[] rightAnswer;
        public int currentBothIndex = 0;
        public bool rightGood;

        public bool leftGood;
        //public int currentTextIndex = 0;
    }
}