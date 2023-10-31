using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * activates/deactivates UI groups in scene
 * handles DialogueMenu choices
 * starts dialogue with shopkeeper
*/

public class ShopManager : MonoBehaviour
{
    private enum Action {OpenShopMenu, OpenCatalog, ExitShop};
    [SerializeField] private TextAsset startConvo;
    [SerializeField] private TextAsset openCatalogConvo;
    [SerializeField] private TextAsset leaveBoughtConvo;
    [SerializeField] private TextAsset leaveNotBoughtConvo;
    [SerializeField] private TextAsset justChattingConvo;

    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField] private GameObject catalogManagerObject;
    [SerializeField] private GameObject shopMenuObject;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameLoadHandler gameLoadHandler;

    // TODO Temporary until items can be loaded at runtime
    [SerializeField] private ItemTest[] completeListOfItems;

    private bool bought;
    private Action onClose;

    private void Start()
    {
        bought = false;
        ItemInventory.Initialize(completeListOfItems);

        // initialize choices
        choicesText = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
        choicesText[0].text = "What's for sale?";
        choicesText[1].text = "Up for a chat?";
        choicesText[2].text = "I'm done shopping";

        // start cutscene
        OpenShopMenu(); // Temp until cutscene works
        //StartShopkeeperDialogue(startConvo, Action.OpenShopMenu);
    }

    public void MakeChoice(int choice)
    {
        if (choice == 0)
            StartShopkeeperDialogue(openCatalogConvo, Action.OpenCatalog);
        if (choice == 1)
            StartShopkeeperDialogue(justChattingConvo, Action.OpenShopMenu);
        if (choice == 2)
            if (bought)
                StartShopkeeperDialogue(leaveBoughtConvo, Action.ExitShop);
            else
                StartShopkeeperDialogue(leaveNotBoughtConvo, Action.ExitShop);
    }
    public void OpenShopCatalog()
    {
        shopMenuObject.SetActive(false);
        catalogManagerObject.SetActive(true);
    }
    public void OpenShopMenu()
    {
        catalogManagerObject.SetActive(false);
        shopMenuObject.SetActive(true);
    }
    public void CloseAll()
    {
        catalogManagerObject.SetActive(false);
        shopMenuObject.SetActive(false);
    }
    private void StartShopkeeperDialogue(TextAsset textAsset, Action onClose)
    {
        this.onClose = onClose;
        CloseAll();
        dialogueManager.StartDialogue(textAsset);
    }
    //private bool absorbFirstDisable = false;
    public void EndShopkeeperDialogue()
    {
        //if (!absorbFirstDisable)
        //{
        //    absorbFirstDisable = true;
        //    return;
        //}
        if (onClose == Action.OpenShopMenu)
            OpenShopMenu();
        else if (onClose == Action.OpenCatalog)
            OpenShopCatalog();
        else if (onClose == Action.ExitShop)
            gameLoadHandler.OpenMainMenu();
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
