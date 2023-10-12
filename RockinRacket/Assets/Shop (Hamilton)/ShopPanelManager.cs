using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPanelManager : MonoBehaviour
{
    // TEMPORARY until I make the dialogue save system
    [SerializeField] private TextAsset textFile;

    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField] private GameObject catalogPanels;
    [SerializeField] private GameObject dialogueMenuPanel;

    [SerializeField] private DialogueManager dialogueManager;

    private void Awake()
    {
        catalogPanels.SetActive(false);
        dialogueMenuPanel.SetActive(true);
        choicesText = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        // current setup
        choicesText[0].text = "Look at catalog";
        choicesText[1].text = "Mention the weather";
        choicesText[2].text = "I'm done shopping";
    }

    public void MakeChoice(int choice)
    {
        if (choice == 0)
            OpenShopCatalog();
        if (choice == 1)
            StartShopkeeperDialogue();
        if (choice == 2)
            CloseShopScene();
    }
    public void OpenShopCatalog()
    {
        dialogueMenuPanel.SetActive(false);
        catalogPanels.SetActive(true);
    }
    public void CloseShopCatalog()
    {
        catalogPanels.SetActive(false);
        dialogueMenuPanel.SetActive(true);
    }
    public void StartShopkeeperDialogue()
    {
        dialogueMenuPanel.SetActive(false);
        dialogueManager.StartDialogue(textFile);
    }
    public void EndShopkeeperDialogue()
    {
        dialogueMenuPanel.SetActive(true);
    }
    public void CloseShopScene()
    {
        CustomSceneEvent.CustomTransitionCalled(1);
        TimeEvents.GameResumed();
        //if (MenuUI != null)
        //{
        //    MenuUI.SetActive(false);
        //}
        if (GameStateManager.Instance != null)
        {
            if (GameStateManager.Instance.ConcertActive)
            { GameStateManager.Instance.EndConcertEarly(); }
        }
    }
}
