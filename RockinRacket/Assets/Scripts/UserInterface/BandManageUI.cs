using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
    This script is a test UI script for the Band Management scene. It allows going through the band members to display their skills, items etc, and
    changing out their items or clothes.
    It will also allow the player to talk to the animal and activate any cutscenes or dialogue the StoryManager has queued for them.
*/

public class BandManageUI : MonoBehaviour
{
    public GameObject ItemGivePanel;
    public GameObject ItemChangePanel;
    public GameObject DialoguePanel;

    public TextMeshProUGUI InfoTextBox; 
    public BandPosition SelectedPosition;

    public List<GameObject> UIBand = new List<GameObject>();
    
    public List<BandPosition> Band = new List<BandPosition>();

    public bool CanChangeMember = true;
    public float transitionDuration = 0.5f;
    private bool isTransitioning = false;

    public DialogueReader SceneReader;
    // Start is called before the first frame update
    void Start()
    {
        CloseAllPanels();
                                                  // If you want to fix the ordering of the raccoons in the scene to match the code
                                                  // I chose to fix it through code instead of moving scene objects
        Band.Add(BandManager.Instance.AnimalOne); //Vocals Raccoon

        Band.Add(BandManager.Instance.AnimalTwo); //Strings Raccoon
        
        Band.Add(BandManager.Instance.AnimalFour); //Percussion Raccoon

        Band.Add(BandManager.Instance.Manager); // This would be the manager Raccoon, but there is no sprite for it

        Band.Add(BandManager.Instance.AnimalThree); //Strings Raccoon
        
        
        SelectedPosition = Band[0];
        
        DisplayInfo();
    }
    public void PreviousMember()
    {
        if (!CanChangeMember|| isTransitioning)
            return;

        CloseAllPanels();

        StartCoroutine(TransitionMembers(true));
    }

    public void NextMember()
    {
        if (!CanChangeMember || isTransitioning)
            return;

        CloseAllPanels();

        StartCoroutine(TransitionMembers(false));
    }

    
    private IEnumerator TransitionMembers(bool isPrevious)
    {
        isTransitioning = true;

        // Define the starting positions and target positions for each member.
        List<Vector3> startPositions = new List<Vector3>();
        List<Vector3> targetPositions = new List<Vector3>();
        List<int> targetSiblingIndices = new List<int>();  // List to store the target sibling indices.

        foreach (var member in UIBand)
        {
            startPositions.Add(member.transform.position);
        }

        if (isPrevious)
        {
            targetPositions.Add(UIBand[UIBand.Count - 1].transform.position);
            targetPositions.AddRange(startPositions.GetRange(0, UIBand.Count - 1));
            targetSiblingIndices.Add(UIBand[UIBand.Count - 1].transform.GetSiblingIndex());
            for (int i = 0; i < UIBand.Count - 1; i++)
            {
                targetSiblingIndices.Add(UIBand[i].transform.GetSiblingIndex());
            }

            BandPosition lastMember = Band[Band.Count - 1];
            Band.RemoveAt(Band.Count - 1);
            Band.Insert(0, lastMember);
        }
        else
        {
            targetPositions.AddRange(startPositions.GetRange(1, UIBand.Count - 1));
            targetPositions.Add(startPositions[0]);
            for (int i = 1; i < UIBand.Count; i++)
            {
                targetSiblingIndices.Add(UIBand[i].transform.GetSiblingIndex());
            }
            targetSiblingIndices.Add(UIBand[0].transform.GetSiblingIndex());

            BandPosition firstMember = Band[0];
            Band.RemoveAt(0);
            Band.Add(firstMember);
        }

        float elapsedTime = 0;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            for (int i = 0; i < UIBand.Count; i++)
            {
                UIBand[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            yield return null;  // Wait for the next frame.
        }

        // Ensure that the final position is set exactly.
        for (int i = 0; i < UIBand.Count; i++)
        {
            UIBand[i].transform.position = targetPositions[i];
            UIBand[i].transform.SetSiblingIndex(targetSiblingIndices[i]);
        }

        SelectedPosition = Band[0];
        DisplayInfo();
        isTransitioning = false;
    }

    //Will open cutscene or dialogue menu if availiable for member, else just show some text about something?
    public void TalkToMember()
    {
        CanChangeMember = false;
        //while dialogue or cutscene is active, I think it would be easier to keep it the same member until that finishes to swap
        if(StoryManager.Instance.GetDialogue() == true)
        {
            SceneReader.DialogueStarted();
            CanChangeMember = true;
            DialoguePanel.SetActive(true);
        }
        else
        {
            DialoguePanel.SetActive(false);
        }

    }

    public void EndDialogue()
    {
        CanChangeMember = true;
        if(DialoguePanel == null)
        {return;}

        if (DialoguePanel.activeSelf)
        {
            DialoguePanel.SetActive(false);
        }
        else
        {
            DialoguePanel.SetActive(true);
        }
    }


    //set itemgivepanel to active, close other panels
    public void GiveItem()
    {
        if(DialoguePanel.activeSelf)
        {return;}
        if(ItemGivePanel == null)
        {return;}

        if (ItemGivePanel.activeSelf)
        {
            ItemGivePanel.SetActive(false);
        }
        else
        {
            ItemGivePanel.SetActive(true);
        }
    }

    //set itemchangepanel to active, close other panels
    public void ChangeGear()
    {
        if(DialoguePanel.activeSelf)
        {return;}
        if(ItemChangePanel == null)
        {return;}

        if (ItemChangePanel.activeSelf)
        {
            ItemChangePanel.SetActive(false);
        }
        else
        {
            ItemChangePanel.SetActive(true);
        }
    }

    //Display current member info such as
    //Skill levels
    //equipment
    //mood
    //Role bonus
    public void DisplayInfo()
    {
        if (SelectedPosition == null || InfoTextBox == null)
        {
            Debug.LogWarning("SelectedPosition or InfoTextBox is not set.");
            return;
        }

        string infoText = "";

        infoText += "Role: " + SelectedPosition.CurrentRole.ToString() + "\n";
        infoText += "Clothes: " + (SelectedPosition.Clothes != null ? SelectedPosition.Clothes.ItemName : "None") + "\n";
        infoText += "Instrument: " + (SelectedPosition.Instrument != null ? SelectedPosition.Instrument.ItemName : "None") + "\n";

        infoText += "Skills:\n";
        foreach (Skill skill in SelectedPosition.Skills)
        {
            infoText += "- " + skill.SkillName.ToString() + ": Level " + skill.Level.ToString() + "\n";
        }

        InfoTextBox.text = infoText;
    }

    public void CloseAllPanels()
    {
        ItemChangePanel.SetActive(false);
        ItemGivePanel.SetActive(false);
        DialoguePanel.SetActive(false);
    }


    

}
