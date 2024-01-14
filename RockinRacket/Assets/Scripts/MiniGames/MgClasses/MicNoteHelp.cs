using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MicNoteHelp : MinigameController
{
    public GameObject ChildCanvasPanels; 
    
    public BandRoleName TargetBandMember = BandRoleName.MJ;
    public float StressFactor = 1;

    [SerializeField] private Gradient noteColorGradient = new Gradient();
    public RectTransform NoteParentRect;
    public GameObject vocalNotePrefab; 
    public List<VocalNote> notes; 
    public List<Chord> chords;

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
        SubscribeEvents();
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
                StopCoroutine(spawnTimerCoroutine);
                if(IsActive)
                {
                    CancelMinigame();
                }
                StopCoroutine(availabilityTimerCoroutine);
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
        currentScore = 0;
        notesAtEnd = 0;
        RestartMiniGameLogic();
        ResetGameplayTimer();
        StartCoroutine(SpawnVocalNotesWithDelay());


    }

    public override void FailMinigame()
    {
        IsActive = false;
        MinigameEvents.EventFail(this);
        // Fail minigame logic 
        StopCoroutine(gameplayTimerCoroutine);
        CloseMinigame();
        ResetSpawnTimer();
    }

    public override void FinishMinigame()
    {
        IsActive = false;
        MinigameEvents.EventComplete(this);
        // Finish minigame logic 
        StopCoroutine(gameplayTimerCoroutine);
        CloseMinigame();
        ResetSpawnTimer();
    }

    public override void CancelMinigame()
    {
        IsActive = false;
        MinigameEvents.EventCancel(this);
        // Cancel minigame logic 
        StopCoroutine(gameplayTimerCoroutine);
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

        if (noteColorGradient != null)
        {
            float colorPosition = (float)notes.Count / numberOfVocalNotes;
            RawImage noteImage = vocalNoteObject.GetComponent<RawImage>();
            if (noteImage != null)
            {
                noteImage.color = noteColorGradient.Evaluate(colorPosition);
            }
        }

        notes.Add(vocalNote);
    }

    public void NoteClicked(VocalNote vocalNote)
    {
        //Debug.Log("Note Clicked");
        currentScore++;
        CheckForCompletion();
    }

    public void NoteReachedEnd(VocalNote vocalNote)
    {
        // notesAtEnd++; 
        //Debug.Log("Note At End");
        if (!vocalNote.WasClicked)
        {
            currentScore--;
        }

        vocalNote.DisableNote();

        CheckForCompletion();
    }

    private void CheckForCompletion()
    {
        if (notesAtEnd == numberOfVocalNotes)
        {
            HandleChordsCompleted();
        }
        if (currentScore == numberOfVocalNotes)
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
    }
}