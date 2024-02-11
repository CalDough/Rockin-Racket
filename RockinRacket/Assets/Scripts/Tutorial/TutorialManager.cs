using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }
    public List<Tutorial> tutorials;
    public float tutorialActivationDelay = 2.5f;
    public int currentTutorialIndex = 0;
    public bool isWaitingForNextTutorial = false;
    public float delayTimer = 0f;
    public bool afterIntermission;

    public SceneLoader sceneLoader;    
    public TransitionData intermissionSwap;

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
    }

    public void StartTutorialSequence()
    {
        ConcertEvents.instance.e_ConcertStarted.Invoke();
        ConcertEvents.instance.e_SongStarted.Invoke();

        if (tutorials.Count > 0)
        {
            currentTutorialIndex = 0;
            tutorials[currentTutorialIndex].StartTutorial();
        }
    }

    public void NextTutorial()
    {
        if (currentTutorialIndex < tutorials.Count)
        {
            Debug.Log("Completed  "+ tutorials[currentTutorialIndex].name);
            tutorials[currentTutorialIndex].CompleteTutorial();
            isWaitingForNextTutorial = true;
            delayTimer = 0f;
        }
    }

    private void Update()
    {
        if (isWaitingForNextTutorial)
        {
            delayTimer += Time.fixedUnscaledDeltaTime;
            if (delayTimer >= tutorialActivationDelay)
            {
                isWaitingForNextTutorial = false;
                delayTimer = 0f;
                currentTutorialIndex++;

                if (currentTutorialIndex < tutorials.Count)
                {
                    tutorials[currentTutorialIndex].StartTutorial();
                }
                else
                {
                    Debug.Log("All current tutorials completed.");
                    if(!afterIntermission)
                    {ChangeToIntermission();}
                    else
                    {
                        Debug.Log("All tutorials completed.");
                    }
                }
            }
        }
        else if (currentTutorialIndex < tutorials.Count && tutorials[currentTutorialIndex].isTutorialCompleted)
        {
            NextTutorial();
        }
    }

    public void ChangeToIntermission()
    {
        TutorialMusicHandler.instance.StopAudio();
        sceneLoader.SwitchScene(intermissionSwap);  
    }

}