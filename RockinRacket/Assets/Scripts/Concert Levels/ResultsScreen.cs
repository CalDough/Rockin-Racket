using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
/*
 * This class controls the values shown in the concerts results screen
 * 
 * NOTE: RESULT CALCULATIONS ARE YET TO BE FINALIZED
 */
public class ResultsScreenHandler : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] GameObject resultPanels;

    [SerializeField] TMP_Text ScoreValue;
    [SerializeField] TMP_Text MoneyValue;
    [SerializeField] Image GradeImage;

    [SerializeField] Sprite LetterAImage,LetterBImage,LetterCImage,LetterDImage,LetterFImage;

    private void Start()
    {
        resultPanels.SetActive(false);


        ConcertEvents.instance.e_ConcertEnded.AddListener(UpdateResultText);
    }

    private void OnDestroy()
    {
    }

    private void UpdateResultText()
    {
        //Debug.LogError("<color=red> ERROR building result text </color>");
        Debug.Log("Setting Results Screen");

        resultPanels.SetActive(true);
        UpdateGradeImage(ConcertController.instance.cData.currentConcertLetter);
        int score = ConcertController.instance.cData.currentConcertScore;
        ScoreValue.text = "" + score;
        int money = ConcertController.instance.cData.localMoney;
        MoneyValue.text = "$"+ money;
        Debug.Log("Results Screen score " + score);
        Debug.Log("Results Screen money " + money);
        
    }
        
    private void UpdateGradeImage(string currentConcertLetter)
    {
        switch (currentConcertLetter.ToUpper())
        {
            case "A":
                GradeImage.sprite = LetterAImage;
                break;
            case "B":
                GradeImage.sprite = LetterBImage;
                break;
            case "C":
                GradeImage.sprite = LetterCImage;
                break;
            case "D":
                GradeImage.sprite = LetterDImage;
                break;
            case "F":
                GradeImage.sprite = LetterFImage;
                break;
            default:
                GradeImage.sprite = LetterFImage;
                Debug.LogError("Invalid concert letter grade: " + currentConcertLetter);
                break;
        }
    }

}
