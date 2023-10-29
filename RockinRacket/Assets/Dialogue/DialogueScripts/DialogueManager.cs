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
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Animator harveyAnimator;
    [SerializeField] private GameObject characterVignette;
    [SerializeField] private GameObject harveyVignette;
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
    
    // Used to determine if a DisplayLine() coroutine is already active
    private Coroutine displayLineCoroutine;
    // Used to determine when the continueButton can be pressed
    private bool canContinueToNextLine = false;

    // Tracks which animator to update
    private Animator currentAnimator;


    // Tag values that will get checked for and processed in the HandleTags() method called in the ContinueStory() method
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
        currentAnimator = characterAnimator;

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

        // Conditionals determining if story will continue, and if the continueButton should be active
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
        characterAnimator.Play("default");
        layoutAnimator.Play("left");

        ContinueStory();        
    }

    // Method that does what it says
    private void ContinueStory() 
    {
        if (currentStory.canContinue)
        {
            // Stops the active displayLineCoroutine, should it exist 
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            
            // Checks the next storyline, and if it has no writable content, will go to the nextline in the story, 
            // should a nextline exist (should never run into a case where there is not a nextline at this point)
            string nextLine = currentStory.Continue();
            if (nextLine.Trim() == "" && currentStory.canContinue)
            {
                nextLine = currentStory.Continue();
            }
            // Assigns the new coroutine to displayLineCoroutine, so that the above check can see if a coroutine is already running
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));

            HandleTags(currentStory.currentTags);
        }
        else
        {
            StopDialogue();
        }
    }

    // Method to be used as a coroutine to create the "typing effect" of the dialogue system
    private IEnumerator DisplayLine(string line)
    {
        // Empty the dialogueText box to get the effect
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // Hiding items while text is typing
        HideChoices();

        canContinueToNextLine = false;
        // Keeps track of if a rich text tag appears (Rich text tags allow for specific substrings to be in different colors, fonts, or bolded/italic)
        bool isAddingRichTextTag = false;

        // Display each character one at a time (achieving the typing effect)
        foreach (char letter in line.ToCharArray())
        {
            // Allows for skipping dialoguetyping should the player want to
            if (continuePressed)
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }
            // Checks for the opening tag character for rich text tags, and essentially works as a counter disruptor for the rich text tags
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

    // Hides all choices
    private void HideChoices()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
    }

    // Method that handles the ink tags, which for ours is the "portrait", "layout", and "speaker"
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
            // Splits each tag into its key and its value
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Based on the tagKey, determines what part of the dialogue system to update to tagValue
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    speakerName.text = tagValue;
                    if (tagValue == "Harvey")
                    {
                        currentAnimator = harveyAnimator;
                        harveyVignette.SetActive(true);
                        characterVignette.SetActive(false);
                    }
                    else
                    {
                        currentAnimator = characterAnimator;
                        harveyVignette.SetActive(false);
                        characterVignette.SetActive(true);
                    }
                    break;
                case PORTRAIT_TAG:
                    if (currentAnimator == harveyAnimator)
                    {
                        harveyAnimator.Play(tagValue.ToLower());
                    }
                    else
                    {
                        characterAnimator.Play(tagValue.ToLower());
                    }
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

        // Preventative check to ensure there are enough choice UI objects to hold the story choices
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were found than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        // Goes through the story choices, and adds them to the choice UI objects, incrementing index each time
        int index = 0;
        for(int i = 0; i < currentChoices.Count; i++)
        {
            choices[i].gameObject.SetActive(true);
            choicesText[i].text = currentChoices[i].text;
            index++;
        }

        // From index, disables unnecessary choice UI objects
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

    // Based on the currentstory, and it's dialogueVariables, attempts to return the variableValue of requested variableName
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