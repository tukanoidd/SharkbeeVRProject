using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCleanupItem : MonoBehaviour
{
    [SerializeField] private IslandCleanupMonkey islandCleanupMonkey;
    [SerializeField] private GameObject objectDestoyer;

    void Start()
    {
        islandCleanupMonkey = FindObjectOfType<IslandCleanupMonkey>();
        objectDestoyer = GameObject.Find("ObjectDestroyer");
    }
    
    void CheckIfObjectDestroyer(GameObject other)
    {
        if (other == objectDestoyer)
        {
            gameObject.SetActive(false);
            islandCleanupMonkey.items[name] = true;
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
