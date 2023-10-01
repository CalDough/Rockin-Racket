using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * This class controls the values shown in the concerts results screen
 * 
 * NOTE: RESULT CALCULATIONS ARE YET TO BE FINALIZED
 */


public class ResultsScreen : MonoBehaviour
{
    // Serialized Fields
    [Header("Object References")]
    [SerializeField] TMP_Text concertResultsText;
    [SerializeField] Audience audienceMoodReference;
    [Header("Score Variables")]
    [SerializeField] float scoreMultiplier;
    [SerializeField] float moneyMultiplier;
    [SerializeField] float moodMultiplier;
    [SerializeField] float baseScore;
    [SerializeField] float baseMoney;


    // Private fields
    private float score;
    private float money;
    private float mood;
    private int eventsFailed;
    private int eventsCompleted;
    private int eventsMissed;


    // Start is called before the first frame update
    void Start()
    {
        // Grabbing our event data variables from the audience script
        mood = audienceMoodReference.GetCurrentMood();
        eventsFailed = audienceMoodReference.GetEventsFailed();
        eventsCompleted = audienceMoodReference.GetEventsCompleted();
        eventsMissed = audienceMoodReference.GetEventsMissed();

        // Updating our score values
        UpdateScores();
        // Updating the text on screen
        UpdateResultText();
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Working");
    }

    private void UpdateScores()
    {
        score = baseScore + (eventsCompleted * scoreMultiplier) - (eventsFailed * (scoreMultiplier)) - (eventsMissed * scoreMultiplier);
        money = baseMoney + (mood * moneyMultiplier);
    }

    private void UpdateResultText()
    {
        concertResultsText.text =
            "Overall score: " + score + "\n" +
            "Audience Score: " + mood + "\n" +
            "Money Gained: " + money + "\n" +
            "Minigames Completed: " + eventsCompleted + "\n" +
            "Minigames Failed: " + eventsFailed + "\n" +
            "Minigames Missed: " + eventsMissed;
    }
}
