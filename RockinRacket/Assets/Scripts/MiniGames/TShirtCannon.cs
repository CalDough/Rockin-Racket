using cinemachine.actions.handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is the class file for the T-Shirt Cannon minigame. You can find the design document here: https://docs.google.com/document/d/1x7XgZG1N7djPAnV18wTUN5dq9bY9pOokqpf27cqf6ww/edit
 * 
 * This minigame utilizes a separate camera from the main scene. You can find the related classes in CinemachineCameraController.cs and CinemachineGameEvents.cs
 * 
 * Additional classes that are connected with this minigame are: Shirt.cs, ....
 */

public class TShirtCannon : MiniGame
{

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

        // Calling the Cinemachine camera switcher
        CinemachineGameEvents.instance.e_SwitchToTShirtCam.Invoke();
    }


}
