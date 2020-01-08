using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrammarMonkey : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI monkey1DialogueText;
    [SerializeField] private TextMeshProUGUI monkey2DialogueText;
    public OVRInput.Button goToGame = OVRInput.Button.One;
    [SerializeField] private bool monkey1SpeakingFirst;
    public Monkey1Answer[] monkey1Answers;
    public Monkey2Answer[] monkey2Answers;
    public GrammarPlayer grammarPlayer;

    private int monkey1Index;
    private int monkey2Index;
    

    public void StartDialogue()
    {
        //if (inGrammarMinigameArea && OVRInput.GetDown(goToGame))
        {
            
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    
   [Serializable] 
   public class Monkey1Answer
        {
            public string [] text;
            public bool isRight;
            public string explanation;
        } 
   
   [Serializable] 
   public class Monkey2Answer
   {
       public string [] text;
       public bool isRight;
       public string explanation;
   } 
   
}
