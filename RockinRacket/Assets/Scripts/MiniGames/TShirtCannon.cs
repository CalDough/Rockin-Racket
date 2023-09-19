using cinemachine.actions.handler;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/*
 * This is the class file for the T-Shirt Cannon minigame. You can find the design document here: https://docs.google.com/document/d/1x7XgZG1N7djPAnV18wTUN5dq9bY9pOokqpf27cqf6ww/edit
 * 
 * This minigame utilizes a separate camera from the main scene. You can find the related classes in CinemachineCameraController.cs and CinemachineGameEvents.cs
 * 
 * Additional classes that are connected with this minigame are: Shirt.cs, ....
 */

// When shirts run out game is over
// Talk to Hamilton about buying shirts to shoot at the concert

// No shooting through air, play animation when it hits surface or audience member

public class TShirtCannon : MiniGame, IPointerDownHandler
{
    // Public Variables
    [Header("Object References")]
    // May not longer be needed... requires further investigation
    [SerializeField] GameObject shirt;
    // Used for Debugging
    [SerializeField] Camera cameraMain;

    [Header("Cannon Pressure Bar")]
    [SerializeField] CannonBar cannonBar;
    [Tooltip("MUST be a value that is divisible by THREE")]
    [SerializeField] int maxPressure;
    [SerializeField] int pressureIncrementMulitplier;

    [Header("Shot Counter")]
    [SerializeField] TMP_Text shotCounterText;

    [Header("Variables")]
    [SerializeField] int cannonRange;
    [SerializeField] int maxNumShots;
    [SerializeField] int fameIncrementer;
    [SerializeField] int fameDecrementer;
    [SerializeField] int fameComboMultiplier;

    // Private Variables
    private int remainingShots;
    private float localPressureValue = 0;
    private float currentFame = 0;
    private Camera currentCamera;
    private bool isEventOpen = false;
    private bool cyclePressureBar = false;

    // Debug Variables
    bool[] successfulShots;

    /*
     * Activate runs when the event is spawn in, NOT when the event is opened by the player
     */
    public override void Activate() 
    {
        // Code from the Activate method in the Minigame.cs class
        isActiveEvent = true;
        remainingDuration = duration;
        if (!infiniteDuration)
        {
            remainingDuration = duration;
            durationCoroutine = StartCoroutine(EventDurationCountdown());
        }
        GameEvents.EventStart(this);
        
        // The following code is custom for this minigame...

        // We need to set the cannon bar's max pressure and value, but not start it until the player opens the event
        cannonBar.SetMaxValue(maxPressure);
        cannonBar.SetValue(0);

        // Setting the T-Shirt ammo counter and initializing the UI element
        remainingShots = 0;
        shotCounterText.text = "Remaining Shirts: " + (maxNumShots - remainingShots);

        // Setting the value for our debug array
        successfulShots = new bool[maxNumShots];
        
        // Debug Statement
        Debug.Log("<color=green>T-Shirt Minigame Activated and Spawned In</color>");
    }

    /*
     * OpenEvent runs when the player opens the minigame
     */
    public override void OpenEvent()
    {
        // The following code is from the parent Minigame.cs class
        GameEvents.EventOpened(this); 
        HandleOpening();

        // The following code is custom for this minigame...
        isEventOpen = true; // This variable is used to block the event's factors to trigger early

        // Calling our Cinemachine game event to swap the camera using the Cinemachine animator
        CinemachineGameEvents.instance.e_SwitchToTShirtCam.Invoke();

        // Setting the boolean that allows us to the cannon pressure bar
        cyclePressureBar = true;

        // Grabbing the current camera to use with Raycasting as a part of this minigame
        currentCamera = Camera.main;
        cameraMain = currentCamera;

        // Debug Statement
        Debug.Log("<color=aqua>Player has opened T-Shirt Minigame Event</color> | OpenEvent() has been run");
    }

    /*
     * CloseEvent() runs when the event is closed by the player
     */
    public override void CloseEvent()
    {
        // The following code is from the parent Minigame.cs class
        GameEvents.EventClosed(this); 
        HandleClosing();

        // The following code is custom for this minigame
        isEventOpen = false; // Setting our gatekeeper variable to false to prevent false runs
        // Switching the camera back to the band-facing one
        CinemachineGameEvents.instance.e_SwitchToBandCam.Invoke();
        // Stopping the pressure bar from cycling
        cyclePressureBar = false;
    }

    /*
     * EndEvent() runs when the minigame is forceably closed by the game or the event is over
     */
    public override void End()
    {
        base.End();

        // Switching the camera back to the band-facing one
        CinemachineGameEvents.instance.e_SwitchToBandCam.Invoke();

        // Stopping the pressure bar from cycling
        cyclePressureBar = false;
    }

    /*
     * HandleClosing handles the fine details of closing the event, and is called by CloseEvent()
     */
    public override void HandleClosing()
    {
        Panels.SetActive(false);

        // Swapping the camera back to the band-facing one
        CinemachineGameEvents.instance.e_SwitchToBandCam.Invoke();

        //If you want to reset the game if they did not complete it
        if (IsCompleted == false)
        { RestartMiniGameLogic(); }

        // Stopping the pressure bar from cycling
        cyclePressureBar = false;
    }

