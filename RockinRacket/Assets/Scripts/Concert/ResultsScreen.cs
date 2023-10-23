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
public class ResultsScreen : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] TMP_Text concertResultsText;
    [SerializeField] AudienceController audienceController;
    [SerializeField] MinigameStatusManager minigameStatusManager;

    [Header("Score Variables")]
    [SerializeField] float moneyMultiplier = 100f;

    void Start()
    {
        minigameStatusManager = MinigameStatusManager.Instance;
        UpdateResultText();
    }

    private void UpdateResultText()
    {
        StringBuilder resultsBuilder = new StringBuilder();

        int crowdSize = audienceController.GetAudienceCount(); 
        int miniGamesCompleted = minigameStatusManager.completedMiniGamesCount; 
        int miniGamesFailed = minigameStatusManager.failedMiniGamesCount; 
        float moneyEarned = crowdSize * moneyMultiplier;
        
        resultsBuilder.AppendLine($"Mini Games Completed: {miniGamesCompleted}");
        resultsBuilder.AppendLine($"Mini Games Failed: {miniGamesFailed}");
        resultsBuilder.AppendLine($"Crowd Size: {crowdSize}");
        resultsBuilder.AppendLine($"Money Earned: ${moneyEarned}");

        for (int i = 0; i < minigameStatusManager.PotentialHypeFromAllSongs.Count; i++)
        {
            float segmentPotentialHype = minigameStatusManager.PotentialHypeFromAllSongs[i];
            float segmentEarnedHype = minigameStatusManager.HypeEarnedFromAllSongs[i];
            resultsBuilder.AppendLine($"Song {i + 1}: {segmentEarnedHype}/{segmentPotentialHype} Hype");
        }

        concertResultsText.text = resultsBuilder.ToString();
    }
}
