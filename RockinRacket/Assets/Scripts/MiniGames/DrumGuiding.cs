using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrumGuiding : MiniGame
{
    [SerializeField] public List<Button> Drums;
    [SerializeField] private List<int> drumSequence = new List<int>();
    [SerializeField] private TextMeshProUGUI indicatorText;
    private int currentDrumIndex = 0;
    private int sequenceLength = 5;
    public BandRoleName bandRole = BandRoleName.Ace;
    public float BrokenLevelChange = 1;


private void InitializeText()
    {
        indicatorText.text = string.Join(" ", new string('~', sequenceLength).ToCharArray());
    }

    //starting of minigame
    public override void Activate()
    {
        base.Activate();
        ConcertAudioEvent.AudioBroken(this, BrokenLevelChange, bandRole, true);
        RestartMiniGameLogic();
        InitializeText();
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
        InitializeText(); 
        UpdateIndicator(currentDrumIndex, 'X');
        currentDrumIndex = 0;
        IsCompleted = false;
        RandomizeDrumSequence();
        HighlightDrum(drumSequence[currentDrumIndex]);
    }

    private void RandomizeDrumSequence()
    {
        drumSequence.Clear();
        
        for (int i = 0; i < sequenceLength; i++)
        {
            int randomIndex = Random.Range(0, Drums.Count);
            drumSequence.Add(randomIndex);
        }
    }

    private void HighlightDrum(int index)
    {
        // remove highlights
        foreach (Button drum in Drums)
        {
            Outline outline = drum.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }

        // highlight the correct drum
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
            UpdateIndicator(currentDrumIndex, 'O');
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

    private void UpdateIndicator(int index, char symbol)
    {
        char[] indicatorChars = indicatorText.text.ToCharArray();
        indicatorChars[index * 2] = symbol; 
        indicatorText.text = new string(indicatorChars);
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
