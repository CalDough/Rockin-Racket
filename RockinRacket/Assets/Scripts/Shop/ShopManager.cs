using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * activates/deactivates UI groups in scene
 * handles DialogueMenu choices
 * starts dialogue with shopkeeper
 * handles saving and loading with ItemInventory
 * initializes and keeps track of money
*/

public class ShopManager : MonoBehaviour
{
    private enum Action {OpenShopMenu, OpenCatalog, ExitShop};
    [Header("Dialogue Assets")]
    [SerializeField] private TextAsset startConvo;
    [SerializeField] private TextAsset returnConvo;
    [SerializeField] private TextAsset openCatalogConvo;
    [SerializeField] private TextAsset leaveBoughtConvo;
    [SerializeField] private TextAsset leaveNotBoughtConvo;
    [SerializeField] private TextAsset justChattingConvo;

    [Header("Scripts")]
    [SerializeField] private CatalogManager catalogManager;
    [SerializeField] private ShopMenu shopMenu;
    [SerializeField] private ShopAudio shopAudio;
    [SerializeField] private CheckoutDialogue checkoutDialogue;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameLoadHandler gameLoadHandler;

    // TODO Temporary until items can be loaded at runtime
    //[SerializeField] private ItemTest[] completeListOfItems;

    public bool Bought { get; set; }

    private Action onClose;

    private void Start()
    {
        Bought = false;
        // TODO find a better way to do this
        //ItemInventory.Initialize(completeListOfItems);

        catalogManager.UpdateMoneyText();
        OpenShopMenu();
        shopAudio.PlayShopEnter();
        //OpenShopCatalog(); // for testing
    }

    // called by ShopMenu Dialogue Choices' buttons on press
    public void MakeChoice(int choice)
    {
        switch (choice)
        {
            case 0: StartShopkeeperDialogue(openCatalogConvo, Action.OpenCatalog); break;
            case 1: StartShopkeeperDialogue(justChattingConvo, Action.OpenShopMenu); break;
            case 2:
                {
                    if (Bought)
                    {
                        StartShopkeeperDialogue(leaveBoughtConvo, Action.ExitShop);
                    }
                    else
                    {
                        StartShopkeeperDialogue(leaveNotBoughtConvo, Action.ExitShop);
                    }
                    break;
                }
        }
    }

    // called by catalog's "back to shop" button on press
    public void ReturnToShop()
    {
        OpenShopMenu();
    }

    // called by ShopDialogueEnds when DialogueBox is disabled
    public void EndShopkeeperDialogue()
    {
        switch (onClose)
        {
            case Action.OpenShopMenu: OpenShopMenu(); break;
            case Action.OpenCatalog: OpenShopCatalog(); break;
            case Action.ExitShop: gameLoadHandler.OpenMainMenu(); break;
        }
    }

    // TODO USE IF DIALOGUE NEEDED ON START
    //private void Update()
    //{
    //    //StartShopkeeperDialogue(startConvo, Action.OpenShopMenu);
    //    enabled = false; // stops update from running after first frame
    //}

    private void OpenShopCatalog()
    {
        shopMenu.Close();
        catalogManager.Open();
    }
    private void OpenShopMenu()
    {
        shopMenu.Open();
        catalogManager.Close();
    }
    private void CloseAll()
    {
        shopMenu.Close();
        catalogManager.Close();
    }
    private void StartShopkeeperDialogue(TextAsset textAsset, Action onClose)
    {
        this.onClose = onClose;
        CloseAll();
        dialogueManager.StartDialogue(textAsset);
    }
}
