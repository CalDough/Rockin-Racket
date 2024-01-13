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
    [SerializeField] CrowdController crowdController;
    [SerializeField] CrowdTrashcan crowdTrashcan;

    [Header("Score Variables")]
    [SerializeField] float moneyMultiplier = 100f;

    void Start()
    {
        UpdateResultText();
    }

    private void UpdateResultText()
    {
        StringBuilder resultsBuilder = new StringBuilder();

        float moneyEarned = crowdController.crowdMembers.Count * moneyMultiplier;
        /*        
        resultsBuilder.AppendLine($"Mini Games Completed: {miniGamesCompleted}");
        resultsBuilder.AppendLine($"Mini Games Failed: {miniGamesFailed}");
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
        */
        concertResultsText.text = resultsBuilder.ToString();
    }
}
