using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MicFixing : MiniGame, IPointerDownHandler
{
    [SerializeField] private List<GameObject> screwObjects;
    [SerializeField] private GameObject plateCoveringBatteries;
    [SerializeField] private List<GameObject> batteryObjects;
    [SerializeField] public List<BatterySlot> batterySlots;
    private List<Battery> batteries;
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
        isActiveEvent = false;
        if (durationCoroutine != null) {
            StopCoroutine(durationCoroutine);
        }
        GameEvents.EventMiss(this);
        GameEvents.EventClosed(this);
        HandleClosing();
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

    private void SetBatteryList()
    {
        batteries = new List<Battery>();
        foreach (var batteryObject in batteryObjects)
        {
            var battery = batteryObject.GetComponent<Battery>();
            if (battery != null)
            {
                batteries.Add(battery);
            }
            else
            {
                Debug.LogWarning("Assigned GameObject does not have a Battery component", batteryObject);
            }
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
        ScrewInteraction(eventData);
        if (CheckAllScrewsUnscrewed())
        {
            HandleMicPartRepaired();
            foreach (BatterySlot battery in batterySlots)
            {
                battery.ResetSlot();
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
            }
        }
    }


    public override void RestartMiniGameLogic()
    {
        IsCompleted = false;        
        SetScrewList();
        SetBatteryList();
        foreach (Screw screw in screws)
        {
            screw.ResetScrew();
        }
        foreach (Battery battery in batteries)
        {
            battery.ResetPosition();
        }
        foreach (BatterySlot battery in batterySlots)
        {
            battery.MarkAsOccupied();
        }
        plateCoveringBatteries.SetActive(true);
    }

    private void HandleMicPartRepaired()
    {
        if(!isActiveEvent || IsCompleted)
        {return;}

        plateCoveringBatteries.SetActive(false);

    }

    public bool CheckAllBatteriesPlaced()
    {
        foreach (Battery battery in batteries)
        {
            if (!battery.IsPlaced)
            {
                return false;
            }
        }
        CompleteMiniGame();
        return true;
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
