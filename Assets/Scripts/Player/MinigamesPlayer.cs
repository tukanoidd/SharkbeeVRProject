using System;
using Monkeys;
using UnityEngine;

    public class MinigamesPlayer : MonoBehaviour
    {
        [HideInInspector] public bool tutorialDone = false;
        [HideInInspector] public bool islandCleanupDone = false;
        [HideInInspector] public bool clockDone = false;
        [HideInInspector] public bool grammarDone = false;

        public OVRInput.Button nextTextButton = OVRInput.Button.One;
        public OVRInput.Button backTextButton = OVRInput.Button.Two;

        [HideInInspector] public OVRPlayerController controller;

        public Monkey monkey;

        protected virtual void Start()
        {
            controller = GetComponent<OVRPlayerController>();
        }
    }