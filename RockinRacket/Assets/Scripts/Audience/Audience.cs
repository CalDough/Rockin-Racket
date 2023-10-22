using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class is intended to house the logic behind the audience's mood during the concert
 */

public class Audience : MonoBehaviour
{
    // Visible Fields
    // Will be reorganized at a later date
    [ReadOnly] int AudienceMood = 0;
    [SerializeField] int AudienceStartingMood;
    [SerializeField] float moodPositiveIncreaseModifier;
    [SerializeField] float moodNegativeIncreaseModifier;
    [SerializeField] int maxMood;
    [SerializeField] TMP_Text moodText;
    [SerializeField] GameObject moodFillBar;

    // Public Variables
    // Need to figure out how we are managing individual audience members
   // public static AudienceTCMReaction[25]

    // Private variables
    private Animator anim;
    private int eventsFailed = 0;
    private int eventsCompleted = 0;
    private int eventsMissed = 0;


    private void Start()
    {
        // Subscribing to Events
        GameEvents.OnEventStart += EventStartMoodAlter;
        GameEvents.OnEventFail += EventFailMoodAlter;
        GameEvents.OnEventCancel += EventCancelMoodAlter;
        GameEvents.OnEventComplete += EventCompleteMoodAlter;
        GameEvents.OnEventMiss += EventMissMoodAlter;

        anim = GetComponent<Animator>();

        AudienceMood = AudienceStartingMood;

       // moodBar.SetMaxValue(maxMood);
       // moodBar.SetValue(AudienceMood);
    }

    private void Update()
    {
        UpdateMoodText();
    }

    private void UpdateMoodText()
    {
        if (AudienceMood > 70)
        {
            moodText.text = "Audience Mood: Happy";
            moodFillBar.GetComponent<Image>().color = Color.green;
        }
        else if (AudienceMood > 30)
        {
                moodText.text = "Audience Mood: Neutral";
                moodFillBar.GetComponent<Image>().color = Color.yellow;
        }
        else if (AudienceMood > 0)
        {
            moodText.text = "Audience Mood: Disgruntled";
            moodFillBar.GetComponent<Image>().color = Color.red;
        }
    }

    public void EventStartMoodAlter(object sender, GameEventArgs e)
    {
        Debug.Log("<color=green>Mood Increase</color> | Event Start");

        AudienceMood = (int)(AudienceMood * moodPositiveIncreaseModifier);

        //moodBar.SetValue(AudienceMood);
    }

    public void EventFailMoodAlter(object sender, GameEventArgs e)
    {
        Debug.Log("<color=red>Mood Decrease</color> | Event Fail");

        AudienceMood = (int)(AudienceMood * moodNegativeIncreaseModifier);

        //moodBar.SetValue(AudienceMood);
        eventsFailed++;
    }

    public void EventCancelMoodAlter(object sender, GameEventArgs e)
    {
        Debug.Log("<color=red>Mood Decrease</color> | Event Cancel");

        AudienceMood = (int)(AudienceMood * moodNegativeIncreaseModifier);

        //moodBar.SetValue(AudienceMood);
    }

    public void EventCompleteMoodAlter(object sender, GameEventArgs e)
    {
        Debug.Log("<color=green>Mood Increase</color> | Event Complete");

        AudienceMood = (int)(AudienceMood * moodPositiveIncreaseModifier);

       // moodBar.SetValue(AudienceMood);
        eventsCompleted++;
    }

    public void EventMissMoodAlter(object sender, GameEventArgs e)
    {
        Debug.Log("<color=red>Mood Decrease</color> | Event Miss");

        AudienceMood = (int)(AudienceMood * moodNegativeIncreaseModifier);

        //moodBar.SetValue(AudienceMood);
        eventsMissed++;
    }

    public int GetEventsFailed()
    {
        return eventsFailed;
    }

    public int GetEventsCompleted()
    {
        return eventsCompleted;
    }

    public int GetEventsMissed()
    {
        return eventsMissed;
    }

    public int GetCurrentMood()
    {
        return AudienceMood;
    }
}
