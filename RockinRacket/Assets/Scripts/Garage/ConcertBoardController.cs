using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.ProBuilder.MeshOperations;

/*
 * This class controls the level selection process in the hub scene
 * 
 * 
 */

public class ConcertBoardController : MonoBehaviour
{
    [Header("Text Objects")]
    public TMP_Text stickyNote;
    public TMP_Text boardTitle;
    public TMP_Text boardDescription;

    [Header("Buttons")]
    public Button startLevelButton;
    public Button[] levelSelectButtons; // 0 = tutorial, 1 = level one, 2 = level two, 3 = level three, 4 = level four

    [Header("Concert Transition Information")]
    public CorkBoardConcertInfoText concertBoardTextData; // Scriptable object... see file for more info
    public SceneLoader sceneLoader;

    [Header("Concert Gating Info")]
    public char requiredGrade;

    [Header("Debug Mode")]
    public bool accessAnyLevel;
    public bool updateConcertCircles;

    [Header("Demo Mode RESTRICTED ACCESS")]
    public bool isDemo;

    [Header("Concert Direction Circles")]
    public GameObject[] concertCircles;

    private void Start()
    {
        SetOnClickListenersForLevelSelectButtons();

        GameManager.Instance.e_updateBoardTextOnGameLoad.AddListener(UpdateConcertCircle);
    }

    private void Update()
    {
        if (updateConcertCircles)
        {
            UpdateConcertCircle();
        }
    }

    /*
     *  The following method updates the circle on the concert board
     */
    public void UpdateConcertCircle()
    {
        if (GameManager.Instance.CompletedTutorial)
        {
            concertCircles[0].SetActive(true);
        }

        if (GameManager.Instance.CompletedLevelOne)
        {
            concertCircles[1].SetActive(true);
        }
        
        if (GameManager.Instance.CompletedLevelTwo)
        {
            concertCircles[2].SetActive(true);
        }
        
        if (GameManager.Instance.CompletedLevelThree)
        {
            concertCircles[3].SetActive(true);
        }
        
        if (GameManager.Instance.CompletedLevelFour)
        {
            concertCircles[4].SetActive(true);
        }
    }

    /*
     * The following method adds listeners to the level pins when the scene loads in
     */
    public void SetOnClickListenersForLevelSelectButtons()
    {
        try
        {
            levelSelectButtons[0].onClick.AddListener(() => OnLevelSelectButtonPressed(Levels.TUTORIAL));
            levelSelectButtons[1].onClick.AddListener(() => OnLevelSelectButtonPressed(Levels.LEVEL_ONE));
            levelSelectButtons[2].onClick.AddListener(() => OnLevelSelectButtonPressed(Levels.LEVEL_TWO));
            levelSelectButtons[3].onClick.AddListener(() => OnLevelSelectButtonPressed(Levels.LEVEL_THREE));
            levelSelectButtons[4].onClick.AddListener(() => OnLevelSelectButtonPressed(Levels.LEVEL_FOUR));
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
            Debug.LogError("Level select buttons not added to inspector");
        }

    }

