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
        if (hasNewDialogue)
        {
            visualCue.SetActive(true);
        }
    }
    
    private void Awake()
    {
        visualCue.SetActive(false);
    }

    public void OnRayHit()
    {
        if (!DialogueManager.GetInstance().dialogueActive)
        {
            hasNewDialogue = false;
            DialogueManager.GetInstance().StartDialogue(inkJSON);
        }
    }
}
