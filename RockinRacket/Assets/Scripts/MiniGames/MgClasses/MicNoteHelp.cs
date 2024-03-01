using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MicNoteHelp : MinigameController
{
    [Header("Score Variables")]
    public int ScoreBonus = 25;
    public int ScorePenalty = -25;
    public float StressFactor = 1;
    public BandRoleName TargetBandMember = BandRoleName.Haley;

    [Header("Reference Variables")]
    public GameObject ChildCanvasPanels; 
    public RectTransform NoteParentRect;
    public GameObject vocalNotePrefab; 
    public List<VocalNote> notes; 
    public List<Chord> chords;
    public RawImage BonusZone;
    public GameObject starContainer; 
    public GameObject starPrefab; 
    private List<StarRating> stars = new List<StarRating>();

    [Header("Settings Variables")]
    [SerializeField] float delayBetweenNotes = 1f; 
    [SerializeField] private int numberOfVocalNotes = 6;
    [SerializeField] private int notesAtEnd = 0; 
    [SerializeField] private int currentScore;

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
        currentScore = 0;
        notesAtEnd = 0;
        RestartMiniGameLogic();
        ResetGameplayTimer();
        StopSpawnTimer();
        StopAvailabilityTimer();
        StartCoroutine(SpawnVocalNotesWithDelay());
        
        GenerateStars();
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

    IEnumerator SpawnVocalNotesWithDelay()
    {
        for (int i = 0; i < numberOfVocalNotes; i++)
        {
            SpawnSingleVocalNote();
            yield return new WaitForSeconds(delayBetweenNotes);
        }
    }

    void SpawnSingleVocalNote()
    {
        Chord randomChord = chords[Random.Range(0, chords.Count)];
        GameObject vocalNoteObject = Instantiate(vocalNotePrefab, Vector3.zero, Quaternion.identity, NoteParentRect);

        RectTransform rectTransform = vocalNoteObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = randomChord.GetWorldPosition(randomChord.StringStart);

        VocalNote vocalNote = vocalNoteObject.GetComponent<VocalNote>();
        vocalNote.AssignedChord = randomChord;
        vocalNote.GameInstance = this;

        notes.Add(vocalNote);
    }

    public void NoteClicked(VocalNote vocalNote)
    {
        if(!vocalNote.IsClickable){ return; }

        bool isInBonusZone = RectTransformUtility.RectangleContainsScreenPoint(BonusZone.rectTransform, Input.mousePosition, null);

        int scoreIncrease = isInBonusZone ? 2 : 1;
        Debug.Log($"Note was in {(isInBonusZone ? "bonus" : "normal")}");
        currentScore += scoreIncrease; 

        UpdateStarHighlights();

        CheckForCompletion();
    }

    public void NoteReachedEnd(VocalNote vocalNote)
    {
        notesAtEnd++;
        if (!vocalNote.WasClicked)
        {
            currentScore -= 2; 
            UpdateStarHighlights();
        }

        vocalNote.DisableNote();
        vocalNote.HasReachedEnd = true;

        CheckForCompletion();
    }

    private void UpdateStarHighlights()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if (i < currentScore)
            {
                stars[i].HighlightStars(); 
            }
            else
            {
                stars[i].HideStars(); 
            }
        }
    }

    private void CheckForCompletion()
    {

        if(notes.Count < numberOfVocalNotes){return;}

        bool allNotesProcessed = notes.TrueForAll(note => note.WasClicked || note.HasReachedEnd);

        if (allNotesProcessed)
        {
            HandleChordsCompleted();
        }
    }

    private void HandleChordsCompleted()
    {
        if(currentScore > 0)
        {
            FinishMinigame();
        }
        else
        {
            FailMinigame();
        }
        RestartMiniGameLogic();
    }

    public void RestartMiniGameLogic()
    {
        foreach (VocalNote vocalNote in notes)
        {
            Destroy(vocalNote.gameObject);
        }
        notes.Clear();
        notesAtEnd = 0;
        currentScore = 0;
    }

    private void GenerateStars()
    {
        Debug.Log("Making Stars");
        for (int i = 0; i < stars.Count; i++)
        {
            Destroy(stars[i].gameObject);
        }
        stars.Clear();

        int starsToGenerate = numberOfVocalNotes; 
        for (int i = 0; i < starsToGenerate; i++)
        {
            GameObject starObj = Instantiate(starPrefab, starContainer.transform);
            StarRating star = starObj.GetComponent<StarRating>();
            if (star != null)
            {
                stars.Add(star);
                star.HideStars();
            }
        }
    }
    

}