using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MicFixing : MiniGame, IPointerDownHandler
{
    [SerializeField] private List<GameObject> screwObjects;
    [SerializeField] private Image screwdriverImage;

    [SerializeField] private bool isScrewdriverSelected;
    private List<Screw> screws;

    public bool randomMember = false;
    public BandRoleName bandRole = BandRoleName.Haley;
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
        base.Miss();
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

    public void SetScrewList()
    {
        screws = new List<Screw>();
        foreach (var screwObject in screwObjects)
        {
            var screw = screwObject.GetComponent<Screw>();
            if (screw != null)
            {
                screws.Add(screw);
            }
            else
            {
                Debug.LogWarning("Assigned GameObject does not have a Screw component", screwObject);
            }
        }
    }

     public void SelectScrewdriver()
    {
        isScrewdriverSelected = true;
        UpdateToolVisuals();
    }

    private void UpdateToolVisuals()
    {
        screwdriverImage.color = isScrewdriverSelected ? Color.white : new Color(1f, 1f, 1f, 0.5f);
    }

    private bool CheckAllScrewsUnscrewed()
    {
        foreach (Screw screw in screws)
        {
            if (!screw.IsUnscrewed)
            {
                return false;
            }
        }
        
        return true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isScrewdriverSelected)
        {
            ScrewInteraction(eventData);
            if (CheckAllScrewsUnscrewed())
            {
                HandleMicPartRepaired();
            }
        }
    }

    private void ScrewInteraction(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            Screw screw = eventData.pointerCurrentRaycast.gameObject.GetComponent<Screw>();
            if (screw != null && !screw.IsUnscrewed)
            {
                screw.OnPointerClick(null);
                //if (CheckAllScrewsUnscrewed())
                //{HandleMicPartRepaired();}
            }
        }
    }


    public override void RestartMiniGameLogic()
    {
        IsCompleted = false;        
        isScrewdriverSelected = false;
        SetScrewList();
        foreach (Screw screw in screws)
        {
            screw.ResetScrew();
        }
        SelectScrewdriver();
    }

    private void HandleMicPartRepaired()
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
        Debug.Log("Mic Fixing game completed!");
        this.IsCompleted = true;
        this.Complete();
    }
}
