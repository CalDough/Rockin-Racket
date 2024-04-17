using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIntTutorialHandler : MonoBehaviour
{
    public GameObject previousTutorials;
    public LevelSegmentBar levelSegmentBar;

    public MinigameQueue minigameQueue;

    public GameObject cannon;
    public Transform cannonLocation;

    public CrowdTrashcan trashcan;

    public List<ConcertAttendee> attendees;



    public void Start()
    {
        
    }


    public void MoveAndEnableObjects()
    {
        Debug.Log("Post Int Tutorial Changes Applied");
        levelSegmentBar.IncrementSegmentIndex(3);
        previousTutorials.SetActive(false);
        minigameQueue.gameObject.SetActive(true);

        cannon.transform.position = cannonLocation.transform.position;
        trashcan.gameObject.SetActive(true);

        foreach(Attendee attendee in attendees)
        {
            attendee.gameObject.SetActive(true);
        }
    }
}
