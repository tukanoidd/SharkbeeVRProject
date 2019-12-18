using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrammarBehavior : MonoBehaviour
{
    private Character character;
    
    private bool inGrammarArea = false;
    public bool grammarStarted = false;
    public bool grammarDone = false;

    [SerializeField] private GrammarMonkey grammarMonkey;

    [SerializeField] private OVRInput.RawAxis1D rightGrammarMonkeyAnswerCheck;
    [SerializeField] private OVRInput.RawAxis1D leftGrammarMonkeyAnswerCheck;

    private GrammarMonkeys monkeys;
    
    void Start()
    {
        character = GetComponent<Character>();
    }
    
    void Update()
    {
        /*if (!grammarDone)
        {
            CheckGrammarDistance();

            if (inGrammarArea && !grammarStarted)
            {
                grammarStarted = true;
            }

            if (monkeys.questionPresented)
            {
                
            }
    }
    
    void CheckGrammarDistance()
    {
        float playerGrammarMonkeyDistance =
            Vector3.Distance(transform.position, grammarMonkey.transform.position);

        inGrammarArea = playerGrammarMonkeyDistance <= grammarMonkey.tutorialAreaDistance;
        inGrammar = playerGrammarMonkeyDistance <= grammarMonkey.nearTutorialMonkeyDistance;
    }
}
