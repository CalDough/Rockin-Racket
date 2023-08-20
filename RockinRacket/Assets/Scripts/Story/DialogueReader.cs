using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
/*
    This script is for a test feature for opening Ink files and reading dialogue, and selecting choices
    Watch https://www.youtube.com/watch?v=KSRpcftVyKg&list=PL3viUl9h9k78KsDxXoAzgQ1yRjhm7p8kl
    series to help understand and complete interaction in script/ink 
*/
public class DialogueReader : MonoBehaviour
{

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] public bool dialogueIsPlaying { get; private set; } = false;

    [SerializeField] public bool submitPressed { get; private set; } = false;
    public TextAsset DialogueFile;

    public Story currentStory;

    
    [SerializeField] private List<GameObject> dialogueChoices;
    
    [SerializeField] private List<TextMeshProUGUI> dialogueChoicesText;


    // Start is called before the first frame update
    void Start()
    {
        DialogueEnded();

        foreach(GameObject choice in dialogueChoices)
        {
            dialogueChoicesText.Add(choice.GetComponentInChildren<TextMeshProUGUI>());
        }
    }

    void Update()
    {
        if(!dialogueIsPlaying)
        {
            return;
        }
        if(submitPressed)
        {
            submitPressed = false;
            ContinueStory();
            
        }
    }

    public void SubmitButton()
    {
        submitPressed = true;
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if(currentChoices.Count > dialogueChoices.Count)
        {
            Debug.Log("Too many choices UI:" + dialogueChoices.Count + " Ink:" + currentChoices.Count);
        }
        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            dialogueChoices[index].gameObject.SetActive(true);
            dialogueChoicesText[index].text = choice.text;
            index++;
        }
        for(int i = index; i < dialogueChoices.Count; i++)
        {
            dialogueChoices[i].gameObject.SetActive(false);
        }
    }

    public void DialogueStarted()
    {
        currentStory = new Story(DialogueFile.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        if(currentStory.canContinue)
        {
            ContinueStory();
            DisplayChoices();
        }
        else
        {
            DialogueEnded();
        }
    }

    public void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            DialogueEnded();
        }
    }



    public void DialogueEnded()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public void MakeChoice(int index)
    {
        currentStory.ChooseChoiceIndex(index);
        submitPressed = true;
    }


}
