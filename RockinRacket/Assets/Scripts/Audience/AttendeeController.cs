using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendeeController : MonoBehaviour
{
    /*
    Still redeciding to play a boo in the Attendee class or this class
    The boo is sort of a multi-person boo sound which sounds odd considering the size of our concert
    Also still want the sound code to be easy to spot in the editor 
    */
    [Header("Attendees Variables")]
    public List<ConcertAttendee> Attendees;

    public List<float> PotentialConcertRatings;
    public List<float> EarnedConcertRatings;
    public List<float> ConcertTrashPerSong;

    [Header("Trash Variables")]
    public int currentTrashCleanedCount = 0;
    public int currentTrashCount = 0;
    public int maxTrashBeforePunishment = 5;
    public float trashNegativeRatingBonus = 2f;

    public static AttendeeController Instance { get; private set; }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        SubscribeEvents();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        MinigameEvents.OnMinigameFail += HandleEventFail;
        MinigameEvents.OnMinigameComplete += HandleEventComplete;
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        MinigameEvents.OnMinigameFail -= HandleEventFail;
        MinigameEvents.OnMinigameComplete -= HandleEventComplete;
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        //TODO 
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        //TODO 
    }

    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                //TODO 
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                CalculateTotalTrash();
                CalculateTotalRatings();
                break;
            default:
                break;
        }
    }

    private void CalculateTotalRatings()
    {
        float totalPotentialRating = 0f;
        float totalEarnedRating = 0f;

        foreach (var attendee in Attendees)
        {
            totalPotentialRating += attendee.maxMoodRating;
            totalEarnedRating += attendee.currentMoodRating;
        }

        PotentialConcertRatings.Add(totalPotentialRating);
        EarnedConcertRatings.Add(totalEarnedRating);
    }

    private void CalculateTotalTrash()
    {
        var trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        currentTrashCount = trashObjects.Length;
        ConcertTrashPerSong.Add(currentTrashCount);
    }

}
