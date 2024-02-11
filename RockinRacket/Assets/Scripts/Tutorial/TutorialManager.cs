using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }
    public List<Tutorial> tutorials;
    public float tutorialActivationDelay = 2.5f;
    public int currentTutorialIndex = 0;
    public bool isWaitingForNextTutorial = false;
    public float delayTimer = 0f;

    [SerializeField] private Button EndConcertSegment;
    
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
                    Debug.Log("All tutorials completed.");
                }
            }
        }
        else if (currentTutorialIndex < tutorials.Count && tutorials[currentTutorialIndex].isTutorialCompleted)
        {
            NextTutorial();
        }
    }

}