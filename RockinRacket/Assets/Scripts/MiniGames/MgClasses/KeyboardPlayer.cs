using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardPlayer : MonoBehaviour
{   
    [Header("Score Variables")]    
    public BandRoleName TargetBandMember = BandRoleName.Harvey;
    public int ScoreBonus = 25;
    public int ScorePenalty = -5;
    public float StressFactor = 1;

    
    [Header("Reference Variables")]
    public GameObject ChildCanvasPanels;     
    public RectTransform NoteParentRect;
    public Animator KeyboardAnim;
    public Button OpenButton;
    public Button HideButton;
    public RawImage ValidClickingZone;
    public bool IsKeyboardPlayerOpen = false;
    public bool IsKeyboardPlayerSpawning = false;

    public List<Chord> chords;
    public GameObject KeyboardNotePrefab; 

    [Header("Settings Variables")]
    [SerializeField] float delayBetweenNotes = 1f; 
    [SerializeField] float delayBetweenScorePenalty = 2f;
    [SerializeField] int notesNeededForScoreBonus = 10;
    [SerializeField] int currentNoteStreak = 0;

    [SerializeField] float DifficultyModifier = 1.0f;
    [SerializeField] float DifficultyMaxModifier = 3.0f;

    void Start()
    {
        ConcertEvents.instance.e_SongStarted.AddListener(StartNoteSpawning);
        ConcertEvents.instance.e_SongEnded.AddListener(StopNoteSpawning);
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void StartNoteSpawning()
    {
        IsKeyboardPlayerSpawning = true;
    }

    void StopNoteSpawning()
    {
        IsKeyboardPlayerSpawning = false;
    }

    public void OpenKeyboardPlayer()
    {
        KeyboardAnim.Play("OpenKeyboard");
        OpenButton.gameObject.SetActive(false);
        HideButton.gameObject.SetActive(true);
    }

    public void HideKeyboardPlayer()
    {
        KeyboardAnim.Play("HideKeyboard");        
        OpenButton.gameObject.SetActive(true);
        HideButton.gameObject.SetActive(false);
    }

    void SpawnNote()
    {
        Chord randomChord = chords[Random.Range(0, chords.Count)];
        GameObject vocalNoteObject = Instantiate(KeyboardNotePrefab, Vector3.zero, Quaternion.identity, NoteParentRect);

        RectTransform rectTransform = vocalNoteObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = randomChord.GetWorldPosition(randomChord.StringStart);

        KeyboardNote KeyNote = vocalNoteObject.GetComponent<KeyboardNote>();
        KeyNote.AssignedChord = randomChord;
        KeyNote.GameInstance = this;

    }

    public void NoteWasMissed()
    {
        ConcertEvents.instance.e_ScoreChange.Invoke(ScorePenalty);        
        ConcertAudioEvent.AudioBroken(null, StressFactor, TargetBandMember);
        currentNoteStreak = 0;
    }

    public void NoteWasClicked(KeyboardNote KeyNote)
    {
        if(!KeyNote.IsClickable){ return; }

        bool isInValidClickingZone = RectTransformUtility.RectangleContainsScreenPoint(ValidClickingZone.rectTransform, Input.mousePosition, null);

        Debug.Log($"Note was in {(isInValidClickingZone ? "Valid Zone" : "Invalid Zone")}");
        if(isInValidClickingZone)
        {
            KeyNote.WasClicked = true;
            currentNoteStreak++;
        }
        if(currentNoteStreak >= notesNeededForScoreBonus )
        {
            ConcertEvents.instance.e_ScoreChange.Invoke(ScoreBonus); 
            currentNoteStreak = 0;
        }
    }
}
