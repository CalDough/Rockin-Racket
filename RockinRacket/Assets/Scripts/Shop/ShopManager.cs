using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * activates/deactivates UI groups in scene
 * handles DialogueMenu choices
 * starts dialogue with shopkeeper
*/

public class ShopManager : MonoBehaviour
{
    private enum Action {OpenShopMenu, ReturnToShopMenu, OpenCatalog, ExitShop};
    [SerializeField] private TextAsset startConvo;
    [SerializeField] private TextAsset returnConvo;
    [SerializeField] private TextAsset openCatalogConvo;
    [SerializeField] private TextAsset leaveBoughtConvo;
    [SerializeField] private TextAsset leaveNotBoughtConvo;
    [SerializeField] private TextAsset justChattingConvo;

    [SerializeField] private CatalogManager catalogManager;
    [SerializeField] private ShopMenu shopMenu;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameLoadHandler gameLoadHandler;

    // TODO Temporary until items can be loaded at runtime
    [SerializeField] private ItemTest[] completeListOfItems;

    private bool bought;
    private Action onClose;

    private void Start()
    {
        bought = false;
        // TODO find a better way to do this
        ItemInventory.Initialize(completeListOfItems);

        //OpenShopMenu();
    }

    private void Update()
    {
        StartShopkeeperDialogue(startConvo, Action.OpenShopMenu);
        enabled = false; // stops update from running after first frame
    }

    public void MakeChoice(int choice)
    {
        switch (choice)
        {
            case 0: StartShopkeeperDialogue(openCatalogConvo, Action.OpenCatalog); break;
            case 1: StartShopkeeperDialogue(justChattingConvo, Action.OpenShopMenu); break;
            case 3:
                {
                    print("1");
                    if (bought)
                    {
                        print("2");
                        StartShopkeeperDialogue(leaveBoughtConvo, Action.ExitShop);
                    }
                    else
                    {
                        print("3");
                        StartShopkeeperDialogue(leaveNotBoughtConvo, Action.ExitShop);
                    }
                    break;
                }
        }
    }

    public void ReturnToShop()
    {
        //StartShopkeeperDialogue(returnConvo, Action.OpenShopMenu);
        OpenShopMenu();
    }

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
    public void EndShopkeeperDialogue()
    {
        switch (onClose)
        {
            case Action.OpenShopMenu: OpenShopMenu(); break;
            //case Action.ReturnToShopMenu: ReturnToShop(); break;
            case Action.OpenCatalog: OpenShopCatalog(); break;
            case Action.ExitShop: gameLoadHandler.OpenMainMenu(); break;
        }
    }

    public void CloseShopScene()
    {
        CustomSceneEvent.CustomTransitionCalled(1);
        TimeEvents.GameResumed();
        if (StateManager.Instance != null)
        {
            if (StateManager.Instance.concertIsActive)
            { StateManager.Instance.EndConcertEarly(); }
        }
    }
}
