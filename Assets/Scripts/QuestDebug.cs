using System;
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

    private List<string> logTexts = new List<string>();

    [SerializeField] private OVRInput.Button debugButton = OVRInput.Button.Four;

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
        Debug.Log(OVRInput.GetDown(debugButton) || OVRInput.GetDown(OVRInput.Button.Start));
        if (OVRInput.GetDown(debugButton) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            Debug.Log("ervei");
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

        if (saveOld)
        {
            if (logTexts.Count > 5)
            {
                logTexts.RemoveAt(0);
            }
            else
            {
                rect.Set(rect.x, rect.y, rect.width, rect.height + startLogHeight);
            }
            logTexts.Add(msg);
        }
        else
        {
            logTexts = new List<string>() {msg};
            rect.Set(rect.x, rect.y, rect.width, startLogHeight); 
        }

        var logString = "";
        if (logTexts.Count > 0)
        {
            for (int i = 0; i < logTexts.Count; i++)
            {
                logString += logTexts[i] + (i == logTexts.Count - 1 ? "" : "\n");
            }
        }
        else logString = logTexts[0];

        logText.text = logString;
    }
}