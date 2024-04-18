using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeDependentDialogueTrigger : DialogueTrigger
{
    [Header("Good Conversation")]
    [SerializeField] private TextAsset goodInkJSON;
    [Header("Bad Conversation")]
    [SerializeField] private TextAsset badInkJSON;


    public override void Button_StartDialogue()
    {
        ConcertData currentConcertData = GameManager.Instance.currentConcertData;
        if (currentConcertData.currentConcertScore < 300)
        {
            inkJSON = badInkJSON;
        }
        else
        {
            inkJSON = goodInkJSON;
        }
        
        
        base.Button_StartDialogue();
    }
}
