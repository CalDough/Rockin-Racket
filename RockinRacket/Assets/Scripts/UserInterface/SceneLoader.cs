using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


    /*
    Ok a lot of the functions below are just overloaded function variations
    Simply, there is switching scenes based on index or name
    There is switching scenes based on custom parameters like different transitions which will spawn, timing, and different loading screen
    There is switching scenes based on a scriptable object, which can set the parameters outside of code and place onto buttons or stuff

    Other functions below those play the animator or load the final scene asyc so that if it is a large scene, 
    the player gets to look at a loading screen instead of frozen level 

    Below that are two classes to handle using an event to call this class instead of making this class a singleton
    Example use if the GameLoadHandler which manages the main menu and pause screen which needs to call scenechange for settings and going to the main menu

    When a scene is loaded, it will also create a animator to play the loading animation.
    */

public class SceneLoader : MonoBehaviour
{
    
    [Header("Scene Transition Settings")]
    public GameObject DefaultAnimatorPrefab;
    public float transitionTime = 1f;
    public string loadingSceneName = "LoadingScene";
    private Animator transition;
    private bool isPrefabReady = false;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CustomSceneEvent.CustomTransition += HandleCustomTransition;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        CustomSceneEvent.CustomTransition -= HandleCustomTransition;
    }  

    private void HandleCustomTransition(object sender, CustomSceneEventArgs e)
    {
        Debug.Log("Scene changing to " + e.SceneIndex);
        StartCoroutine(LoadSceneIndexAsync(e.SceneIndex, e.AnimatorPrefab, e.TransitionTime, e.LoadingSceneName));
    }

    public void SwitchSceneIndex(int SceneIndex)
    {
        int sceneIndex = SceneIndex;
        GameObject animatorPrefab = null;
        float customTransitionTime = this.transitionTime;
        string customLoadingSceneName = this.loadingSceneName;
        StartCoroutine(LoadSceneIndexAsync(sceneIndex, animatorPrefab, customTransitionTime, customLoadingSceneName));
    }

    public void SwitchScene(TransitionData tData)
    {
        int sceneIndex = tData.sceneIndex;
        GameObject animatorPrefab = tData.animatorPrefab;

        float customTransitionTime = this.transitionTime;
        if(tData.transitionTime > 0)
        {customTransitionTime = tData.transitionTime;}

        string customLoadingSceneName = this.loadingSceneName;
        if(String.IsNullOrEmpty(tData.loadingSceneName))
        {customLoadingSceneName = tData.loadingSceneName;}
        
        StartCoroutine(LoadSceneIndexAsync(sceneIndex, animatorPrefab, customTransitionTime, customLoadingSceneName));
    }

    public void SwitchScene(string sceneName, GameObject animatorPrefab = null, float? customTransitionTime = null, string customLoadingSceneName = null)
    {
        StartCoroutine(LoadSceneAsync(sceneName, animatorPrefab, customTransitionTime, customLoadingSceneName));
    }

    public void SwitchSceneIndex(int sceneIndex, GameObject animatorPrefab = null, float? customTransitionTime = null, string customLoadingSceneName = null)
    {
        StartCoroutine(LoadSceneIndexAsync(sceneIndex, animatorPrefab, customTransitionTime, customLoadingSceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, GameObject animatorPrefab, float? customTransitionTime, string customLoadingSceneName)
    {
        PlayTransition(animatorPrefab);
        
        yield return new WaitForSeconds(customTransitionTime ?? transitionTime);
        
        SceneManager.LoadScene(customLoadingSceneName ?? loadingSceneName);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator LoadSceneIndexAsync(int sceneIndex, GameObject animatorPrefab, float? customTransitionTime, string customLoadingSceneName)
    {
        PlayTransition(animatorPrefab);
        
        yield return new WaitForSeconds(customTransitionTime ?? transitionTime);

        SceneManager.LoadScene(customLoadingSceneName ?? loadingSceneName);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private void PlayTransition(GameObject animatorPrefab)
    {
        if (animatorPrefab == null)
        {
            animatorPrefab = DefaultAnimatorPrefab;
        }

        if (animatorPrefab)
        {
            Canvas canvas = FindCanvas();
            if (canvas == null)
            {return;}

            GameObject transitionObj = Instantiate(animatorPrefab, canvas.transform, false);
            transition = transitionObj.GetComponent<Animator>();
            transitionObj.SetActive(true);

            if (transition)
            {
                transition.SetTrigger("Start");
            }
        }
    }
    /*
    private void PlayTransition(GameObject animatorPrefab, float? customTransitionTime)
    {
        if (animatorPrefab == null)
        {
            animatorPrefab = DefaultAnimatorPrefab;
        }

        if (animatorPrefab)
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("No canvas found in the scene.");
                return;
            }

            GameObject transitionObj = Instantiate(animatorPrefab, canvas.transform);
            transitionObj.SetActive(false);  
            transition = transitionObj.GetComponent<Animator>();

            isPrefabReady = true;
            
            StartCoroutine(ActivateAndTriggerTransition(transitionObj, transition));
        }
    }
    
    */

    private IEnumerator ActivateAndTriggerTransition(GameObject transitionObj, Animator transitionAnimator)
    {
        while(!isPrefabReady)
        {
            yield return null;  
        }
        
        transitionObj.SetActive(true); 
        yield return null;  
        transitionAnimator.SetTrigger("Start"); 
    }

    
    private void PlayReverseTransition(GameObject animatorPrefab)
    {
        if (animatorPrefab == null)
        {
            animatorPrefab = DefaultAnimatorPrefab;
        }

        if (animatorPrefab)
        {
            Canvas canvas = FindCanvas();
            if (canvas == null)
            {return;}

            GameObject transitionObj = Instantiate(animatorPrefab, canvas.transform, false);
            transition = transitionObj.GetComponent<Animator>();
            transitionObj.SetActive(true);

            if (transition)
            {
                transition.SetTrigger("Reverse");
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Instantiate the transition prefab in the newly loaded scene
        PlayReverseTransition(DefaultAnimatorPrefab);
    }
 
    private Canvas FindCanvas()
    {
        GameObject canvasObj = GameObject.FindGameObjectWithTag("MainCanvas");
        if (canvasObj != null)
        {return canvasObj.GetComponent<Canvas>();}

        canvasObj = GameObject.Find("MainCanvas");
        if (canvasObj != null)
        {return canvasObj.GetComponent<Canvas>();}

        Debug.LogWarning("canvas with tag 'MainCanvas' or name 'MainCanvas' not found in the scene");
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No canvas found in the scene.");
            return null;
        }
        return canvas;
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}

public static class CustomSceneEvent
{
    public static event EventHandler<CustomSceneEventArgs> CustomTransition;

    public static void CustomTransitionCalled(int sceneIndex, GameObject animatorPrefab, float transitionTime, string loadingSceneName)
    {
        CustomTransition?.Invoke(null, new CustomSceneEventArgs(sceneIndex, animatorPrefab, transitionTime, loadingSceneName));
    }

    public static void CustomTransitionCalled(int sceneIndex)
    {
        CustomTransition?.Invoke(null, new CustomSceneEventArgs(sceneIndex, null, 1, null));
    }

    public static void CustomTransitionCalled(TransitionData transitionData)
    {
        CustomTransition?.Invoke(null, new CustomSceneEventArgs(transitionData));
    }
    
}

public class CustomSceneEventArgs : EventArgs
{
    public int SceneIndex { get; private set; }
    public GameObject AnimatorPrefab { get; private set; }
    public float TransitionTime { get; private set; } = 1f;
    public string LoadingSceneName { get; private set; } = "LoadingScene";

    public CustomSceneEventArgs(int sceneIndex, GameObject animatorPrefab, float transitionTime, string loadingSceneName)
    {
        SceneIndex = sceneIndex;
        AnimatorPrefab = animatorPrefab;
        TransitionTime = transitionTime;
        LoadingSceneName = loadingSceneName;
    }

    public CustomSceneEventArgs(TransitionData transitionData)
    {
        SceneIndex = transitionData.sceneIndex;
        AnimatorPrefab = transitionData.animatorPrefab;
        TransitionTime = transitionData.transitionTime;
        LoadingSceneName = transitionData.loadingSceneName;
    }
}