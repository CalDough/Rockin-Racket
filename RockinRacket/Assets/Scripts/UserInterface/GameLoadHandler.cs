using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
/*
    This script is a test UI script the Pause Menu and Main Menu
*/
public class GameLoadHandler : MonoBehaviour
{      
    public int MainID = 0;
    public GameObject MenuUI;
    public InputActionAsset actionAsset;
    private InputAction pauseAction;
    private int currentSceneIndex;

    private static Stack<int> sceneIndexHistory = new();

    private void Awake()
    {
        //Save();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Assuming you have an action map named "Menu" and a Pause action within it.
        var menuActionMap = actionAsset.FindActionMap("PauseMenu");
        pauseAction = menuActionMap.FindAction("Pause");

        pauseAction.performed += _ => ToggleMenu();
    }
    
    public void NewData()
    {
        //AnimalManager.Instance.LoadNewAnimals();
        //BandManager.Instance.LoadDefaultBand();
        //InventoryManager.Instance.LoadDefaultItems();
        GameManager.Instance.NewGame();
    }

    public void LoadAllData()
    {
        //AnimalManager.Instance.LoadAnimals();
        //BandManager.Instance.LoadBand();
        //InventorySaver.Instance.LoadInventory();
        GameSaver.Load();
    }

    public void SaveAllData()
    {
        //AnimalManager.Instance.SaveAnimals();
        //BandManager.Instance.SaveBand();
        //InventorySaver.Instance.SaveInventory();
        GameSaver.Save();
    }

    public void SaveAndExit()
    {
        //AnimalManager.Instance.SaveAnimals();
        //BandManager.Instance.SaveBand();
        //InventorySaver.Instance.SaveInventory();
        GameSaver.Save();
        ItemInventory.Save(null);
        Application.Quit();
    }
    public void Save()
    {
        //AnimalManager.Instance.SaveAnimals();
        //BandManager.Instance.SaveBand();
        //InventorySaver.Instance.SaveInventory();
        GameSaver.Save();
        ItemInventory.Save(null);
    }

    public void GoBackScene()
    {
        int nextScene = 0;
        if (sceneIndexHistory.Count != 0)
            nextScene = sceneIndexHistory.Pop();
        if (currentSceneIndex != nextScene)
            SetScene(nextScene);
    }

    // for TESTING
    public void RandomScene()
    {
        SwitchToScene(new System.Random().Next(2, 7));
    }

    public void SwitchToScene(int sceneIndex)
    {
        // clear sceneIndexHistory
        if (sceneIndex == 8 || sceneIndex == 10)
        {
            sceneIndexHistory = new();
            SetScene(currentSceneIndex);
        }
        if (currentSceneIndex != sceneIndex)
        {
            AddSceneIndexToHistory(currentSceneIndex);
            currentSceneIndex = sceneIndex;
            SetScene(currentSceneIndex);
        }
        Debug.Log("Current Index: " + currentSceneIndex + "  ||  " + "Next Index: " + sceneIndex + "  ||  " + "history size: " + sceneIndexHistory.Count.ToString());
    }

    public void OpenMainMenu() { SwitchToScene(0); }
    public void OpenSelectVenues() { SwitchToScene(2); }
    public void OpenShop() { SwitchToScene(4); }
    public void OpenSettings() { SwitchToScene(9); }

    //private void PrintSceneIndexHistory()
    //{
    //    string print = "Current Index: " + currentSceneIndex;
    //    foreach (int sceneIndex in sceneIndexHistory.ToArray())
    //        print += "Stack Index: " + sceneIndex + "\n";
    //    Debug.Log(print);
    //}

    private void AddSceneIndexToHistory(int sceneIndex)
    {
        // do nothing if we already have that scene at the top of the stack
        //if (sceneIndexHistory.Count != 0)
        //    if (sceneIndex == sceneIndexHistory.Peek())
        //        return;
        // else add it to the stack
        sceneIndexHistory.Push(sceneIndex);
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    private void OpenPauseMenu()
    {
        if (currentSceneIndex == 8)
            TimeEvents.GamePaused();
        if (MenuUI != null)
            MenuUI.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        TimeEvents.GameResumed();
        if(MenuUI != null)
        {
        MenuUI.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void SetScene(int sceneIndex)
    {
        CustomSceneEvent.CustomTransitionCalled(sceneIndex);
        ClosePauseMenu();
        if(GameStateManager.Instance != null)
        {
            if( GameStateManager.Instance.ConcertActive)
            {GameStateManager.Instance.EndConcertEarly();}
        }
    }

    public void ToggleMenu()
    {
        if(MenuUI == null)
        {return;}

        if (MenuUI.activeSelf)
        {
            ClosePauseMenu();
        }
        else
        {
            OpenPauseMenu();
        }
    }
    
}
