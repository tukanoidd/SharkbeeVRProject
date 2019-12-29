using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClockMonkey : MonoBehaviour
{
    public int neededPoints = 3;

    public int currentPoints = 0;

    public GameObject text;
    [SerializeField] private GameObject textNext;

    [SerializeField] private Character_ClockBehaviour player;

    public float minigameAreaDistance = 6;

    public int currentPhase = 0;
    public int currentPhaseTextIndex = 0;

    [Serializable]
    public struct Texts
    {
        public string[] startMinigameTexts;
        public string[] minigamePraiseTexts;
        public string[] minigameErrorTexts;
        public string[] endMinigameTexts;
    }

    public Texts texts;

    public ClockRandomizer.TimeStruct currentTime;

    void Start()
    {
        currentTime = ClockRandomizer.RandomizeTime();
    }

    public static class ClockRandomizer
    {
        public struct TimeStruct
        {
            public string timeInText;
            public int hour;
            public int minute;
        }

        private static string[] fractionsOfTime = {"Quarter, Half"};
        private static string[] prepositions = {"past", "to"};

        public static TimeStruct RandomizeTime()
        {
            TimeStruct hourMinute = new TimeStruct();

            string fractionOfTime = fractionsOfTime[Random.Range(0, fractionsOfTime.Length)] + " ";
            string preposition = (
                                     fractionOfTime == "Half"
                                         ? "past"
                                         : prepositions[Random.Range(0, prepositions.Length)]) + " ";
            int textHour = Random.Range(1, 25);
            string timeText = fractionOfTime + preposition + textHour;

            int hour = preposition == "to" ? textHour - 1 : textHour;
            int minute = fractionOfTime == "Half" ? 30 : (preposition == "to" ? 45 : 15);

            hourMinute.timeInText = timeText;
            hourMinute.hour = hour;
            hourMinute.minute = minute;
            
            return hourMinute;
        }
    }
}