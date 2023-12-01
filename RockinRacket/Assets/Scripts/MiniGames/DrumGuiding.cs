using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrumGuiding : MiniGame
{
    [SerializeField] public List<Button> Drums;
    private List<int> drumSequence = new List<int>();
    private int currentDrumIndex = 0;
    private int sequenceLength = 5;
    public BandRoleName bandRole = BandRoleName.Ace;
    public float BrokenLevelChange = 1;

    //starting of minigame
    public override void Activate()
    {
        base.Activate();
        ConcertAudioEvent.AudioBroken(this, BrokenLevelChange, bandRole, true);
        RestartMiniGameLogic();
    }

    //completion of minigame
    public override void Complete()
    {
        base.Complete();
        ConcertAudioEvent.AudioFixed(this, BrokenLevelChange, bandRole, true);
    }
    
    //Misses the mini-game due to end of concert state
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
        for (int i = 0; i < Drums.Count; i++)
        {
            int index = i; 
            Drums[i].onClick.AddListener(() => OnDrumClicked(index));
        }
    }


    public override void RestartMiniGameLogic()
    {
        IsCompleted = false;
        currentDrumIndex = 0;
        RandomizeDrumSequence();
        HighlightDrum(drumSequence[currentDrumIndex]);
    }

    private void RandomizeDrumSequence()
    {
        drumSequence.Clear();
        for (int i = 0; i < Drums.Count; i++)
        {
            drumSequence.Add(i);
        }
        for (int i = 0; i < drumSequence.Count; i++)
        {
            int temp = drumSequence[i];
            int randomIndex = Random.Range(i, drumSequence.Count);
            drumSequence[i] = drumSequence[randomIndex];
            drumSequence[randomIndex] = temp;
        }
    }

    private void HighlightDrum(int index)
    {
        // Remove highlight from all drums
        foreach (Button drum in Drums)
        {
            Outline outline = drum.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }

        // Highlight the correct drum
        Outline currentOutline = Drums[index].GetComponent<Outline>();
        if (currentOutline == null)
        {
            currentOutline = Drums[index].gameObject.AddComponent<Outline>();
            currentOutline.effectColor = Color.white;
            currentOutline.effectDistance = new Vector2(5, 5);
        }
        currentOutline.enabled = true;
    }

    public void OnDrumClicked(int drumIndex)
    {
        if (drumIndex == drumSequence[currentDrumIndex])
        {
            currentDrumIndex++;
            if (currentDrumIndex == sequenceLength)
            {
                Complete();
            }
            else
            {
                HighlightDrum(drumSequence[currentDrumIndex]);
            }
        }
        else
        {
            RestartMiniGameLogic();
        }
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

}
