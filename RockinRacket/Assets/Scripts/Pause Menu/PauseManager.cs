using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static SceneInfo;

public class PauseManager : MonoBehaviour
{
    public GameLoadHandler gameLoadHandler;
    public GameObject menuUI;
    public GameObject UIBlocker;
    public InputActionAsset actionAsset;

    private InputAction pauseAction;
    private InputActionMap menuActionMap;
    private readonly float animationDuration = 1f;
    private readonly Vector3 openPosition = new(0, 0, 0);
    private readonly Vector3 closedPosition = new(0, -1200, 0);

    private bool isPaused;

    private void Awake()
    {
        // Assuming you have an action map named "Menu" and a Pause action within it.
        menuActionMap = actionAsset.FindActionMap("PauseMenu");
        pauseAction = menuActionMap.FindAction("Pause");

        pauseAction.performed += _ => ToggleMenu();
        UIBlocker.SetActive(false);
    }

    public void OpenStartMenu() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.StartMenu); }
    public void OpenMainMenu() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.MainMenu); }
    public void OpenVenueSelection() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.VenueSelection); }
    public void OpenBandManagement() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.BandManagement); }
    public void OpenShop() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.Shop); }
    public void OpenSettings() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.SettingsMenu); }

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

    public void DisablePauseAction()
    {
        pauseAction.Disable();
        pauseAction.Dispose();
    }

    private void OnEnable()
    {
        pauseAction.Enable();
        menuActionMap.Enable();
    }

    // TODO: THESE DON'T WORK
    private void OnDisable()
    {
        DisablePauseAction();
    }

    private void OnDestroy()
    {
        DisablePauseAction();
    }
}
