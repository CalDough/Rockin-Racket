using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using System;

/*
 * This class handles the score system for the concert
 */


public class ScoreManager : MonoBehaviour
{
    [Header("Score Values")]
    [SerializeField] private int currentScore;
    [SerializeField] private int scoreStartingValue;
    [SerializeField] private int maxScoreValue;
    [SerializeField] private string scoreLetter;

    [Header("UI References")]
    public TMP_Text letterText;
    public Slider scoreSlider;


    /*
     * In start we add a listener for our score change event and then initialize our score variables
     */
    private void Start()
    {
        // If after intermission, we load the score from the first half of the concert
        if (ConcertController.instance.afterIntermission)
        {
            Debug.Log("<color=green> Loading Score From Post Intermission </color>");
            //currentScore = GameManager.Instance.currentConcertScore;
            currentScore = GameManager.Instance.currentConcertData.currentConcertScore;
            scoreSlider.value = currentScore;
            scoreLetter = GameManager.Instance.currentConcertData.currentConcertLetter;
        }
        else
        {
            Debug.Log("<color=green> Loading Score For New Concert State </color>");
            currentScore = scoreStartingValue;
            scoreSlider.value = scoreStartingValue;
        }

        AdjustLetterText();
        scoreSlider.maxValue = maxScoreValue;
        ConcertEvents.instance.e_ScoreChange.AddListener(AdjustScore);
        ConcertEvents.instance.e_ConcertEnded.AddListener(SaveScore);
    }

    /*
     *  This method is called when the score is updated, it updates the slider and updates the letter text
     */
    private void AdjustScore(int scoreAmount)
    {
        currentScore += scoreAmount;

        // Apparently this line isn't working...
        //Mathf.Clamp(currentScore,0,maxScoreValue);

        if (currentScore <= 0)
        {
            currentScore = 0;
        }
        else if (currentScore >= maxScoreValue)
        {
            currentScore = maxScoreValue;
        }


        scoreSlider.value = currentScore;

        AdjustLetterText();
    }

    /*
     * This method updates the letter text based on the American grading scale (A-F)
     */
    private void AdjustLetterText()
    {
        if(letterText == null)
        {
            Debug.Log("LetterText is Null");
            return;
        }

        if (currentScore < 100)
        {
            letterText.text = "F";
            scoreLetter = "F";
        }
        else if(currentScore < 200)
        {
            letterText.text = "D";
            scoreLetter = "D";
        }
        else if (currentScore < 300)
        {
            letterText.text = "C";
            scoreLetter = "C";
        }
        else if (currentScore < 400)
        {
            letterText.text = "B";
            scoreLetter = "B";
        }
        else if (currentScore < 500)
        {
            letterText.text = "A";
            scoreLetter = "A";
        }

        SaveScore();
    }

    /*
     *  This method is called when the concert ends to save the score before intermission or after the concert ends
     */
    private void SaveScore()
    {
        //GameManager.Instance.currentConcertScore = currentScore;
        GameManager.Instance.currentConcertData.currentConcertScore = currentScore;
        //GameManager.Instance.currentConcertLetter = letterText.text;
        GameManager.Instance.currentConcertData.currentConcertLetter = scoreLetter;
        //GameManager.Instance.currentConcertData.currentConcertLetter = letterText.text;
    }
}
