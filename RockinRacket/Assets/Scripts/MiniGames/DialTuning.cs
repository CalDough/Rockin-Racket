using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialTuning : MiniGame
{
    public GameObject dialPrefab; 
    public List<RectTransform> positionObjects; // List of points where dials will be instantiated
    public List<Dial> dials = new List<Dial>(); // List of instantiated dials

    public bool randomMember = false;
    public int ConcertPositionTarget = 1;
    public float BrokenLevelChange = 1;

    public override void Activate()
    {
        base.Activate();
        ConcertAudioEvent.AudioBroken(this, BrokenLevelChange, ConcertPositionTarget, true);
    }

    public override void Complete()
    {
        base.Complete();
        ConcertAudioEvent.AudioFixed(this, BrokenLevelChange, ConcertPositionTarget, true);
    }


    void Start()
    {
        if(randomMember == true)
        {
            ConcertPositionTarget = Random.Range(1, 6); 
        }
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;

        foreach (RectTransform positionObject in positionObjects)
        {
            GameObject dialObject = Instantiate(dialPrefab, positionObject.position, Quaternion.identity, positionObject);
            Dial newDial = dialObject.GetComponent<Dial>();
            
            if (newDial != null)
            {
                // Set the local position to 0, 0 so it aligns with the positionObject
                dialObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                newDial.SetMarkerAngle(Random.Range(0f, 360f));
                newDial.currentAngle = Random.Range(0f, 360f);
                newDial.OnDialMatched += HandleDialMatched;
                dials.Add(newDial);
            }
            else
            {
                Debug.LogError("object does not have a Dial component!");
            }
        }
    }

    public override void RestartMiniGameLogic()
    {
        foreach (Dial dial in dials)
        {
            if(!dial.isLocked)
            {
                float randomAngle = Random.Range(dial.startAngle, dial.endAngle);
                dial.SetMarkerAngle(randomAngle);
                dial.currentAngle = randomAngle;
            }
            
            
        }
    }

    private void HandleDialMatched()
    {
        if(!isActiveEvent || IsCompleted)
        {return;}

        // If any dial is not matched, exit the function
        foreach (Dial dial in dials)
        {
            if (!dial.MatchingAngle)
            {
                return; 
            }
        }

        // All dials are matched
        CompleteMiniGame();
    }

    private void CompleteMiniGame()
    {
        Debug.Log("Dial Tuning game completed!");
        this.IsCompleted = true;
        this.Complete();
    }
}
