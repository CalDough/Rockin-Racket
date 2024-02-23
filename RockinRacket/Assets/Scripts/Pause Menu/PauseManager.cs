using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class PauseManager : MonoBehaviour
{
    public GameLoadHandler gameLoadHandler;
    public GameObject menuUI;
    public GameObject UIBlocker;
    public InputActionAsset actionAsset;

    public StudioEventEmitter buttonPress;
    public StudioEventEmitter buttonRelease;

    private InputAction pauseAction;
    private InputActionMap menuActionMap;
    private readonly float animationDuration = 1f;
    private readonly Vector3 openPosition = new(0, -100, 0);
    private readonly Vector3 closedPosition = new(0, -1200, 0);

    private bool isPaused;
    //private bool pauseAnim;

    private void Awake()
    {
        // Assuming you have an action map named "Menu" and a Pause action within it.
        menuActionMap = actionAsset.FindActionMap("PauseMenu");
        pauseAction = menuActionMap.FindAction("Pause");

        pauseAction.performed += _ => ToggleMenu();
        UIBlocker.SetActive(false);
    }

    public void OpenMainMenu() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.Garage); }
    public void OpenStartMenu() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.StartMenu); }
    //public void OpenHub() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.Garage); }
    public void OpenVenueSelection() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.VenueSelection); }
    public void OpenBandManagement() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.BandManagement); }
    public void OpenShop() { ToggleMenu(); gameLoadHandler.SwitchToScene((int)SceneIndex.Shop); }

    

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
    }

    public IEnumerator PauseMenuAnimation(bool toOpen)
    {
        Vector3 endPosition = closedPosition;
        if (toOpen)
        {
            menuUI.transform.localPosition = closedPosition;
            endPosition = openPosition;
        }
        float counter = 0;
        //Get the current position of the object to be moved
        Vector3 startPosition = menuUI.transform.localPosition;

        while (counter < animationDuration)
        {
            counter += Time.unscaledDeltaTime;
            menuUI.transform.localPosition = Vector3.Lerp(startPosition, endPosition, counter / animationDuration);
            yield return null;
        }
    }

    public void DisablePauseAction()
    {
        pauseAction.Disable();
        pauseAction.Dispose();
    }

    public void PlayButtonDown()
    {
        buttonPress.Play();
    }

    public void PlayButtonUp()
    {
        buttonRelease.Play();
    }

    private void OnEnable()
    {
        pauseAction.Enable();
        menuActionMap.Enable();
    }

    private void OpenPauseMenu()
    {
        TimeEvents.GamePaused();
        StartCoroutine(PauseMenuAnimation(true));
    }

    private void ClosePauseMenu()
    {
        TimeEvents.GameResumed();
        //menuUI.SetActive(false);
        StartCoroutine(PauseMenuAnimation(false));
    }

    // TODO: THESE DON'T WORK. OKAY THEY DO NOW?!?!
    private void OnDisable()
    {
        pauseAction.performed -= _ => ToggleMenu();
        DisablePauseAction();
    }

    private void OnDestroy()
    {
        DisablePauseAction();
    }
}
