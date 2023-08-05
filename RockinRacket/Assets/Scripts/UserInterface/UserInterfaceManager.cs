using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{
    
    public Animator transition;
    public float transitionTime = 1f;
    public string loadingSceneName = "LoadingScene";
    public UIState CurrentState = UIState.Main;
    //public MenuState CurrentState = MenuState.MainMenu;
    public static UserInterfaceManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this);
        } 
    }
    
    void OnEnable()
    {
        UIEvent.UserInterfaceChanged += HandleUIChanged;
    }

    void OnDisable()
    {
        UIEvent.UserInterfaceChanged -= HandleUIChanged;
    }

    void HandleUIChanged(object sender, UIEventArgs args)
    {
        
        CurrentState = args.CurrentState;
        
    }

    public void SwitchSceneIndex(int sceneIndex)
    {
        StartCoroutine(LoadSceneIndexAsync(sceneIndex));
    }

    public void InvokeUIChanged(UIState newState)
    {
        UIEvent.UIChanged(newState);
    }

    public void SwitchScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {

        if(transition != null)
        {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        }
         // Load the loading scene synchronously first
        SceneManager.LoadScene(loadingSceneName);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            // Here you can update UI for loading progress with asyncOperation.progress
            yield return null;
        }
    }

    private IEnumerator LoadSceneIndexAsync(int sceneName)
    {
        if(transition != null)
        {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        }
         // Load the loading scene synchronously first
        SceneManager.LoadScene(loadingSceneName);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            // Here you can update UI for loading progress with asyncOperation.progress
            yield return null;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
