using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumRepair : MiniGame
{
    public GameObject drumPart; 

    public bool randomMember = false;
    public BandRoleName bandRole = BandRoleName.Ace;
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
        }

        
    }

    public void SpawnBrokenParts()
    {
        
    }

    public override void RestartMiniGameLogic()
    {
        IsCompleted = false;
    }

    private void HandleDrumPartRepaired()
    {
        if(!isActiveEvent || IsCompleted)
        {return;}

        // All components are fixed
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
        if (IsCompleted == false)
        { 
            //RestartMiniGameLogic(); 
        }
    }

    private void CompleteMiniGame()
    {
        Debug.Log("Drum Repair game completed!");
        this.IsCompleted = true;
        this.Complete();
    }
}
