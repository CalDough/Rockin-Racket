using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialTuning : MiniGame
{
    public GameObject dialPrefab; 
    public List<RectTransform> positionObjects; // List of points where dials will be instantiated
    public List<Dial> dials = new List<Dial>(); // List of instantiated dials

    public bool randomMember = false;
    public BandRoleName bandRole = BandRoleName.Kurt;
    public float BrokenLevelChange = 1;

    public override void Activate()
    {
        base.Activate();
        ConcertAudioEvent.AudioBroken(this, BrokenLevelChange, bandRole, true);
        RestartMiniGameLogic();
    }

    public override void Complete()
    {
        base.Complete();
        ConcertAudioEvent.AudioFixed(this, BrokenLevelChange, bandRole, true);
    }
    
    public override void Miss()
    {
        base.Complete();
        ConcertAudioEvent.AudioFixed(this, BrokenLevelChange, bandRole, true);
    }

    public override void OpenEvent()
    { 
        GameEvents.EventOpened(this); 
        HandleOpening();
    }
    public override void CloseEvent() 
    { 
        GameEvents.EventClosed(this); 
        HandleClosing();
    }

    void Start()
    {
        if(randomMember == true)
        {
            var excludedValues = new List<BandRoleName> { BandRoleName.Default, BandRoleName.Harvey, BandRoleName.Speakers };
            BandRoleName randomValue = BandRoleEnumHelper.GetRandomBandRoleName(excludedValues);
            bandRole = randomValue;
        }

        
    }

    public void SpawnDials()
    {
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
            dial.OnDialMatched -= HandleDialMatched;
            Destroy(dial.gameObject);
        }
        dials.Clear();
        SpawnDials();
        IsCompleted = false;
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

    public override void HandleOpening()
    {
        if(!IsCompleted)
        {
            Panels.SetActive(true);
        }
    }

    public override void HandleClosing()
    {
        Panels.SetActive(false);

        //If you want to reset the game if they did not complete it
        if (IsCompleted == false)
        { //RestartMiniGameLogic(); 
        }
    }

    private void CompleteMiniGame()
    {
        Debug.Log("Dial Tuning game completed!");
        this.IsCompleted = true;
        this.Complete();
    }
}
