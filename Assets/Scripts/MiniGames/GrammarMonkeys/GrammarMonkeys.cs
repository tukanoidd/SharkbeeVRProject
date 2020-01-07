using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Monkeys;
using UnityEngine;

// for now in GrammarMonkeys the code for the first argument is made
public class GrammarMonkeys : MonoBehaviour
{
    public enum GramMonkey
    {
        Left,
        Right
    }
    
    public GrammarMonkey leftGrammarMonkey;
    public GrammarMonkey rightGrammarMonkey;

    public float grammarMonkeyMinigameArea = 10;

    [HideInInspector] public OVRInput.RawAxis1D leftAnswer = OVRInput.RawAxis1D.LIndexTrigger;
    [HideInInspector] public OVRInput.RawAxis1D rightAnswer = OVRInput.RawAxis1D.RIndexTrigger;

    private GrammarPlayer grammarPlayer;

    void Start()
    {
        grammarPlayer = leftGrammarMonkey.player.GetComponent<GrammarPlayer>();
    }
    
    //Is update needed here? 
    void Update()
    {
        
    }

    void SwitchTextbox(GramMonkey monkey)
    {
        var isLeft = monkey == GramMonkey.Left;

        leftGrammarMonkey.dialogTextObject.SetActive(isLeft);
        rightGrammarMonkey.dialogTextObject.SetActive(!isLeft);
        
    }

    [Serializable]
    public class Dialog
    {
        public Replica[] replicas;
        public int currentReplicaIndex;
    }

    [Serializable]
    public class Replica
    {
        private GramMonkey monkey;
        public string[] texts;
        public int currentTextIndex;
    }

    [Serializable]
    public class MinigameReplica : Replica
    {
        public Answer answer;
    }
    
    [Serializable]
    public class Answer
    {
        public string text;
        public bool isRight;
        
        public string explanation;
    }
}