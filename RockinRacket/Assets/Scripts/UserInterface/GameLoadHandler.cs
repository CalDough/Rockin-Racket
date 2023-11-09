using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static SceneInfo;
/*
    This script is a test UI script the Pause Menu and Main Menu
*/
public class GameLoadHandler : MonoBehaviour
{
    //public PauseManager pauseManager;

    private int currentSceneIndex;
    private static Stack<int> sceneIndexHistory = new();

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    
    public void NewData()
    {
        //AnimalManager.Instance.LoadNewAnimals();
        //BandManager.Instance.LoadDefaultBand();
        //InventoryManager.Instance.LoadDefaultItems();
        GameManager.Instance.NewGame();
    }

    // TODO replace in new script
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
        int nextScene = (int)SceneIndex.MainMenu;
        if (sceneIndexHistory.Count != 0)
            nextScene = sceneIndexHistory.Pop();
        if (currentSceneIndex != nextScene)
            SetScene(nextScene);
        else if (currentSceneIndex == (int)SceneIndex.MainMenu)
            SetScene((int)SceneIndex.StartMenu);
    }

    // for TESTING
    public void RandomScene()
    {
        SwitchToScene(new System.Random().Next(1, 7));
    }

    public void SwitchToScene(int sceneIndex)
    {
        // clear sceneIndexHistory
        if (sceneIndex == (int)SceneIndex.ConcertDefault)
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

    public void OpenStartMenu() { SwitchToScene((int)SceneIndex.StartMenu); }
    public void OpenMainMenu() { SwitchToScene((int)SceneIndex.MainMenu); }
    public void OpenVenueSelection() { SwitchToScene((int)SceneIndex.VenueSelection); }
    public void OpenBandManagement() { SwitchToScene((int)SceneIndex.BandManagement); }
    public void OpenShop() { SwitchToScene((int)SceneIndex.Shop); }
    public void OpenSettings() { SwitchToScene((int)SceneIndex.SettingsMenu); }

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

    public void ExitGame()
    {
        Application.Quit();
    }

    private void SetScene(int sceneIndex)
    {
        CustomSceneEvent.CustomTransitionCalled(sceneIndex);
        if(StateManager.Instance != null)
        {
            if (StateManager.Instance.concertIsActive) {StateManager.Instance.EndConcertEarly();}
        }
    }
}
