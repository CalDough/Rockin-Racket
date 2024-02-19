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
    [SerializeField] public List<DrumSetPiece> Drums;

    [Header("Settings Variables")]
    [SerializeField]private int sequenceLength = 5;
    [SerializeField]private int currentDrumIndex = 0;
    [SerializeField] private List<int> drumSequence = new List<int>();

    public GameObject starContainer; 
    public GameObject starPrefab; 
    private List<StarRating> stars = new List<StarRating>();

    /*
    Event and State Logic
    */

    void Start()
    {
        CanActivate = false;
        IsActive = false;

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
        HighlightDrum(drumSequence[currentDrumIndex]);
        ClearAndGenerateStars();
    }

    public override void CloseMinigame()
    {
        ChildCanvasPanels.SetActive(false);
    }

    public void RestartMiniGameLogic()
    {
        currentDrumIndex = 0;

        foreach (var drum in Drums)
        {
            drum.HideDrum();
        }
        RandomizeDrumSequence();
        if (drumSequence.Count > 0)
        {
            HighlightDrum(drumSequence[currentDrumIndex]);
        }

        ClearAndGenerateStars();
    }

    private void RandomizeDrumSequence()
    {
        drumSequence.Clear(); 

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < Drums.Count; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < sequenceLength && availableIndices.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count); 
            drumSequence.Add(availableIndices[randomIndex]);
            availableIndices.RemoveAt(randomIndex); 
        }
    }

    private void HighlightDrum(int index)
    {
        if (index >= 0 && index < Drums.Count)
        {
            Drums[index].HighlightDrum();
        }
        else
        {
            Debug.LogError("Attempted to highlight a drum with an index out of bounds.");
        }
    }

    public void OnDrumClicked(GameObject clickedDrum)
    {
        int drumIndex = Drums.IndexOf(clickedDrum.GetComponent<DrumSetPiece>());

        if (drumIndex == -1)
        {
            Debug.LogError("Clicked drum is not part of the drum list.");
            return;
        }

        if (drumIndex == drumSequence[currentDrumIndex])
        {
            Debug.Log("Correct drum clicked.");
            stars[currentDrumIndex].HighlightStars(); 
            currentDrumIndex++;

            if (currentDrumIndex >= drumSequence.Count)
            {
                Debug.Log("Sequence complete.");
                FinishMinigame();
            }
            else
            {
                Drums[drumSequence[currentDrumIndex]].HighlightDrum();
            }
        }
        else
        {
            Debug.Log("Incorrect drum clicked. Resetting sequence.");
            foreach (var star in stars)
            {
                star.HideStars();
            }
            RestartMiniGameLogic();
        }
    }

    private void ClearAndGenerateStars()
    {
        foreach (Transform child in starContainer.transform)
        {
            Destroy(child.gameObject);
        }
        stars.Clear();

        for (int i = 0; i < sequenceLength; i++)
        {
            GameObject starObj = Instantiate(starPrefab, starContainer.transform);
            StarRating star = starObj.GetComponent<StarRating>();
            if (star != null)
            {
                stars.Add(star);
                star.starImage.color = Color.black; 
            }
        }
    }
}
