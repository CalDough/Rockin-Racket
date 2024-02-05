using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DrumGuide : MinigameController
{
    [Header("Score Variables")]
    public int ScoreBonus = 25;
    public int ScorePenalty = -25;
    public float StressFactor = 1;
    public BandRoleName TargetBandMember = BandRoleName.Ace;

    [Header("Reference Variables")]
    public GameObject ChildCanvasPanels; 
    [SerializeField] public List<Button> Drums;

    [Header("Settings Variables")]
    [SerializeField]private int sequenceLength = 5;
    [SerializeField]private int currentDrumIndex = 0;
    [SerializeField] private List<int> drumSequence = new List<int>();
    private List<Color> originalColors;

    /*
    Event and State Logic
    */

    void Start()
    {
        CanActivate = false;
        IsActive = false;
        originalColors = new List<Color>();
        for (int i = 0; i < Drums.Count; i++)
        {
            int index = i; 
            Drums[i].onClick.AddListener(() => OnDrumClicked(index));

            originalColors.Add(Drums[i].colors.normalColor);
        }
        ConcertEvents.instance.e_SongStarted.AddListener(HandleGameStateStart);
        ConcertEvents.instance.e_SongEnded.AddListener(HandleGameStateEnd);
    }

    public void HandleGameStateStart()
    {
        ResetSpawnTimer();
    }
    
    private void HandleGameStateEnd()
    {
        if(IsActive)
        {
            CancelMinigame();
        }
        if(CanActivate)
        {
            CanActivate = false;
            CancelMinigame();
        }
        StopSpawnTimer();
        StopAvailabilityTimer();
    }


    /*
    Minigame Logic
    */

    public override void StartMinigame()
    {
        IsActive = true;
        MinigameEvents.EventStart(this);
        // Start minigame logic 
        RestartMiniGameLogic();
        ResetGameplayTimer();
        StopSpawnTimer();
        StopAvailabilityTimer();
    }

    public override void FailMinigame()
    {
        IsActive = false;
        MinigameEvents.EventFail(this);
        // Fail minigame logic 
        StopGameplayTimer();
        StopAvailabilityTimer();
        CloseMinigame();
        ResetSpawnTimer();
        ConcertEvents.instance.e_ScoreChange.Invoke(ScorePenalty);
    }

    public override void FinishMinigame()
    {
        IsActive = false;
        MinigameEvents.EventComplete(this);
        // Finish minigame logic 
        StopGameplayTimer();
        StopAvailabilityTimer();
        CloseMinigame();
        ResetSpawnTimer();        
        ConcertEvents.instance.e_ScoreChange.Invoke(ScoreBonus);
    }

    public override void CancelMinigame()
    {
        IsActive = false;
        MinigameEvents.EventCancel(this);
        // Cancel minigame logic 
        StopGameplayTimer();
        StopAvailabilityTimer();
        CloseMinigame();
        ResetSpawnTimer();
    }

    public override void OpenMinigame()
    {
        ChildCanvasPanels.SetActive(true);
    }

    public override void CloseMinigame()
    {
        ChildCanvasPanels.SetActive(false);
    }

    public void RestartMiniGameLogic()
    {
        currentDrumIndex = 0;
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
            currentDrumIndex++;

            Button drum = Drums[drumIndex];
            ColorBlock colorBlock = drum.colors;

            colorBlock.normalColor = originalColors[drumIndex];
            colorBlock.highlightedColor =  originalColors[drumIndex];
            colorBlock.pressedColor = originalColors[drumIndex];
            colorBlock.selectedColor = originalColors[drumIndex];

            if (currentDrumIndex == sequenceLength)
            {
                FinishMinigame();
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

}
