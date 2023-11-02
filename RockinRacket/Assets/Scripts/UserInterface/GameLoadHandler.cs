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
    public int mainID = 0;
    public GameObject menuUI;
    public GameObject UIBlocker;
    public InputActionAsset actionAsset;
    private InputAction pauseAction;
    private int currentSceneIndex;
    private InputActionMap menuActionMap;

    private static Stack<int> sceneIndexHistory = new();

    private bool isPaused;

    private float animationDuration = 1f;
    private Vector3 openPosition = new(0, 0, 0);
    private Vector3 closedPosition = new(0, -1200, 0);
    //private Quaternion openRotation = Quaternion.AngleAxis(0, Vector3.right);
    //private Quaternion closedRotation = Quaternion.AngleAxis(30, Vector3.right);

    private void Awake()
    {
        // Assuming you have an action map named "Menu" and a Pause action within it.
        menuActionMap = actionAsset.FindActionMap("PauseMenu");
        pauseAction = menuActionMap.FindAction("Pause");

        pauseAction.performed += _ => ToggleMenu();
    }

    private void Start()
    {
        // pause menu position
        Vector3 endPosition = closedPosition;
        //Quaternion endRotation = closedRotation;
        menuUI.transform.localPosition = endPosition;
        //menuUI.transform.localRotation = endRotation;

        UIBlocker.SetActive(false);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
        pauseAction.Disable();
        pauseAction.Dispose();
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
    public void OpenSettings() { /*SwitchToScene(9);*/ }

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
        if (currentSceneIndex == 8)
        {
            Debug.Log("Implement Pause");
            //TimeEvents.GamePaused();
        }
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

        if (currentSceneIndex == 8)
        {
            menuUI.transform.localPosition = endPosition;
            //menuUI.transform.localRotation = endRotation;
            yield break;
        }

        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startPosition = menuUI.transform.localPosition;
        //Quaternion startRotation = menuUI.transform.localRotation;

        while (counter < animationDuration)
        {
            counter += Time.deltaTime;
            menuUI.transform.localPosition = Vector3.Lerp(startPosition, endPosition, counter / animationDuration);
            //menuUI.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, counter / animationDuration);
            yield return null;
        }
    }
}
