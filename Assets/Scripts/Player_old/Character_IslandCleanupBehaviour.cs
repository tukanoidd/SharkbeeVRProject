/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Character_IslandCleanupBehaviour : MonoBehaviour
{
    private bool inMinigameArea = false;
    public bool minigameStarted = false;
    public bool minigameDone = false;

    [SerializeField] private IslandCleanupMonkey_old islandCleanupMonkeyOld;
    private TutorialMonkey_old _tutorialMonkeyOld;

    private Character character;

    void Start()
    {
        character = GetComponent<Character>();

        _tutorialMonkeyOld = islandCleanupMonkeyOld.GetComponent<TutorialMonkey_old>();
    }

    void Update()
    {
        if (!minigameDone && _tutorialMonkeyOld.teleported)
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
            Vector3.Distance(transform.position, islandCleanupMonkeyOld.transform.position);

        inMinigameArea = playerMinigameMonkeyDistance <= islandCleanupMonkeyOld.minigameAreaRadius;
    }

    void MinigameCheck()
    {
        if (OVRInput.GetDown(character.tutorialNextTextButton))
        {
            if (islandCleanupMonkeyOld.currentTextIndex == islandCleanupMonkeyOld.texts.Length - 1) EndMinigame();
            else islandCleanupMonkeyOld.currentTextIndex = Mathf.Clamp(islandCleanupMonkeyOld.currentTextIndex + 1, 0, islandCleanupMonkeyOld.texts.Length - 2);
        }

        if (OVRInput.GetDown(character.tutorialBackTextButton))
        {
            islandCleanupMonkeyOld.currentTextIndex =
                Mathf.Clamp(islandCleanupMonkeyOld.currentTextIndex - 1, 0, islandCleanupMonkeyOld.texts.Length - 1);
        }
    }
    
    void EndMinigame()
    {
        minigameDone = true;
    }
}*/