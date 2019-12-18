using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarMonkeys : MonoBehaviour
{
    private GrammarMonkey leftGrammarMonkey;
    private GrammarMonkey rightGrammarMonkey;

    public bool questionPresented;
    
    Argument argument = new Argument();

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}

class Argument
{
    public bool initialized = false;
    public bool ended = false;
    
    public void Initialize( /*m1, m2*/)
    {
    }

    public void NextText( /*m1, m2*/)
    {
    }

    public void CheckArgumentEnd()
    {
        
    }
}