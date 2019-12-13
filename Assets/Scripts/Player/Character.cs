using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool inTutorial;
    private bool tutorialDone;
    
    [SerializeField] private GameObject tutorialCollider;
    private TutorialMonkey tutorialMonkey;

    private OVRInput.Button tutorialNextTextButton;
    // Start is called before the first frame update
    void Start()
    {
        if (tutorialCollider != null)
        {
            tutorialMonkey = tutorialCollider.GetComponentInParent<TutorialMonkey>();   
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorialDone && inTutorial) TutorialCheck();
    }

    void TutorialCheck()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject == tutorialCollider)
        {
            inTutorial = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == tutorialCollider)
        {
            inTutorial = false;
        }
    }
}
