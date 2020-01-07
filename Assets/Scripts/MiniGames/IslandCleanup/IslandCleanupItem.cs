using System;
using System.Collections;
using System.Collections.Generic;
using Monkeys;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class IslandCleanupItem : MonoBehaviour
{
    private IslandCleanupMonkey islandCleanupMonkey;
    [SerializeField] private GameObject objectDestoyer;

    void Start()
    {
        islandCleanupMonkey = FindObjectOfType<IslandCleanupMonkey>();
        objectDestoyer = GameObject.Find("ObjectDestroyer");
    }
    
    void CheckIfObjectDestroyer(GameObject other)
    {
        if (islandCleanupMonkey.currentPhase == IslandCleanupMonkey.IslandCleanupPhases.Cleaning)
        {
            if (other == objectDestoyer)
            {
                gameObject.SetActive(false);
                islandCleanupMonkey.items[name] = true;

                var texts = islandCleanupMonkey.islandCleanupPhasesInfo.cleaningPhase.texts;
                islandCleanupMonkey.dialogText.text = texts[Random.Range(0, texts.Length - 1)];
                
                islandCleanupMonkey.CheckItems();
            }   
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        CheckIfObjectDestroyer(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckIfObjectDestroyer(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckIfObjectDestroyer(other.gameObject);
    }

    private void OnCollisionStay(Collision other)
    {
        CheckIfObjectDestroyer(other.gameObject);
    }
}
