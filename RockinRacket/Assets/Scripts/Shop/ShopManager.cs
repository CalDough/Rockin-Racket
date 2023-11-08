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
    private enum Action {OpenShopMenu, OpenCatalog, ExitShop};
    [SerializeField] private TextAsset startConvo;
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
        
        OpenShopMenu();
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
        if (GameStateManager.Instance != null)
        {
            if (GameStateManager.Instance.ConcertActive)
            { GameStateManager.Instance.EndConcertEarly(); }
        }
    }
}
