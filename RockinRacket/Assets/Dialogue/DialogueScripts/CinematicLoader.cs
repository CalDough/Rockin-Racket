using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicLoader : MonoBehaviour
{
    [SerializeField] private TextAsset cinematic_text;
    [SerializeField] private GameObject toNextSceneButton;
    private DialogueManager dialogueManager;
    private bool hasInitialized = false;

    void Awake()
    {
        DialogueEvents.DialogueEnd += OnDialogueEnd;
    }

    void Update()
    {
        if (hasInitialized)
        {
            return;
        }
        else
        {
            hasInitialized = true;
            dialogueManager = DialogueManager.GetInstance();
            toNextSceneButton.SetActive(false);
            dialogueManager.StartDialogue(cinematic_text);
        }
    }

    void OnDialogueEnd(object sender, EventArgs args)
    {
        toNextSceneButton.SetActive(true);
    }

    void OnDestroy()
    {
        DialogueEvents.DialogueEnd -= OnDialogueEnd;
    }
}
