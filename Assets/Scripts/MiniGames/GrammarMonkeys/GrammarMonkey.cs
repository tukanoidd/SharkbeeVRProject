using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarMonkey : MonoBehaviour
{
    public float grammarAreaDistance = 6;
    public float nearGrammarMonkeyDistance;
    
    [TextArea]
    public string[] argumentSentences;
    public Answer[] answers;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable] public class Answer
{
    [TextArea]    
    public string[] preAnswerTexts;
    [TextArea]
    public string answer;
    public bool right;
    [TextArea]
    public string explanation;
}
