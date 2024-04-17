using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }
    public List<Tutorial> tutorials;
    public List<Tutorial> postIntermissionTutorials;
    public float tutorialActivationDelay = 2.5f;
    public int currentTutorialIndex = 0;
    public bool isWaitingForNextTutorial = false;
    public float delayTimer = 0f;
    public bool afterIntermission;
    public bool isInIntermission;
    public ConcertData tutorialConcertData;
    public SceneLoader sceneLoader;    
    public TransitionData intermissionSwap;
    public PostIntTutorialHandler postIntTutorialHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameManager.Instance.currentConcertData = tutorialConcertData;
        afterIntermission = tutorialConcertData.isPostIntermission; 
    }

    public void StartTutorialSequence()
    {
        if(afterIntermission)
        {
            postIntTutorialHandler.MoveAndEnableObjects();
        }

        var tutorialList = afterIntermission ? postIntermissionTutorials : tutorials;

        if(ConcertEvents.instance != null)
        {
            ConcertEvents.instance.e_ConcertStarted.Invoke();
            ConcertEvents.instance.e_SongStarted.Invoke();
        }


        if (tutorialList.Count > 0)
        {
            currentTutorialIndex = 0;
            tutorialList[currentTutorialIndex].StartTutorial();
        }
    }

    public void NextTutorial()
    {
        var tutorialList = afterIntermission ? postIntermissionTutorials : tutorials;

        if (currentTutorialIndex < tutorialList.Count)
        {
            Debug.Log("Completed " + tutorialList[currentTutorialIndex].name);
            tutorialList[currentTutorialIndex].CompleteTutorial();
            isWaitingForNextTutorial = true;
            delayTimer = 0f;
        }
    }

    private void Update()
    {
        var tutorialList = afterIntermission ? postIntermissionTutorials : tutorials;

        if (isWaitingForNextTutorial)
        {
            delayTimer += Time.fixedUnscaledDeltaTime;
            if (delayTimer >= tutorialActivationDelay)
            {
                isWaitingForNextTutorial = false;
                delayTimer = 0f;
                currentTutorialIndex++;

                if (currentTutorialIndex < tutorialList.Count)
                {
                    tutorialList[currentTutorialIndex].StartTutorial();
                }
                else
                {
                    if (!afterIntermission)
                    {
                        Debug.Log("All Pre Intermission Tutorials completed.");
                        ChangeToIntermission();
                    }
                    else
                    {
                        Debug.Log("All tutorials completed.");
                    }
                }
            }
        }
        else if (currentTutorialIndex < tutorialList.Count && tutorialList[currentTutorialIndex].isTutorialCompleted)
        {
            NextTutorial();
        }
    }

    public void ChangeToIntermission()
    {
        if(isInIntermission){return;}
        
        if(TutorialMusicHandler.instance)
        {
            TutorialMusicHandler.instance.StopAudio();
        }

        afterIntermission = true;

        sceneLoader.SwitchScene(intermissionSwap);  
    }

}