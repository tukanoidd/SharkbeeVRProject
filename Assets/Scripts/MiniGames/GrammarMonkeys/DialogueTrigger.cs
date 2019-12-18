using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private Character character;
    private bool inGrammar = false;
    private bool inGrammarArea = false;
    public bool grammarStarted = false;
    public bool grammarDone = false;

    [SerializeField] private GrammarMonkey grammarMonkey;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    
    void Start()
    {
        character = GetComponent<Character>();
    }
    
    void Update()
    {
        /*if (!grammarDone)
        {
            CheckGrammarDistance();

            if (inGrammar && !grammarStarted)
            {
                grammarStarted = true;
            }

            if ((grammarMonkey.currentPhase > 0 && (inGrammar || inGrammarArea)) ||
                (grammarMonkey.currentPhase == 0 && inGrammar))
            {
                GrammarCheck();
            }
        }
        else
        {
            if (!grammarMonkey.teleported)
            {
                NextPhase();
            }
        }*/
    }
    
    /*void CheckGrammarDistance()
    {
        float playerGrammarMonkeyDistance =
            Vector3.Distance(transform.position, grammarMonkey.transform.position);

        inGrammarArea = playerGrammarMonkeyDistance <= grammarMonkey.tutorialAreaDistance;
        inGrammar = playerGrammarMonkeyDistance <= grammarMonkey.nearTutorialMonkeyDistance;
    }*/
    
   
   

}


