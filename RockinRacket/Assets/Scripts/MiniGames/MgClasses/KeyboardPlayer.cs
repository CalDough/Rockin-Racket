using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text scoreText; 
    public TMP_Text streakText;
    public TMP_Text instructionText;
    public List<Chord> chords;
    public GameObject KeyboardNotePrefab; 

    [Header("Settings Variables")]
    [SerializeField] float delayBetweenNotes = 1f; 
    //[SerializeField] float delayBetweenScorePenalty = 2f;
    [SerializeField] int notesNeededForScoreBonus = 10;
    [SerializeField] int currentNoteStreak = 0;

    [SerializeField] private float spawnTimer = 0f;
    [SerializeField] float DifficultyModifier = 0.8f;
    [SerializeField] float DifficultyMaxModifier = 2.5f;
    [SerializeField] private int lastNoteIndex = -1; 

    void Start()
    {
        ConcertEvents.instance?.e_SongStarted.AddListener(StartNoteSpawning);
        ConcertEvents.instance?.e_SongEnded.AddListener(StopNoteSpawning);
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (IsKeyboardPlayerSpawning)
        {
            spawnTimer -= Time.fixedDeltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnNote();
                
                spawnTimer = delayBetweenNotes / DifficultyModifier;
                DifficultyModifier = Mathf.Min(DifficultyModifier, DifficultyMaxModifier);
            }
        }
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
        GameManager.Instance.isMinigameOpen = true;
        KeyboardAnim.Play("OpenKeyboard");
        OpenButton.gameObject.SetActive(false);
        HideButton.gameObject.SetActive(true);
        IsKeyboardPlayerOpen = true;
        ShowInstructionText("Click the notes when they are over the piano.", 10f);
    }

    public void HideKeyboardPlayer()
    {
        GameManager.Instance.isMinigameOpen = false;
        KeyboardAnim.Play("HideKeyboard");        
        OpenButton.gameObject.SetActive(true);
        HideButton.gameObject.SetActive(false);
        IsKeyboardPlayerOpen = false;        
        instructionText.gameObject.SetActive(false); 
    }

    void SpawnNote()
    {
        int noteRangeMin = 2; 
        int noteRangeMax = 5; 

        if (lastNoteIndex == -1) 
        {

            lastNoteIndex = Random.Range(0, chords.Count);
        }
        else
        {
            int minIndex = Mathf.Max(0, lastNoteIndex - noteRangeMax); 
            int maxIndex = Mathf.Min(chords.Count - 1, lastNoteIndex + noteRangeMax); 
            lastNoteIndex = Random.Range(minIndex, maxIndex + 1); 
        }

        Chord selectedChord = chords[lastNoteIndex];
        GameObject vocalNoteObject = Instantiate(KeyboardNotePrefab, Vector3.zero, Quaternion.identity, NoteParentRect);
        RectTransform rectTransform = vocalNoteObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = selectedChord.GetWorldPosition(selectedChord.StringStart);
        KeyboardNote keyNote = vocalNoteObject.GetComponent<KeyboardNote>();
        keyNote.AssignedChord = selectedChord;
        keyNote.GameInstance = this;
    }

    public void NoteWasMissed()
    {
        ConcertEvents.instance?.e_ScoreChange.Invoke(ScorePenalty);        
        ConcertAudioEvent.AudioBroken(null, StressFactor, TargetBandMember);
        currentNoteStreak = 0;            
        DifficultyModifier -= .02f;
        if(DifficultyModifier <= 0.8f)
        {
            DifficultyModifier = 0.8f;
        }
    }

    public void NoteWasClicked(KeyboardNote KeyNote)
    {
        if (!KeyNote.IsClickable) { return; }

        bool isInValidClickingZone = RectTransformUtility.RectangleContainsScreenPoint(ValidClickingZone.rectTransform, Input.mousePosition, null);

        if (isInValidClickingZone)
        {
            currentNoteStreak++;
            UpdateStreakText(currentNoteStreak);
            DifficultyModifier += .03f;
            KeyNote.ChangeOpacity();
        }
        if (currentNoteStreak >= notesNeededForScoreBonus)
        {
            int score = ScoreBonus; 
            ConcertEvents.instance?.e_ScoreChange.Invoke(score);
            UpdateScoreText(score);
            currentNoteStreak = 0;
        }
    }

    void UpdateScoreText(int score)
    {
        scoreText.text = "SCORE +" + score;
        StartCoroutine(FadeTextToZeroAlpha(2f, scoreText));
    }

    void UpdateStreakText(int streak)
    {
        streakText.text = "STREAK: " + streak;
        StartCoroutine(FadeTextToZeroAlpha(1f, streakText));
    }

    void ShowInstructionText(string message, float duration)
    {
        instructionText.text = message;
        instructionText.gameObject.SetActive(true); 
        Color color = instructionText.color;
        color.a = 1f;
        instructionText.color = color;
        StartCoroutine(FadeTextAfterDelay(duration, instructionText));
    }

    IEnumerator FadeTextToZeroAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeTextAfterDelay(float delay, TMP_Text textElement)
    {
        yield return new WaitForSeconds(delay); 
        float fadeDuration = 2f; 
        float startAlpha = textElement.color.a;
        for (float t = 0; t < 1.0f; t += Time.deltaTime / fadeDuration)
        {
            Color newColor = new Color(textElement.color.r, textElement.color.g, textElement.color.b, Mathf.Lerp(startAlpha, 0, t));
            textElement.color = newColor;
            yield return null;
        }
        textElement.gameObject.SetActive(false); 
    }
}