using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Character Details")]
    [SerializeField] private string characterName;
    
    public bool hasNewDialogue;
    
    private bool isShown = true;

    private bool thisDialogueActive = false;
    
    
    private void Awake()
    {
        DialogueEvents.DialogueEnd += onDialogueEnd;
    }

    private void Start()
    {
        hasNewDialogue = RoomManager.getInstance().CheckIfInteracted(RoomManager.getInstance().currentHub, characterName);
        visualCue.SetActive(true);

    }
    
    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueActive)
        {
            return;
        }
        else if (isShown)
        {
            return;
        }
        else
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            isShown = true;
            thisDialogueActive = false;
        }
    }
    
    public void Button_StartDialogue()
    {
        if (!DialogueManager.GetInstance().dialogueActive)
        {
            visualCue.SetActive(false);
            DialogueManager.GetInstance().StartDialogue(inkJSON);
            isShown = false;
            this.gameObject.GetComponent<Image>().enabled = false;
            thisDialogueActive = true;
        }
    }

    private void onDialogueEnd(object sender, EventArgs args)
    {
        if (thisDialogueActive)
        {
            RoomManager.getInstance().setInteraction(RoomManager.getInstance().currentHub, characterName);
        }
    }

    void OnDestroy()
    {
        DialogueEvents.DialogueEnd -= onDialogueEnd;
    }
}
