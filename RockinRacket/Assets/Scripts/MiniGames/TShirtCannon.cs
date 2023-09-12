using cinemachine.actions.handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This is the class file for the T-Shirt Cannon minigame. You can find the design document here: https://docs.google.com/document/d/1x7XgZG1N7djPAnV18wTUN5dq9bY9pOokqpf27cqf6ww/edit
 * 
 * This minigame utilizes a separate camera from the main scene. You can find the related classes in CinemachineCameraController.cs and CinemachineGameEvents.cs
 * 
 * Additional classes that are connected with this minigame are: Shirt.cs, ....
 */

public class TShirtCannon : MiniGame
{
    [Header("Object References")]
    [SerializeField] Sprite cannon;
    [SerializeField] GameObject shirt;

    [Header("Cannon Pressure Bar")]
    [SerializeField] CannonBar cannonBar;
    [SerializeField] int maxPressure;
    [SerializeField] int pressureIncrement;

    float localPressureValue = 0;


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
    }

    public override void OpenEvent()
    {
        base.OpenEvent();

        // Calling the Cinemachine camera switcher
        CinemachineGameEvents.instance.e_SwitchToTShirtCam.Invoke();
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
        // Yes, this is the old input system... it will be changed later
        //PlayerClick();
    }

    // This method cycles the pressure bar back and forth
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

    private void PlayerClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Red Range
            if (localPressureValue <  maxPressure / 3)
            {
                Debug.Log("Red");
                FireShirt(500);
            }
            else if (localPressureValue < (maxPressure / 3) * 2)
            {
                Debug.Log("Yellow");
                FireShirt(5);
            }
            else if (localPressureValue < maxPressure)
            {
                Debug.Log("Green - T-Shirt fires successfully");
                FireShirt(100);
            }
        }
    }

    private void FireShirt(int pressure)
    {
        Vector3 pos = Input.mousePosition;
        pos.z = Camera.main.nearClipPlane;
        GameObject shirtInstance = Instantiate(shirt, pos, Quaternion.identity);
        shirtInstance.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, pressure));
    }




}
