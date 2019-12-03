﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDebug : MonoBehaviour
{
    public static QuestDebug Instance;

    private bool inMenu = false;

    private Text logText;

    private float startLogHeight;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var logTextRT = DebugUIBuilder.instance.AddLabel("Debug");
        logText = logTextRT.GetComponent<Text>();

        startLogHeight = logTextRT.rect.height;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (inMenu)
            {
                DebugUIBuilder.instance.Hide();
            }
            else
            {
                DebugUIBuilder.instance.Show();
            }

            inMenu = !inMenu;
        }
    }

    public void Log(string msg, bool saveOld = false)
    {
        var rect = logText.GetComponent<RectTransform>().rect;
        if (saveOld) rect.Set(rect.x, rect.y, rect.width, rect.height + startLogHeight);
        else rect.Set(rect.x, rect.y, rect.width, startLogHeight); 
        
        logText.text = saveOld ? logText.text + "\n" + msg : msg;
    }
}