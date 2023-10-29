using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] public bool hasNewDialogue;

    private void Update()
    {
        
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
    }
}
