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

    private Stack<int> sceneIndexHistory = new();
    
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

        Application.Quit();
    }
    public void Save()
    {
        //AnimalManager.Instance.SaveAnimals();
        //BandManager.Instance.SaveBand();
        //InventorySaver.Instance.SaveInventory();
        GameSaver.Save();
    }

    public void GoBackScene()
    {
        if (sceneIndexHistory.Count != 0)
            currentSceneIndex = sceneIndexHistory.Pop();
        else
            currentSceneIndex = 1;
        SetScene(currentSceneIndex);
    }

    // for TESTING
    public void RandomScene()
    {
        SwitchToScene(new System.Random().Next(1, 12));
    }

    public void SwitchToScene(int sceneIndex)
    {
        AddSceneIndexToHistory(currentSceneIndex);
        if (currentSceneIndex != sceneIndex)
        {
            currentSceneIndex = sceneIndex;
            SetScene(currentSceneIndex);
        }
    }

    private void AddSceneIndexToHistory(int sceneIndex)
    {
        // do nothing if we already have that scene at the top of the stack
        if (sceneIndexHistory.Count != 0)
            if (sceneIndex == sceneIndexHistory.Peek())
                return;
        // else add it to the stack
        sceneIndexHistory.Push(sceneIndex);
    }

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Assuming you have an action map named "Menu" and a Pause action within it.
        var menuActionMap = actionAsset.FindActionMap("PauseMenu");
        pauseAction = menuActionMap.FindAction("Pause");
        
        pauseAction.performed += _ => ToggleMenu();
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    public void OpenMenu()
    {
        TimeEvents.GamePaused();
        if(MenuUI != null)
        {
        MenuUI.SetActive(true);
        }
    }

    public void CloseMenu()
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
        Debug.Log(sceneIndexHistory.Count);
        CustomSceneEvent.CustomTransitionCalled(sceneIndex);
        CloseMenu();
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
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }
    
}
