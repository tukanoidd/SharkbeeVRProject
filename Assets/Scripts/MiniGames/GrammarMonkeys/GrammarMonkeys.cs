using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// for now in GrammarMonkeys the code for the first argument is made
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
    
    //Is update needed here? 
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
    private float countDown = 12f;
    
    
    public void Initialize( /*m1, m2*/)
    {
        initialized = true;
        if (countDown <= 12)
        {
            //show first monkey first text
        }

        if (countDown <= 8)
        {
            //Show second monkey first text
            //delete previous text
        }

        if (countDown <= 4)
        {
            //show first monkey second text
            // delete previous text
        }
        
    }

   /* public void NextText( GrammarMonkey m1, GrammarMonkey m2)
    {
        
    }
    */

    public void CheckArgumentEnd()
    {
        if (countDown <= 0)
        {
            ended = true;
            //show proceed text
        }
    }

    public void ProceedToGame ()
    {
        //if player presses button in proceed text screen, go to game
    }
}