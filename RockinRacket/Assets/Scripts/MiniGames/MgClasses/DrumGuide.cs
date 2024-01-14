using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DrumGuide : MinigameController
{

    public GameObject ChildCanvasPanels; 
    
    public BandRoleName TargetBandMember = BandRoleName.Ace;
    public float StressFactor = 1;

    [SerializeField] public List<Button> Drums;
    [SerializeField] private List<int> drumSequence = new List<int>();
    private List<Color> originalColors;
    [SerializeField]private int sequenceLength = 5;
    [SerializeField]private int currentDrumIndex = 0;

    /*
    Event and State Logic
    */

    void Start()
    {
        CanActivate = false;
        IsActive = false;
        SubscribeEvents();
        originalColors = new List<Color>();
        for (int i = 0; i < Drums.Count; i++)
        {
            int index = i; 
            Drums[i].onClick.AddListener(() => OnDrumClicked(index));

            originalColors.Add(Drums[i].colors.normalColor);
        }
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }

    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                ResetSpawnTimer();
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                if(IsActive)
                {
                    CancelMinigame();
                }
                if(CanActivate)
                {
                    CanActivate = false;
                    CancelMinigame();
                }
                StopCoroutine(availabilityTimerCoroutine);
                StopCoroutine(spawnTimerCoroutine);
                
                break;
            default:
                break;
        }
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
    }

    public override void FailMinigame()
    {
        IsActive = false;
        MinigameEvents.EventFail(this);
        // Fail minigame logic 
        StopGameplayTimer();
        CloseMinigame();
        ResetSpawnTimer();
    }

    public override void FinishMinigame()
    {
        IsActive = false;
        MinigameEvents.EventComplete(this);
        // Finish minigame logic 
        StopGameplayTimer();
        CloseMinigame();
        ResetSpawnTimer();
    }

    public override void CancelMinigame()
    {
        IsActive = false;
        MinigameEvents.EventCancel(this);
        // Cancel minigame logic 
        StopGameplayTimer();
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