    private void Update()
    {
        // The cannon pressure bar is only cycled when this 'gatekeeping' boolean is triggered
        if (cyclePressureBar)
        {
            //CycleCannonBar();

            if (localPressureValue < maxPressure)
            {
                localPressureValue += (Time.deltaTime * pressureIncrementMulitplier);
            }
            else if (localPressureValue > 0)
            {
                localPressureValue -= (Time.deltaTime * pressureIncrementMulitplier);
            }
        }
    }

    /*
     * This method cycles the pressure bar back and forth
     * 
     * ... Possibly going to be depreciated
     */
    private void CycleCannonBar()
    {
        //while(IsCompleted == false)
        //{
        //    if (localPressureValue < maxPressure)
        //    {
        //        localPressureValue += pressureIncrement;
        //    }
        //    else if (localPressureValue > 0)
        //    {
        //        localPressureValue -= pressureIncrement;
        //    }
        //}
    }

    /*
     * This method takes player input when they click the mouse and determines what the 
     * player is hitting and at what pressure amount
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        // These to three statements are used to safeguard the raycasting from any NULL errors
        if (!isActiveEvent) {
            Debug.Log("<color=red>Event not active</color>");
            return;
        }

        //if (isEventOpen) {
        //    Debug.Log("<color=red>Event not open</color>");
        //    return; 
        //}

        if (currentCamera == null)
        {
            Debug.Log("<color=red>Current Camera is NULL</color>");
            currentCamera = Camera.main;
            cameraMain = currentCamera;
        }

        // This block of code evaluates the current pressure level of the cannon bar
        // Right now the pressure value CAN ONLY BE a value divisible by THREE
        string currentPressure = "";

        if (localPressureValue < maxPressure / 3)
        {
            Debug.Log("Red");
            currentPressure = "Bad";
        }
        else if (localPressureValue < (maxPressure / 3) * 2)
        {
            Debug.Log("Yellow");
            currentPressure = "Weak";
        }
        else if (localPressureValue < maxPressure)
        {
            Debug.Log("Green - T-Shirt fires successfully");
            currentPressure = "Good";
        }

        

        // Cast a ray from the camera's position through the mouse's position on the screen
        Ray ray = currentCamera.ScreenPointToRay(eventData.position);
        RaycastHit hit; 

        // TODO - Fix raycast bug
        
        if (Physics.Raycast(ray, out hit, cannonRange))
        {
            GameObject target = hit.transform.gameObject;

            // Debug Statement
            Debug.Log("<color=orange>Raycast is fired</color>");
            Debug.DrawRay(cameraMain.transform.position, cameraMain.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 1000);
            Debug.Log("Did Hit" + hit.transform.gameObject.tag);

            // TODO - Play a sound when the shirt is fired

            // Detecting what the player hits
            if (target.CompareTag("Audience")) 
            {
                Debug.Log("<color=green>Shot lands successfully</green>");
                FireShirt(true, currentPressure, target);
            }
            else
            {
                Debug.Log("<color=orange>Shot misses...</color>");
                FireShirt(false, currentPressure, target);
            }
        }
    }

    /*
     * This method takes values from OnPointerDown and translates it into firing the t-shirt
     * into the audience
     */
    private void FireShirt(bool hitsAudience, string pressure, GameObject target)
    {
        // Setting the value for our debug array and then incrementing the remainingShots variable
        //successfulShots[remainingShots] = hitsAudience;
        remainingShots++;
        shotCounterText.text = "Remaining Shirts: " + (maxNumShots - remainingShots);
        // If the player is out, the game ends and we send the appropriate variables back to the game manager
        if (remainingShots <= 0)
        {
            Debug.Log("Minigame ended with fame value of: " + currentFame);
            Debug.Log("Successful shot amount: " + remainingShots.ToString());

            // TODO - Send values to game manager script once fame implementation is finalized

            End();
        }

        // Triggering our method in the Audience class
        if (hitsAudience)
        {
            target.GetComponent<Audience>().PlayTCMReaction(pressure);
        }

        if (hitsAudience && pressure.Equals("Good"))
        {
            /*
             * TODO - Get audience class from target to trigger T-Shirt animation
             */
            //currentFame += fameIncrementer * fameComboMultiplier;
        }
        else if (hitsAudience && pressure.Equals("Weak"))
        {
            // TODO - Call audience class and play animation once created
            //currentFame -= fameDecrementer * fameComboMultiplier;
        }
        else if (hitsAudience && pressure.Equals("Bad"))
        {
            // TODO - Call audience class and play animation once created
            //currentFame -= fameDecrementer * fameComboMultiplier;

        }
        else
        {
            // This case handles when the player misses the audience and hits something else

            // TODO - Implement T-Shirt missed animation when it is created

            currentFame += fameDecrementer * fameComboMultiplier;

        }
    }
}
