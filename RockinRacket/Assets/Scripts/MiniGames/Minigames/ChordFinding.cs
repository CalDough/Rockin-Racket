using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordFinding : MinigameController
{
    public GameObject ChildCanvasPanels; 
    
    public BandRoleName TargetBandMember = BandRoleName.MJ;
    public float StressFactor = 1;

    public GameObject chordPrefab; 
    public List<ChordNote> notes; 
    public List<Chord> chords;

    private int numberOfChords = 3;
    
    private int chordnotesToPlay = 0;
    private int chordnotesRemaining = 0;
    private int chordIndex = 0; 

    private int chordsRemaining;
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
        RestartMiniGameLogic();
        ResetGameplayTimer();
        
        chordsRemaining = numberOfChords;
        chordIndex = 0; // Start from the first chord
        SpawnChordNotes();
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

    public void SpawnChordNotes()
    {
        if (chordIndex >= chords.Count)
        {
            Debug.LogError("Chord index out of range.");
            return;
        }

        Chord currentChord = chords[chordIndex];
        chordnotesRemaining = chordnotesToPlay;

        for (int i = 0; i < chordnotesToPlay; i++)
        {
            Vector2 spawnPosition = Vector2.Lerp(currentChord.StringStart, currentChord.StringEnd, Random.value);
            GameObject noteObject = Instantiate(chordPrefab, spawnPosition, Quaternion.identity, currentChord.transform);
            ChordNote note = noteObject.GetComponent<ChordNote>();
            notes.Add(note);
            ChordNote.OnChordNoteClicked += HandleChordNoteClicked; // Subscribe to note clicked event
        }
    }

    private void HandleChordNoteClicked(ChordNote chordNote)
    {
        chordnotesRemaining--;
        ChordNote.OnChordNoteClicked -= HandleChordNoteClicked; // Unsubscribe from note clicked event
        notes.Remove(chordNote);

        if (chordnotesRemaining <= 0)
        {
            chordIndex++;
            chordsRemaining--;

            if (chordsRemaining > 0)
            {
                SpawnChordNotes(); // Spawn next set of chord notes
            }
            else
            {
                HandleChordsCompleted(); // All chords completed
            }
        }
    }

    private void HandleChordsCompleted()
    {
        FinishMinigame();
    }

    public void RestartMiniGameLogic()
    {
        foreach (ChordNote chordNote in notes)
        {
            Destroy(chordNote.gameObject);
        }
        notes.Clear();
        SpawnChordNotes();
    }
}