    /*
     * The following method updates the text on the cork board based on the level select button that the user pressed
     */
    public void OnLevelSelectButtonPressed(Levels levelPressed)
    {
        startLevelButton.onClick.RemoveAllListeners();

        string stickyNoteText = "";
        string notePadTitle = "";
        string notePadText = "";
        TransitionData transition = null;
        bool canAccessSelectedLevel = false;

        // This switch case updates the text info on screen based on the level pressed
        // It also checks level gating
        switch (levelPressed)
        {
            case Levels.TUTORIAL:
                stickyNoteText = concertBoardTextData.tutorialStartLevelText;
                notePadTitle = concertBoardTextData.tutorialNotePadTitle;
                notePadText = CalculateNotePadText(Levels.TUTORIAL);
                transition = concertBoardTextData.tutorialTransitionData;
                GameManager.Instance.currentConcertData = concertBoardTextData.tutorialData;
                canAccessSelectedLevel = true;
                break;
            case Levels.LEVEL_ONE:
                stickyNoteText = concertBoardTextData.levelOneStartLevelText;
                notePadTitle = concertBoardTextData.levelOneNotePadTitle;
                notePadText = CalculateNotePadText(Levels.LEVEL_ONE);
                transition = concertBoardTextData.levelOneTransitionData;
                GameManager.Instance.currentConcertData = concertBoardTextData.levelOneData;

                if (GameManager.Instance.CompletedTutorial)
                {
                    canAccessSelectedLevel = true;
                }

                if (isDemo)
                {
                    canAccessSelectedLevel = true;
                }

                canAccessSelectedLevel = true;

                break;
            case Levels.LEVEL_TWO:
                stickyNoteText = concertBoardTextData.levelTwoStartLevelText;
                notePadTitle = concertBoardTextData.levelTwoNotePadTitle;
                notePadText = CalculateNotePadText(Levels.LEVEL_TWO);
                transition = concertBoardTextData.levelTwoTransitionData;
                GameManager.Instance.currentConcertData = concertBoardTextData.levelTwoData;

                if (GameManager.Instance.CompletedLevelOne && GameManager.Instance.concertResultsList[1].gradeLetter == requiredGrade)
                {
                    canAccessSelectedLevel = true;
                }

                if (isDemo)
                {
                    canAccessSelectedLevel = false;
                }

                break;
            case Levels.LEVEL_THREE:
                stickyNoteText = concertBoardTextData.levelThreeStartLevelText;
                notePadTitle = concertBoardTextData.levelThreeNotePadTitle;
                notePadText = CalculateNotePadText(Levels.LEVEL_THREE);
                transition = concertBoardTextData.levelThreeTransitionData;
                GameManager.Instance.currentConcertData = concertBoardTextData.levelThreeData;

                if (GameManager.Instance.CompletedLevelTwo && GameManager.Instance.concertResultsList[2].gradeLetter == requiredGrade)
                {
                    canAccessSelectedLevel = true;
                }

                if (isDemo)
                {
                    canAccessSelectedLevel = false;
                }

                break;
            case Levels.LEVEL_FOUR:
                stickyNoteText = concertBoardTextData.levelFourStartLevelText;
                notePadTitle = concertBoardTextData.levelFourNotePadTitle;
                notePadText = CalculateNotePadText(Levels.LEVEL_FOUR);
                transition = concertBoardTextData.levelFourTransitionData;
                GameManager.Instance.currentConcertData = concertBoardTextData.levelFourData;

                if (GameManager.Instance.CompletedLevelThree && GameManager.Instance.concertResultsList[3].gradeLetter == requiredGrade)
                {
                    canAccessSelectedLevel = true;
                }

                if (isDemo)
                {
                    canAccessSelectedLevel = false;
                }

                break;
            default:
                stickyNoteText = "Invalid Level";
                notePadTitle = "Invalid Level";
                notePadText = "Invalid Level";
                break;
        }

        // Updating the concert circles
        UpdateConcertCircle();

        // Setting the descriptions for our text objects
        stickyNote.text = stickyNoteText;
        boardTitle.text = notePadTitle;
        boardDescription.text = notePadText;

        // Resetting scriptable object data
        ResetConcertScriptableObject(GameManager.Instance.currentConcertData);

        // Adding the scene transition if the data is not null
        if (transition != null && canAccessSelectedLevel)
        {
            startLevelButton.onClick.AddListener(() => sceneLoader.SwitchScene(transition));
        }
        // Checking for debug mode override
        else if (accessAnyLevel && transition != null)
        {
            startLevelButton.onClick.AddListener(() => sceneLoader.SwitchScene(transition));
            Debug.Log("<color=blue> Level access override active </color>");
        }
        // Otherwise the player has not unlocked the next level, so we update the button text accordingly
        else if (!canAccessSelectedLevel)
        {

            if (isDemo && levelPressed != Levels.LEVEL_ONE)
            {
                stickyNote.text = "Accessible in full game!";

            }
            else
            {
                stickyNote.text = "Previous Concert Score Not High Enough";

            }
            Debug.Log("<color=orange>Player has not unlocked the selected level</color>");
        }
        // Null data check
        else if (transition == null)
        {
            Debug.LogError("Transition data missing for selected concert");
        }
    }

