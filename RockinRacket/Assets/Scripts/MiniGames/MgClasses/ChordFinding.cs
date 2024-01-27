using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordFinding : MinigameController
{
    public GameObject ChildCanvasPanels; 
    
    public BandRoleName TargetBandMember = BandRoleName.MJ;
    public float StressFactor = 1;

    public RectTransform NoteParentRect;
    public GameObject chordnotePrefab; 
    public List<ChordNote> notes; 
    public List<Chord> chords;

    [SerializeField] private int numberOfChordsToPlayInTotal = 3;
    [SerializeField] private int numberOfChordnotesToSpawn = 3;
    [SerializeField] private int chordnotesRemaining = 0;
    private int chordIndex = 0;

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
        chordIndex = 0; 
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

    public void SpawnChordNotes()
    {
        List<Chord> availableChords = new List<Chord>(chords);
        chordnotesRemaining = numberOfChordnotesToSpawn;

        for (int i = 0; i < numberOfChordnotesToSpawn; i++)
        {
            if (availableChords.Count == 0)
            {
                availableChords = new List<Chord>(chords);
            }

            int chordIndex = Random.Range(0, availableChords.Count);
            Chord currentChord = availableChords[chordIndex];
            availableChords.RemoveAt(chordIndex); 

            Vector2 spawnPosition = Vector2.Lerp(currentChord.StringStart, currentChord.StringEnd, Random.value);
            Vector2 worldPosition = currentChord.GetWorldPosition(spawnPosition);

            GameObject noteObject = Instantiate(chordnotePrefab, Vector3.zero, Quaternion.identity, NoteParentRect);
            RectTransform noteRectTransform = noteObject.GetComponent<RectTransform>();
            noteRectTransform.anchoredPosition = worldPosition;

            ChordNote note = noteObject.GetComponent<ChordNote>();
            notes.Add(note);
        }
        foreach (ChordNote note in notes)
        {
            note.GameInstance = this; 
        }
    }

    public void NoteClicked(ChordNote chordNote)
    {
        chordnotesRemaining--;

        if (chordnotesRemaining <= 0)
        {
            chordIndex++;
            DeleteOldChordnotes();

            if (chordIndex < numberOfChordsToPlayInTotal)
            {
                SpawnChordNotes(); 
            }
            else
            {
                HandleChordsCompleted(); 
            }
        }
    }

    private void HandleChordsCompleted()
    {
        //maybe do something fancy here
        FinishMinigame();
    }

    public void RestartMiniGameLogic()
    {
        DeleteOldChordnotes();
        
        chordIndex = 0;
        SpawnChordNotes();
    }

    public void DeleteOldChordnotes()
    {
        foreach (ChordNote chordNote in notes)
        {
            if (chordNote != null)
            {
                Destroy(chordNote.gameObject);
            }
        }
        notes.Clear();
    }
}
