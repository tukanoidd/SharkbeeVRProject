using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrammarBehavior : MinigamesPlayer
{
    private bool inGrammarArea = false;
    public bool grammarStarted = false;
    public bool grammarDone = false;

    [SerializeField] private GrammarMonkey grammarMonkey;

    [SerializeField] private OVRInput.RawAxis1D rightGrammarMonkeyAnswerCheck;
    [SerializeField] private OVRInput.RawAxis1D leftGrammarMonkeyAnswerCheck;

    private GrammarMonkeys monkeys;

    void Start()
    {
    }

    void Update()
    {
        if (!grammarDone)
        {
            CheckGrammarDistance();

            if (!monkeys.argument.initialized)
            {
                monkeys.argument.Initialize();
            }

            if (inGrammarArea  && !monkeys.argument.ended)
            {
                if (!grammarStarted)
                {
                    grammarStarted = true;
                }

                if (monkeys.questionPresented)
                {

                }
            }
        }

        void CheckGrammarDistance()
        {
            float playerGrammarMonkeyDistance =
                Vector3.Distance(transform.position, grammarMonkey.transform.position);

            inGrammarArea = playerGrammarMonkeyDistance <= grammarMonkey.grammarAreaDistance;
        }
    }
}
