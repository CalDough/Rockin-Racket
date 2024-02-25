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
    public List<ConcertAttendee> ActiveAttendees;
    public List<ConcertAttendee> HiddenAttendees;

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
        ConcertEvents.instance.e_SongEnded.AddListener(CalculateTotalTrash);
    }

    void OnDestroy()
    {

    }


    private void CalculateTotalTrash()
    {
        var trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        currentTrashCount = trashObjects.Length;
        ConcertTrashPerSong.Add(currentTrashCount);
    }

}
