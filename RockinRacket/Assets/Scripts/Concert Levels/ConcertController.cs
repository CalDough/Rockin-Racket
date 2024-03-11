using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

/*
 * This class controls the state and flow of the concert
 * 
 * 
 * 
 * 
 */

public class ConcertController : MonoBehaviour
{
    public static ConcertController instance;

    [Header("Concert Data References")]
    public ConcertData cData;
    public SongData currentSong;
    public SceneLoader sceneLoader;
    public TransitionData intermissionSwap;
    public TransitionData returnToHub;
    public Button resultsScreenButton;

    [Header("Start Screen Details")]
    [SerializeField] private GameObject startScreen;
    [SerializeField] private Button startConcert;

    [Header("Current Song Details")]
    public int numSongsRemaining;
    public string currentSongName;
    public float currentSongLength;
    public float songTimer;
    public bool afterIntermission;
    public List<SongData> localConcertSongList;

    [Header("Final Level Details")]
    public bool isFinalLevel;
    public TransitionData cinematicEight;


    /*
     *  In our awake method we are declaring a singleton for this class
     */
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    /*
     *  In our start method we are locating object references and initializing our concert song data
     */
    private void Start()
    {
        // Locating references to start screen objects
        startScreen = GameObject.Find("StartScreen");
        startConcert = GameObject.Find("StartButton").GetComponent<Button>();
        //resultsScreenButton = GameObject.Find("ReturnToHubButton").GetComponent<Button>();

        GameManager.Instance.currentConcertData = cData;

        if (startScreen == null || startConcert == null)
        {
            Debug.LogError("<color=red> ERROR Initializing Concert Start Screen. Check ConcertController Script</color>");
        }

        // Adding a listener to the button
        startConcert.onClick.AddListener(() => StartConcert());
        resultsScreenButton.onClick.AddListener(() => ReturnToHub());

        // Then we check if it is after intermission, if so, we can skip the start screen
        //afterIntermission = GameManager.Instance.isPostIntermission;
        afterIntermission = GameManager.Instance.currentConcertData.isPostIntermission;

        // Initializing our Song Data for the Concert
        InitializeConcertSongData(afterIntermission);
        //numSongsRemaining = 2;

        if (afterIntermission)
        {
            Debug.Log("<color=green> Post Intermission State Detected </color>");
            //startScreen.SetActive(false);
            //StartConcert();
        }
        else
        {
            Debug.Log("<color=green> Pre-Intermission - Loading Start Screen </color>");
            if (!startScreen.activeSelf)
            {
                startScreen.SetActive(true);
            }
        }
    }

    /*
     *  This method initializes the songs for the concert
     */
    private void InitializeConcertSongData(bool isPostIntermission)
    {
        // Depending on if we are post or pre intermission, we load a separate song list
        if (isPostIntermission)
        {
            localConcertSongList = cData.concertSongsSecondHalf;
            numSongsRemaining = cData.concertSongsSecondHalf.Count;
        }
        else
        {
            localConcertSongList = cData.concertSongsFirstHalf;
            numSongsRemaining = cData.concertSongsFirstHalf.Count;
        }

        currentSong = localConcertSongList[0];
        currentSongLength = currentSong.Duration;
        currentSongName = currentSong.SongName;

        Debug.Log("<color=green> Concert Song Data Initialized</color>");
    }

    /*
     * This method is called when the player clicks on the start concert button. This method
     * sends out the StartSong Event to start the first song of the concert
     */
    private void StartConcert()
    {
        // Disable Start Screen
        startScreen.SetActive(false);

        // Start the concert by calling these two events
        ConcertEvents.instance.e_ConcertStarted.Invoke();
        ConcertEvents.instance.e_SongStarted.Invoke();

        // And then we want to call our song coroutine
        StartCoroutine(SongTimer());
        numSongsRemaining--;

        Debug.Log("<color=green> Concert Started </color>");
    }

    /*
     *  The following method starts the next song
     */
    private void StartNextSong()
    {
        if (numSongsRemaining >= 1)
        {
            Debug.Log("<color=green> Starting Next Song </color>");
            currentSong = localConcertSongList[1];
            numSongsRemaining--;
            currentSongLength = currentSong.Duration;
            currentSongName = currentSong.SongName;
            songTimer = 0;
            StartCoroutine(SongTimer());

            ConcertEvents.instance.e_SongStarted.Invoke();
        }
        else
        {
            if(AttendeeController.Instance != null)
            {
                ConcertEvents.instance.e_ScoreChange.Invoke(-AttendeeController.Instance.GetScorePenalty());
                Debug.Log("<color=green> Concert Ending - Penalty for trash being applied");
            }

            if (afterIntermission)
            {
                Debug.Log("<color=green> Concert Ending - No Songs Remaining and After Intermission");

                

                ConcertEvents.instance.e_ConcertEnded.Invoke();
                resultsScreenButton.gameObject.SetActive(true);
                //GameManager.Instance.isPostIntermission = false;
                GameManager.Instance.currentConcertData.isPostIntermission = false;
            }
            else
            {
                if (isFinalLevel)
                {
                    Debug.Log("<color=green> Swapping to Cinematic 8 </color>");
                    sceneLoader.SwitchScene(cinematicEight);
                }
                else
                {
                    Debug.Log("<color=green> Swapping to Intermission Scene </color>");
                    sceneLoader.SwitchScene(intermissionSwap);
                }
            }
        }
    }

    /*
     * The following coroutine is a timer for the current song, when it finishes
     * the SongEnded event is invoked and the StartNextSong method is called
     */
    IEnumerator SongTimer()
    {
        while (songTimer < currentSongLength)
        {
            songTimer += Time.deltaTime;

            yield return null;
        }

        ConcertEvents.instance.e_SongEnded.Invoke();
        StartNextSong();
    }

    /*
     *  The following method saves the relevant concert data
     */
    private void ReturnToHub()
    {
        GameManager.Instance.UpdateLevelCompletionStatus(cData.concertLevelName, cData.currentConcertLetter[0], (short) cData.currentConcertScore, (short) cData.localMoney);
    }


}
