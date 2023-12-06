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
    private List<Color> originalColors;


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
        originalColors = new List<Color>();
        for (int i = 0; i < Drums.Count; i++)
        {
            int index = i; 
            Drums[i].onClick.AddListener(() => OnDrumClicked(index));

            originalColors.Add(Drums[i].colors.normalColor);
        }
    }


    public override void RestartMiniGameLogic()
    {
        currentDrumIndex = 0;
        InitializeText(); 
        UpdateIndicator(currentDrumIndex, 'X');
        IsCompleted = false;
        RandomizeDrumSequence();
        HighlightDrum(drumSequence[currentDrumIndex]);
    }

    private void RandomizeDrumSequence()
    {
        drumSequence.Clear();
        int previousIndex = -1; 

        for (int i = 0; i < sequenceLength; i++)
        {
            int randomIndex = Random.Range(0, Drums.Count);

           
            while (randomIndex == previousIndex)
            {
                randomIndex = Random.Range(0, Drums.Count);
            }

            drumSequence.Add(randomIndex);
            previousIndex = randomIndex; 
        }
    }

    private void HighlightDrum(int index)
    {
        for (int i = 0; i < Drums.Count; i++)
        {
            Button drum = Drums[i];
            ColorBlock colorBlock = drum.colors;

            Outline outline = drum.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }

            if (i == index)
            {
                //not sure why but it literally requires all 4 to be white to look decent in game
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = Color.white;
                colorBlock.pressedColor = Color.white;
                colorBlock.selectedColor = Color.white;
            }
            else
            {
                colorBlock.normalColor = originalColors[i];
                colorBlock.highlightedColor =  originalColors[i];
                colorBlock.pressedColor = originalColors[i];
                colorBlock.selectedColor = originalColors[i];
            }

            drum.colors = colorBlock;
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

            Button drum = Drums[drumIndex];
            ColorBlock colorBlock = drum.colors;

            colorBlock.normalColor = originalColors[drumIndex];
            colorBlock.highlightedColor =  originalColors[drumIndex];
            colorBlock.pressedColor = originalColors[drumIndex];
            colorBlock.selectedColor = originalColors[drumIndex];

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
