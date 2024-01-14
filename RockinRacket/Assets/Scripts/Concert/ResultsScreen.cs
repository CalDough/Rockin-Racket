using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
/*
 * This class controls the values shown in the concerts results screen
 * 
 * NOTE: RESULT CALCULATIONS ARE YET TO BE FINALIZED
 */
public class ResultsScreenHandler : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] GameObject resultPanels;
    [SerializeField] TMP_Text concertResultsText;
    [SerializeField] CrowdController crowdController;
    [SerializeField] CrowdTrashcan crowdTrashcan;

    [Header("Score Variables")]
    public int attendees = 0;
    public float moneyMultiplier = 100f;
    
    public int moneyEarned = 0;

    [SerializeField] int minigamesFailed = 0;
    [SerializeField] int minigamesCompleted = 0;
    [SerializeField] int minigamesCanceled = 0;

    public int AceStress = 0;
    public int HaleyStress = 0;
    public int KurtStress = 0;
    public int MJStress = 0;

    private void Start()
    {
        resultPanels.SetActive(false);
        SubscribeEvents();
    }
    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void UpdateResultText()
    {
        StringBuilder resultsBuilder = new StringBuilder();

        resultsBuilder.AppendLine($"Mini Games Completed: {minigamesCompleted}");
        resultsBuilder.AppendLine($"Mini Games Failed: {minigamesFailed}");
        resultsBuilder.AppendLine($"Crowd Size: { crowdController.crowdMembers.Count}");
        resultsBuilder.AppendLine($"Money Earned: ${moneyEarned}");
        resultsBuilder.AppendLine($"Trash Cleaned: {crowdTrashcan.TotalTrashCleaned}");
        for (int i = 0; i < crowdController.PotentialConcertRatings.Count; i++)
        {
            float segmentPotentialrating = crowdController.PotentialConcertRatings[i];
            float segmentEarnedrating = crowdController.EarnedConcertRatings[i];
            string formattedSegmentEarnedRating = segmentEarnedrating.ToString("F2");
            resultsBuilder.AppendLine($"Song {i + 1}: {formattedSegmentEarnedRating}/{segmentPotentialrating} Rating");
        }
        
        concertResultsText.text = resultsBuilder.ToString();
    }


    private void OnMinigameComplete(object sender, GameEventArgs e)
    {
        minigamesCompleted++;
    }

    private void OnMinigameFail(object sender, GameEventArgs e)
    {
        minigamesFailed++;

    }
    private void OnMinigameCancel(object sender, GameEventArgs e)
    {
        minigamesCanceled++;
    }

    private void SubscribeEvents()
    {
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
        MinigameEvents.OnMinigameCancel += OnMinigameCancel;
        MinigameEvents.OnMinigameFail += OnMinigameFail;
        MinigameEvents.OnMinigameComplete += OnMinigameComplete;
        
    }

    private void UnsubscribeEvents()
    {
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
        MinigameEvents.OnMinigameCancel -= OnMinigameCancel;
        MinigameEvents.OnMinigameFail -= OnMinigameFail;
        MinigameEvents.OnMinigameComplete -= OnMinigameComplete;
    }
    
    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.SceneOutro:
                resultPanels.SetActive(true);
                UpdateResultText();
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        
    }
}
