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
 * NOTE: Here is the updated design document: https://docs.google.com/document/d/1BIFCALaLvl-oxMP8eftEsNlz9snuA1UFpEYQF-54G1c/edit?usp=sharing
 * 
 * This minigame utilizes a separate camera from the main scene. You can find the related classes in CinemachineCameraController.cs and CinemachineGameEvents.cs
 * 
 * Additional classes that are connected with this minigame are: Shirt.cs, ....
 */

public class TShirtCannon : MiniGame, IPointerDownHandler
{
    // Enums for pressure states
    public enum PressureState { Bad = 0, Weak = 1, Good = 2 }

    [Header("Object References")]
    //[SerializeField] GameObject shirt;
    [SerializeField] Camera mainCamera;
    [SerializeField] private Slider cannonPressureSlider; 

    [Header("Playable Area")]
    [SerializeField] RectTransform playableArea;
    [SerializeField] private RectTransform tShirtCursorImage;

    [Header("Cannon Pressure Bar")]
    [SerializeField] int maxPressure;
    //[SerializeField] int pressureIncrementMultiplier;
    private int pressureDirection = 1; 

    [Header("Shot Counter")]
    [SerializeField] TMP_Text shotCounterText;
    
    [Header("Variables")]
    [SerializeField] int cannonRange;
    [SerializeField] int maxNumShots;
    [SerializeField] int fameIncrement;
    [SerializeField] int fameDecrement;
    [SerializeField] int fameComboMultiplier;
    
    [SerializeField] private int remainingShots;
    [SerializeField] private float currentPressureValue = 0;
    [SerializeField] private float currentFame = 0;
    [SerializeField] private bool isEventOpen = false;
    //[SerializeField] private bool isPressureIncreasing = true;

    public override void Activate()
    {
        base.Activate();

        cannonPressureSlider.maxValue = maxPressure;
        cannonPressureSlider.value = 0;
        remainingShots = maxNumShots;
        shotCounterText.text = "Remaining Shirts: " + remainingShots;
        
        Debug.Log("<color=green>T-Shirt Minigame Activated</color>");
        this.isActiveEvent = true;
        StartCoroutine(CyclePressureBar());
    }

    public override void OpenEvent()
    {
        base.OpenEvent();
        StartCoroutine(CyclePressureBar());
        isEventOpen = true;
        CinemachineGameEvents.instance.e_SwitchToTShirtCam.Invoke();
        mainCamera = Camera.main;
    }

    public override void CloseEvent()
    {
        base.CloseEvent();
        isEventOpen = false;
        CinemachineGameEvents.instance.e_SwitchToBandCam.Invoke();
        StopCoroutine(CyclePressureBar());
    }

    public override void Complete()
    {
        isActiveEvent = false;
        if (durationCoroutine != null){StopCoroutine(durationCoroutine);}
        GameEvents.EventComplete(this);
        GameEvents.EventClosed(this);
        this.IsCompleted = true;
        HandleClosing();
        StopCoroutine(CyclePressureBar());
    }

    public override void End()
    {
        isActiveEvent = false;
        if (durationCoroutine != null) {StopCoroutine(durationCoroutine);}
        GameEvents.EventFail(this);
        GameEvents.EventClosed(this);
        HandleClosing();
        StopCoroutine(CyclePressureBar());
    }

    public override void HandleClosing()
    {
        Panels.SetActive(false);
        CinemachineGameEvents.instance.e_SwitchToBandCam.Invoke();
        if (IsCompleted == false)
        { RestartMiniGameLogic(); }
    }

    IEnumerator CyclePressureBar()
    {
        while (isEventOpen && this.isActiveEvent)
        {
            currentPressureValue += pressureDirection;

            if (currentPressureValue >= maxPressure || currentPressureValue <= 0)
            {
                pressureDirection *= -1; 
            }

            cannonPressureSlider.value = currentPressureValue;

            yield return new WaitForSeconds(0.1f);  
        }
    }

    private void Update()
    {
        if (isEventOpen)
        {
            tShirtCursorImage.anchoredPosition = Input.mousePosition;
            tShirtCursorImage.gameObject.SetActive(IsMouseInPlayableArea());
        }
    }


    private bool IsMouseInPlayableArea()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(playableArea, Input.mousePosition))
        {
            return true;
        }
        return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActiveEvent) 
        {return;}
        
        if (mainCamera == null)
        {mainCamera = Camera.main;}

        if (!IsMouseInPlayableArea())
        {return;} 

        PressureState currentPressureState;

        if (currentPressureValue < maxPressure / 3)
        {currentPressureState = PressureState.Bad;} 

        else if (currentPressureValue < 2 * maxPressure / 3) 
        {currentPressureState = PressureState.Weak;}

        else 
        {currentPressureState = PressureState.Good;}

        Ray ray = mainCamera.ScreenPointToRay(eventData.position);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, cannonRange))
        {
            GameObject target = hit.transform.gameObject;

            if (target.CompareTag("Audience"))
            {
                FireShirt(true, currentPressureState, target);
            }
            else
            {
                FireShirt(false, currentPressureState, target);
            }
        }
    }

    private void FireShirt(bool hitsAudience, PressureState pressureState, GameObject target)
    {
        remainingShots--;
        shotCounterText.text = "Remaining Shirts: " + remainingShots;

        if (remainingShots <= 0)
        {
            Complete();
        }

        if (hitsAudience)
        {
            target.GetComponentInParent<AudienceMember>().PlayTCMReaction(pressureState);

            if (pressureState == PressureState.Good)
            {
                currentFame += fameIncrement * fameComboMultiplier;
            }
            else
            {
                currentFame -= fameDecrement * fameComboMultiplier;
            }
        }
        else
        {
            currentFame -= fameDecrement * fameComboMultiplier;
        }
    }
}

