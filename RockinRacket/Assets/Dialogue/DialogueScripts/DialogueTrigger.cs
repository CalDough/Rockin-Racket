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

    [SerializeField] public bool hasNewDialogue;
    private bool isShown = true;

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
        }
    }
    
    private void Awake()
    {
        hasNewDialogue = true;
        visualCue.SetActive(true);
    }

    public void Button_StartDialogue()
    {
        if (!DialogueManager.GetInstance().dialogueActive)
        {
            hasNewDialogue = false;
            visualCue.SetActive(false);
            DialogueManager.GetInstance().StartDialogue(inkJSON);
        }
        isShown = false;
        this.gameObject.GetComponent<Image>().enabled = false;
    }
}
