﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarMonkeys : MonoBehaviour
{
    private GrammarMonkey leftGrammarMonkey;
    private GrammarMonkey rightGrammarMonkey;
    private string text;
    
    

    public bool questionPresented;
    
    public Argument argument = new Argument();

    void Start()
    {
        
    }
    
    void Update()
    {
        if (argument.initialized)
        {
            if (!argument.ended)
            {
            }
        }
    }
}

public class Argument
{
    public bool initialized = false;
    public bool ended = false;
    
    public void Initialize( /*m1, m2*/)
    {
        
    }

    public void NextText( GrammarMonkey m1, GrammarMonkey m2)
    {
        
    }

    public void CheckArgumentEnd()
    {
        
    }
}