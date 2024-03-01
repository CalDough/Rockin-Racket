using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendeeController : MonoBehaviour
{
    /*
    Attendees manage their own boos and such
    In the future, this class may involve swapping attendees out after getting a shirt or getting a low mood score
    */
    [Header("Attendees Variables")]
    public List<ConcertAttendee> ActiveAttendees;
    //public List<ConcertAttendee> HiddenAttendees;

    public List<int> ConcertTrashPerSong;

    [Header("Trash Variables")]
    public int currentTrashCleanedCount = 0;
    public int currentTrashCount = 0;
    public int maxTrashBeforePunishment = 4;
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
        ConcertEvents.instance.e_SongEnded.AddListener(CalculateTotalTrash);
    }

    void OnDestroy()
    {

    }

    public int GetScorePenalty()
    {
        int sum = 0;
        foreach (int trashPerSong in ConcertTrashPerSong)
        {
            sum += trashPerSong;
        }

        int penalty = sum - 5;

        if (penalty < 0)
        {
            return 0;
        }
        else
        {
            return (int)(penalty * trashNegativeRatingBonus);
        }
    }

    private void CalculateTotalTrash()
    {
        var trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        currentTrashCount = trashObjects.Length;
        ConcertTrashPerSong.Add(currentTrashCount);
    }

}
