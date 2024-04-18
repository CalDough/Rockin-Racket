using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicLoader : MonoBehaviour
{
    [SerializeField] private TextAsset cinematic_text;
    [SerializeField] private GameObject toNextSceneButton;
    
    [SerializeField] private Sprite[] cinematicSlides;
    [SerializeField] private GameObject cinematicViewer;
    private bool showingCine = false;
    private DialogueManager dialogueManager;
    private bool hasInitialized = false;
    private int cinematicIndex = 0;


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

    public void MidDialogueCinematic(int numSlides)
    {

    }

    public void ShowCinematic()
    {
        if (showingCine)
        {
            if (DialogueManager.GetInstance().dialogueActive)
            {
                HideCinematic();
            }
            else if (cinematicIndex < cinematicSlides.Length)
            {
                cinematicViewer.GetComponent<Image>().sprite = cinematicSlides[cinematicIndex];
                cinematicIndex++;
            }
            else
            {
                HideCinematic();
            }
        }
        else
        {
            showingCine = true;
            cinematicViewer.GetComponent<Image>().sprite = cinematicSlides[cinematicIndex];
            cinematicViewer.SetActive(true);
            cinematicIndex++;
        }
    }

    public void HideCinematic()
    {
        showingCine = false;
        cinematicViewer.SetActive(false);
    }

    void OnDialogueEnd(object sender, EventArgs args)
    {
        if (cinematicIndex < cinematicSlides.Length)
        {
            ShowCinematic();
        }
        toNextSceneButton.SetActive(true);
    }

    void OnDestroy()
    {
        DialogueEvents.DialogueEnd -= OnDialogueEnd;
        cinematicIndex = 0;
    }
}
