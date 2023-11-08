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
    public int mainID = 0;
    public GameObject menuUI;
    public GameObject UIBlocker;
    public InputActionAsset actionAsset;
    private InputAction pauseAction;
    private int currentSceneIndex;
    private InputActionMap menuActionMap;

    private static Stack<int> sceneIndexHistory = new();

    private bool isPaused;

    private readonly float animationDuration = 1f;
    private readonly Vector3 openPosition = new(0, 0, 0);
    private readonly Vector3 closedPosition = new(0, -1200, 0);
    //private Quaternion openRotation = Quaternion.AngleAxis(0, Vector3.right);
    //private Quaternion closedRotation = Quaternion.AngleAxis(30, Vector3.right);

    private void Awake()
    {
        actionAsset.FindActionMap("PauseMenu").Disable();
        // Assuming you have an action map named "Menu" and a Pause action within it.
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        menuActionMap = actionAsset.FindActionMap("PauseMenu");
        pauseAction = menuActionMap.FindAction("Pause");

        pauseAction.performed += _ => ToggleMenu();
        UIBlocker.SetActive(false);
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
        pauseAction.Disable();
        pauseAction.Dispose();
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

    private void OnEnable()
    {
        pauseAction.Enable();
        menuActionMap.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
        menuActionMap.Disable();
    }

    private void OnDestroy()
    {
        pauseAction.Disable();
    }

    private void OpenPauseMenu()
    {
        TimeEvents.GamePaused();
        //if (currentSceneIndex == 8)
        //{
        //    Debug.Log("Implement Pause");
        //    //TimeEvents.GamePaused();
        //}
        //menuUI.SetActive(true);
        StartCoroutine(PauseMenuAnimation(true));
    }

    private void ClosePauseMenu()
    {
        TimeEvents.GameResumed();
        //menuUI.SetActive(false);
        StartCoroutine(PauseMenuAnimation(false));
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
        if (isPaused)
        {
            UIBlocker.SetActive(false);
            ClosePauseMenu();
        }
        else
        {
            UIBlocker.SetActive(true);
            OpenPauseMenu();
        }
        isPaused = !isPaused;
        //if (menuUI.activeSelf)
        //{
        //    ClosePauseMenu();
        //}
        //else
        //{
        //    OpenPauseMenu();
        //}
    }

    public IEnumerator PauseMenuAnimation(bool toOpen)
    {
        Vector3 endPosition = closedPosition;
        //Quaternion endRotation = closedRotation;
        if (toOpen)
        {
            menuUI.transform.localPosition = closedPosition;
            endPosition = openPosition;
            //endRotation = openRotation;
        }

        //if (currentSceneIndex == 8)
        //{
        //    menuUI.transform.localPosition = endPosition;
        //    //menuUI.transform.localRotation = endRotation;
        //    yield break;
        //}

        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startPosition = menuUI.transform.localPosition;
        //Quaternion startRotation = menuUI.transform.localRotation;

        while (counter < animationDuration)
        {
            counter += Time.unscaledDeltaTime;
            menuUI.transform.localPosition = Vector3.Lerp(startPosition, endPosition, counter / animationDuration);
            //menuUI.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, counter / animationDuration);
            yield return null;
        }
    }
}
