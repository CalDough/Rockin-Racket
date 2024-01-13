using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicNoteHelp : MinigameController
{
    public GameObject ChildCanvasPanels; 
    
    public BandRoleName TargetBandMember = BandRoleName.MJ;
    public float StressFactor = 1;

    public GameObject chordPrefab; 
    public List<VocalNote> notes; 
    public List<Chord> chords;

    float delayBetweenNotes = 1f; 
    private int numberOfVocalNotes = 6;
    
    private int vocalNotesRemaining = 0;
    private int totalScore;
    private int currentScore;

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
        totalScore = numberOfVocalNotes;
        currentScore = 0;
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
        GameObject vocalNoteObject = Instantiate(chordPrefab, randomChord.StringStart, Quaternion.identity, transform);
        VocalNote vocalNote = vocalNoteObject.GetComponent<VocalNote>();
        vocalNote.AssignedChord = randomChord;
        vocalNote.GameInstance = this; 
        notes.Add(vocalNote);
    }


    public void NoteClicked(VocalNote vocalNote)
    {

    }

    public void NoteReachedEnd(VocalNote vocalNote)
    {
        // Handle note reaching the end logic
        if (!vocalNote.WasClicked)
        {
            currentScore--; 
        }
        else
        {
            currentScore++; 
        }

        CheckForCompletion();
    }

    private void CheckForCompletion()
    {
        if (notes.Count == 0)
        {
            HandleChordsCompleted();
        }
    }

    private void HandleChordsCompleted()
    {
        // Implement logic for completion
        Debug.Log($"Game completed with score: {currentScore}/{totalScore}");
        FinishMinigame();
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