using cinemachine.actions.handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
/*
 * This class functions as a listener for cinemachine camera events. If you have a minigame that requires a camera swap, simply follow the steps below:
 * 
 * COMING SOON...
 * 
 * 
 */

public class CinemachineCameraController : MonoBehaviour
{
    // Using a Cinemachine Animator to switch between camera states
    [Header("Cinemachine Animator")]
    [SerializeField] Animator cinemachineAnimator;

    private void Start()
    {
        CinemachineGameEvents.instance.e_SwitchToBandCam.AddListener(SwitchToBandCamera);
        CinemachineGameEvents.instance.e_SwitchToTShirtCam.AddListener(SwitchToTShirtCannonCamera);
    }

    private void SwitchToBandCamera()
    {
        //cinemachineAnimator.Play("Default Concert View");
        cinemachineAnimator.SetTrigger("MinigameOver");
    }

    private void SwitchToTShirtCannonCamera()
    {
        //cinemachineAnimator.Play("T-Shirt Cannon Cam");
        cinemachineAnimator.SetTrigger("StartTCannon");
    }
}
