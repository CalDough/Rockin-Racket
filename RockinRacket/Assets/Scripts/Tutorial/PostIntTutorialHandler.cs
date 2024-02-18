using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIntTutorialHandler : MonoBehaviour
{
    public GameObject previousTutorials;

    public MinigameQueue minigameQueue;

    public GameObject cannon;
    public Transform cannonLocation;

    public CrowdTrashcan trashcan;

    public List<ConcertAttendee> attendees;



    public void Start()
    {
        MoveAndEnableObjects();
    }


    public void MoveAndEnableObjects()
    {
        if(TutorialManager.Instance.afterIntermission)
        {
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
}
