/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class IslandCleanupMonkey_old : MonoBehaviour
{
    [HideInInspector] public Dictionary<string, bool> items = new Dictionary<string, bool>();
    public TextMeshPro itemsListText;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private TextMeshPro textNext;

    public float minigameAreaRadius = 10f;

    public string[] texts;
    public int currentTextIndex = 0;

    [SerializeField] private Character_IslandCleanupBehaviour player;

    private TutorialMonkey_old _tutorialMonkeyOld;
    
    // Start is called before the first frame update
    void Start()
    {
        _tutorialMonkeyOld = GetComponent<TutorialMonkey_old>();
        
        foreach (var item in FindObjectsOfType<IslandCleanupItem>())
        {
            items.Add(item.name, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_tutorialMonkeyOld.teleported && player.minigameStarted)
        {
            if (!player.minigameDone)
            {
                textNext.text = "A: Next";
                text.text = texts[currentTextIndex];
                
                if (!text.gameObject.activeSelf) text.gameObject.SetActive(true);
                if (!itemsListText.gameObject.activeSelf) itemsListText.gameObject.SetActive(true);

                CheckItemsList();
                CheckTextIndex();
                CheckItems();
            }
            else
            {
                if (text.gameObject.activeSelf) text.gameObject.SetActive(false);   
            }
        }
    }

    void CheckItemsList()
    {
        if (itemsListText != null)
        {
            if (items != null)
            {
                if (items.Keys.Count > 0)
                {
                    itemsListText.text = String.Join("\n", items.Keys.Select(item =>
                        (items[item] ? "<color=green>" : "<color=white>") + item + "</color>").ToArray());   
                }   
            }
        }
    }

    void CheckTextIndex()
    {
        textNext.gameObject.SetActive(true);
        
        if (currentTextIndex == texts.Length - 2) textNext.gameObject.SetActive(false);
        else if (currentTextIndex == texts.Length - 1)
        {
            textNext.text = "A: Finish Minigame";
        }
    }

    void CheckItems()
    {
        if (items.Values.Where(val => val).ToArray().Length == items.Count)
        {
            currentTextIndex = texts.Length - 1;
        }
    }
}
*/