    /*
     * 
     * The following method calculates the text displayed in the description of a level based
     * on whether or not the player has already completed it
     * 
     */
    private string CalculateNotePadText(Levels levelPressed) {

        switch (levelPressed)
        {
            case Levels.TUTORIAL:
                
                if (GameManager.Instance.CompletedTutorial)
                {
                    Debug.Log($"Displaying results stats for {levelPressed.ToString()}");
                    return FormatConcertScoreData(GameManager.Instance.concertResultsList[0]);
                }
                else
                {
                    Debug.Log($"Displaying default text for {levelPressed.ToString()}");
                    return concertBoardTextData.tutorialNotePadDescription;
                }

                //break;
            case Levels.LEVEL_ONE:

                if (GameManager.Instance.CompletedLevelOne)
                {
                    Debug.Log($"Displaying results stats for {levelPressed.ToString()}");
                    return FormatConcertScoreData(GameManager.Instance.concertResultsList[1]);
                }
                else
                {
                    Debug.Log($"Displaying default text for {levelPressed.ToString()}");
                    return concertBoardTextData.levelOneNotePadDescription;
                }

                //break;
            case Levels.LEVEL_TWO:

                if (GameManager.Instance.CompletedLevelTwo)
                {
                    Debug.Log($"Displaying results stats for {levelPressed.ToString()}");
                    return FormatConcertScoreData(GameManager.Instance.concertResultsList[2]);
                }
                else
                {
                    Debug.Log($"Displaying default text for {levelPressed.ToString()}");
                    return concertBoardTextData.levelTwoNotePadDescription;
                }

                //break;
            case Levels.LEVEL_THREE:

                if (GameManager.Instance.CompletedLevelThree)
                {
                    Debug.Log($"Displaying results stats for {levelPressed.ToString()}");
                    return FormatConcertScoreData(GameManager.Instance.concertResultsList[3]);
                }
                else
                {
                    Debug.Log($"Displaying default text for {levelPressed.ToString()}");
                    return concertBoardTextData.levelThreeNotePadDescription;
                }

                //break;
            case Levels.LEVEL_FOUR:

                if (GameManager.Instance.CompletedLevelFour)
                {
                    Debug.Log($"Displaying results stats for {levelPressed.ToString()}");
                    return FormatConcertScoreData(GameManager.Instance.concertResultsList[4]);
                }
                else
                {
                    Debug.Log($"Displaying default text for {levelPressed.ToString()}");
                    return concertBoardTextData.levelFourNotePadDescription;
                }

                //break;
            default:
                return "Invalid Level";
                //break;
        }
    }

    /*
     * The following method formats the concert results class for the given level
     * 
     */
    private string FormatConcertScoreData(ConcertResultData results)
    {
        return $"CONCERT GRADE: {results.gradeLetter}\nCONCERT SCORE: {results.gradeScore}\nPROFIT: {results.profitAmount}";
    }

    /*
     *  The following method resets the scriptable object for the concert level that the player selects
     */
    public void ResetConcertScriptableObject(ConcertData concertData)
    {
        concertData.isPostIntermission = false;
        concertData.currentAudienceRating = 0;
        concertData.localMoney = 0;
        concertData.numMerchTableCustomers = 0;
        concertData.currentConcertScore = 250;
        concertData.currentConcertLetter = "C";
    }
}
