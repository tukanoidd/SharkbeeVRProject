using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_IslandCleanupBehaviour : MonoBehaviour
{
    private bool inMinigameArea = false;
    public bool minigameStarted = false;
    public bool minigameDone = false;

    [SerializeField] private IslandCleanupMonkey islandCleanupMonkey;
    private TutorialMonkey tutorialMonkey;

    private Character character;

    void Start()
    {
        character = GetComponent<Character>();

        tutorialMonkey = islandCleanupMonkey.GetComponent<TutorialMonkey>();
    }

    void Update()
    {
        if (!minigameDone && tutorialMonkey.teleported)
        {
            CheckMinigameDistance();

            if (inMinigameArea && !minigameStarted)
            {
                minigameStarted = true;
            }

            if (inMinigameArea && minigameStarted)
            {
                MinigameCheck();
            }
        }
    }

    void CheckMinigameDistance()
    {
        float playerMinigameMonkeyDistance =
            Vector3.Distance(transform.position, islandCleanupMonkey.transform.position);

        inMinigameArea = playerMinigameMonkeyDistance <= islandCleanupMonkey.minigameAreaRadius;
    }

    void MinigameCheck()
    {
        if (OVRInput.GetDown(character.tutorialNextTextButton))
        {
            if (islandCleanupMonkey.currentTextIndex == islandCleanupMonkey.texts.Length - 1) EndMinigame();
            else islandCleanupMonkey.currentTextIndex = Mathf.Clamp(islandCleanupMonkey.currentTextIndex + 1, 0, islandCleanupMonkey.texts.Length - 2);
        }

        if (OVRInput.GetDown(character.tutorialBackTextButton))
        {
            islandCleanupMonkey.currentTextIndex =
                Mathf.Clamp(islandCleanupMonkey.currentTextIndex - 1, 0, islandCleanupMonkey.texts.Length - 1);
        }
    }
    
    void EndMinigame()
    {
        minigameDone = true;
    }
}