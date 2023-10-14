using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.05f;
    
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Load Globals JSON File")]
    [SerializeField] private TextAsset loadGlobalsJSON;


    // Internal variable that tracks the current state of the dialogue currently given (Story is the Plugins class for saving the ink file) 
    private Story currentStory;
    // Used to track when the dialogue window is up
    public bool dialogueActive;
    // Used as an internal variable to connect multiple input sources to continue
    private bool continuePressed;
    public int numChoices = 0;
    private bool isAddingRichTextTag = false;
    
    
    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;


    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private DialogueVariables dialogueVariables;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in this scene");
        }
        instance = this;
        dialoguePanel.SetActive(false);
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);

        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        choicesText = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        continuePressed = false; 
    }

    private void Update()
    {
        // Forces Update to stop for this object if the dialogue is inactive, helping memory
        if (!dialogueActive)
        {
            return;
        }

        if(canContinueToNextLine 
           && currentStory.currentChoices.Count == 0 
           && continuePressed)
        {
            continuePressed = false;
            ContinueStory();
        }
        else if (currentStory.currentChoices.Count == 0 || !canContinueToNextLine)
        {
            continueButton.SetActive(true);
        }
        else 
        {
            continueButton.SetActive(false);
        }
        continuePressed = false;
    }

    // Button method to continue to nextline
    public void Button_Continue()
    {
        continuePressed = true;
    }

    // Method that does what it says
    public void StartDialogue(TextAsset InkJSON)
    {
        currentStory = new Story(InkJSON.text);
        dialogueActive = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        speakerName.text = "?????????";
        portraitAnimator.Play("default");
        layoutAnimator.Play("left");

        ContinueStory();        
    }

    // Method that does what it says
    private void ContinueStory() 
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            HandleTags(currentStory.currentTags);
        }
        else
        {
            StopDialogue();
        }
    }


    private IEnumerator DisplayLine(string line)
    {
        // Empty the dialogueText box to get the effect
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // Hiding items while text is typing
        HideChoices();

        canContinueToNextLine = false;

        // Display each character one at a time (achieving the typing effect)
        foreach (char letter in line.ToCharArray())
        {
            if (continuePressed)
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }
            
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // Actions to take after the line has finished displaying all characters
        DisplayChoices();

        canContinueToNextLine = true;
    }


    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }


    private void HandleTags(List<String> currentTags)
    {
        //Iterates through currentTags and handles accordingly (For each loops can be memory intensive, so I rarely use them unless situation warrants, such as hashmaps)
        for (int i = 0; i < currentTags.Count; i++)
        {
            string tag = currentTags[i];
            string[] splitTag = tag.Split(":");
            //Ensure tag comes in how we want it
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    speakerName.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue.ToLower());
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently handled: " + tag);
                    break;
            }
        }
    }

    // Method that does what it says
    private void StopDialogue()
    {
        DialogueEvents.InvokeDialogueEnd();
        dialogueActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        dialogueVariables.StopListening(currentStory);
    }

    // Method that does what it says
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were found than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        for(int i = 0; i < currentChoices.Count; i++)
        {
            choices[i].gameObject.SetActive(true);
            choicesText[i].text = currentChoices[i].text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        numChoices = currentChoices.Count;
    }

    // Method that states the choice of the given input
    // Might require updating inputManager for what button is pressed
    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
}