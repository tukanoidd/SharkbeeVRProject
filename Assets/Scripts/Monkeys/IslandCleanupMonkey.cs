using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class IslandCleanupMonkey : MonoBehaviour
{
    [HideInInspector] public Dictionary<string, bool> items = new Dictionary<string, bool>();
    public TextMeshPro itemsListText;
    private TextMeshPro text;
    private TextMeshPro textNext;

    public float minigameAreaRadius;

    public string[] texts;
    public int currentTextIndex = 0;

    [SerializeField] private Character_IslandCleanupBehaviour player;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in FindObjectsOfType<IslandCleanupItem>())
        {
            items.Add(item.name, false);
        }

        textNext.text = "A: Next";
    }

    // Update is called once per frame
    void Update()
    {
        if (player.minigameStarted)
        {
            if (!player.minigameDone)
            {
                if (!itemsListText.gameObject.activeSelf)
                {
                    text.text = texts[currentTextIndex];
                    text.gameObject.SetActive(true);
                }
                
                CheckItemsList();
                CheckTextIndex();
                CheckItems();
            }
            else
            {
                if (itemsListText.gameObject.activeSelf) text.gameObject.SetActive(false);   
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
        if (currentTextIndex == texts.Length - 2) textNext.gameObject.SetActive(false);
        else if (currentTextIndex == texts.Length - 1)
        {
            textNext.gameObject.SetActive(true);
            textNext.text = "A: Finish Minigame";
        }
        else textNext.gameObject.SetActive(true);
    }

    void CheckItems()
    {
        if (items.Values.Where(val => val).ToArray().Length == items.Count)
        {
            currentTextIndex = texts.Length - 1;
        }
    }
}
