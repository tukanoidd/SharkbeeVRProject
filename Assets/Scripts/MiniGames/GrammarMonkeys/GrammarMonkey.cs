using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In GrammarMonkey the dialogues are made
public class GrammarMonkey : MonoBehaviour
{
    public float grammarAreaDistance = 6; // Why is this here and not in behavior
    public float nearGrammarMonkeyDistance; //not used anywhere, yet?
    
    [TextArea]
    public string[] argumentSentences;
    public Answer[] answers;
    [TextArea]
    public string[] endSentences;
    
    
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
    public string answer;
    public bool right;
    [TextArea]
    public string explanation;
}
