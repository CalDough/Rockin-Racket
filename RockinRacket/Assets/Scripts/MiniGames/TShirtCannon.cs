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

public class TShirtCannon : MiniGame, IPointerDownHandler
{
    // Public Variables
    [Header("Object References")]
    [SerializeField] Sprite cannon;
    [SerializeField] GameObject shirt;

    [Header("Cannon Pressure Bar")]
    [SerializeField] CannonBar cannonBar;
    [SerializeField] int maxPressure;
    [SerializeField] int pressureIncrement;

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
    private bool fireShirt = false;
    private Camera currentCamera;
    private Transform start;
    private Transform end;

    // Debug Variables
    bool[] successfulShots;

    public override void Activate() 
    {
        isActiveEvent = true;
        remainingDuration = duration;
        if (!infiniteDuration)
        {
            remainingDuration = duration;
            durationCoroutine = StartCoroutine(EventDurationCountdown());
        }
        GameEvents.EventStart(this);

        // Setting our variables for the cannon pressure bar
        cannonBar.SetMaxValue(maxPressure);
        cannonBar.SetValue(0);
        CycleCannonBar();

        // Setting the number of remaining t-shirts left to be shot and our text object
        remainingShots = 0;
        shotCounterText.text = "Remaining Shirts: " + (maxNumShots - remainingShots);

        // Setting the value for our debug array
        successfulShots = new bool[maxNumShots];

        Debug.Log("Activated");
    }

    public override void OpenEvent()
    {
        base.OpenEvent();
        Debug.Log("Event Opened");
        // Calling the Cinemachine camera switcher
        CinemachineGameEvents.instance.e_SwitchToTShirtCam.Invoke();

        // Grabbing the current camera for object reference
        currentCamera = Camera.current.GetComponent<Camera>();
        Transform start = currentCamera.transform;
    }

    // End/Complete
    public override void CloseEvent()
    {
        base.CloseEvent();
        CinemachineGameEvents.instance.e_SwitchToBandCam.Invoke();
    }

    // Miss
    public override void End()
    {
        base.End();
        CinemachineGameEvents.instance.e_SwitchToBandCam.Invoke();
    }

    private void Update()
    {
        // This method is called only when a shirt firing animation needs played
        if (fireShirt)
        {
            fireShirt = false;
            FireShirtAnimationHelper();
        }
    }

    /*
     * This method cycles the pressure bar back and forth
     */
    private void CycleCannonBar()
    {
        while(IsCompleted == false)
        {
            if (localPressureValue < maxPressure)
            {
                localPressureValue += pressureIncrement;
            }
            else if (localPressureValue > 0)
            {
                localPressureValue -= pressureIncrement;
            }
        }
    }

    /*
     * This method takes player input when they click the mouse and determines where the t-shirt
     * lands and at what pressure amount it was fired at
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        string currentPressure = "";

        // First we need to obtain the pressure at the time that the player clicked
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

        // Then we need to determine what the player is actually shooting at...
        // To detect if the t-shirt will find an audience memeber, we are using a raycast to find the audience by tag
        RaycastHit hit;
        if (Physics.Raycast(currentCamera.transform.position, currentCamera.transform.forward, out hit, cannonRange))
        {
            GameObject target = hit.transform.gameObject;

            if (target.tag.Equals("Audience"))
            {
                Debug.Log("Shot lands successfully");
                FireShirt(true, currentPressure, target);
            }
            else
            {
                Debug.Log("Shot misses...");
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
        // Setting off the animation and assigning a value to our target
        end = target.transform;
        fireShirt = true;

        // Setting the value for our debug array and then incrementing the remainingShots variable
        successfulShots[remainingShots] = hitsAudience;
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

        if (hitsAudience && pressure.Equals("Good"))
        {
            currentFame += fameIncrementer * fameComboMultiplier;
        }
        else
        {
            // Negative player feedback here?
        }
    }

    /*
     * This method shoots a shirt prefab in an arc from the cannon to the object the player shot at
     */
    private void FireShirtAnimationHelper()
    {
        // Declaring variables for our shot calculations
        float startTime = Time.time;
        float totalMovementTime = 1.0f;

        // Calculations for shooting our t-shirt from the cannon to whatever the target
        Vector3 arcCenter = (start.position + end.position) * .5F;
        arcCenter -= new Vector3(0, 1, 0);

        Vector3 startRelativeCenter = start.position - arcCenter;
        Vector3 endRelativeCenter = end.position - arcCenter;

        float fractionComplete = (Time.time - startTime) / totalMovementTime;

        GameObject shirtInstance = Instantiate(shirt, start.position, Quaternion.identity);
        shirtInstance.transform.position = Vector3.Slerp(startRelativeCenter, endRelativeCenter, fractionComplete);
        transform.position += arcCenter;
    }
}
