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

    [Header("Click Particle Effect")]
    [SerializeField] private GameObject particlePrefab; 
    [SerializeField] private RectTransform parentRect;

    [Header("Cannon Pressure Bar")]
    [SerializeField] int maxPressure;
    [SerializeField] private float minGoodPressure = 33;
    [SerializeField] private float maxGoodPressure = 66;
    //[SerializeField] int pressureIncrementMultiplier;
    private int pressureDirection = 1; 
    [Header("Slider Fill Color")]
    [SerializeField] private Image fillBarImage; 
    [SerializeField] private Color defaultColor = Color.red; 
    [SerializeField] private Color goodPressureColor = Color.green;

    [Header("Shot Counter")]
    [SerializeField] TMP_Text shotCounterText;
    
    [Header("Variables")]
    //Just gonna serialize these values so i can debug them later on
    [SerializeField] int cannonRange;
    [SerializeField] int maxNumShots;
    [SerializeField] int fameIncrement;
    [SerializeField] int fameDecrement;
    [SerializeField] int fameComboMultiplier;
    
    [SerializeField] private int remainingShots;
    [SerializeField] private float currentPressureValue = 0;
    [SerializeField] private float currentFame = 0;
    [SerializeField] private bool isEventOpen = false;

    /*
     * Activate runs when the event is spawn in, NOT when the event is opened by the player
     */
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

    /*
     * OpenEvent runs when the player opens the minigame through the UI
     */
    public override void OpenEvent()
    {
        base.OpenEvent();
        isEventOpen = true; // This variable is used to block the event's factors to trigger early
        StartCoroutine(CyclePressureBar());
        // Calling our Cinemachine game event to swap the camera using the Cinemachine animator
        CinemachineGameEvents.instance.e_SwitchToTShirtCam.Invoke();
        // Grabbing the current camera to use with Raycasting as a part of this minigame
        mainCamera = Camera.main;
    }

    /*
     * CloseEvent() runs when the event is closed by the player through the UI
     */
    public override void CloseEvent()
    {
        base.CloseEvent();
        isEventOpen = false;
        CinemachineGameEvents.instance.e_SwitchToBandCam.Invoke();
        StopCoroutine(CyclePressureBar());
    }

    /*
     * Complete is called when the player finishes the event before time is up
     */
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

    /*
     * EndEvent() runs when the minigame is forceably closed by the game or the event is over
     */
    public override void End()
    {
        isActiveEvent = false;
        if (durationCoroutine != null) {StopCoroutine(durationCoroutine);}
        GameEvents.EventFail(this);
        GameEvents.EventClosed(this);
        HandleClosing();
        StopCoroutine(CyclePressureBar());
    }

    /*
     * HandleClosing handles the fine details of closing the event, and is called by CloseEvent()
     */
    public override void HandleClosing()
    {
        Panels.SetActive(false);
        CinemachineGameEvents.instance.e_SwitchToBandCam.Invoke();
        if (IsCompleted == false)
        { RestartMiniGameLogic(); }
    }
    
    /*
     * This co-routine cycles the cannon bar and changes its color
     */
    IEnumerator CyclePressureBar()
    {
        float previousPressureValue = 0;

        while (isEventOpen)
        {
            //Debug.Log("Cycling pressure bar. Current pressure: " + currentPressureValue);
            currentPressureValue += pressureDirection;

            if (currentPressureValue >= maxPressure || currentPressureValue <= 0)
            {
                pressureDirection *= -1; 
            }

            cannonPressureSlider.value = Mathf.SmoothStep(previousPressureValue, currentPressureValue, 0.5f);
            previousPressureValue = cannonPressureSlider.value;

            if (GetPressureState() == PressureState.Good)
            {
                fillBarImage.color = goodPressureColor;
            }
            else
            {
                fillBarImage.color = defaultColor;
            }

            float dynamicWaitTime = CalculateDynamicWaitTime();
            yield return new WaitForSeconds(dynamicWaitTime);  
        }
    }
    // helper function to make the bar cycle faster/slower depending on its values
    float CalculateDynamicWaitTime()
    {
        float normalizedPressure = currentPressureValue / maxPressure;
        return Mathf.Clamp((normalizedPressure - 0.5f) * (normalizedPressure - 0.5f) + 0.01f, 0.01f, 0.05f);
    }

    private void Update()
    {
        if (isEventOpen && tShirtCursorImage)
        {
            Vector2 localCursor;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(playableArea, Input.mousePosition, null, out localCursor))
            {
                tShirtCursorImage.anchoredPosition = localCursor;
            }

            tShirtCursorImage.gameObject.SetActive(IsMouseInPlayableArea());
        }
    }

    private PressureState GetPressureState()
    {
        if (currentPressureValue < minGoodPressure)
        {
            return PressureState.Bad;
        }
        else if (currentPressureValue >= minGoodPressure && currentPressureValue <= maxGoodPressure)
        {
            return PressureState.Good;
        }
        else
        {
            return PressureState.Weak;
        }
    }

    /*
     * This method checks if the players mouse is within the firing zone so they dont waste their shot
     */
    private bool IsMouseInPlayableArea()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(playableArea, Input.mousePosition))
        {
            return true;
        }
        return false;
    }

    /*
     * This method takes player input when they click the mouse and determines what the 
     * player is hitting and at what pressure amount
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActiveEvent) 
        {return;}
        
        if (mainCamera == null)
        {mainCamera = Camera.main;}

        if (!IsMouseInPlayableArea())
        {return;} 

        //PlayParticlesAtPosition(eventData.position);

        PressureState currentPressureState = GetPressureState();

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

    /*
     * This method takes values from OnPointerDown and translates it into firing the t-shirt
     * into the audience
     */
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

    //Not sure why this doesn't work, disabled for now
    private void PlayParticlesAtPosition(Vector2 screenPosition)
    {
        if (particlePrefab && parentRect)
        {
            Vector2 localCursor;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenPosition, null, out localCursor))
            {
                GameObject particleInstance = Instantiate(particlePrefab, parentRect.transform);
                
                RectTransform rectTransform = particleInstance.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = localCursor;
                
                ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
                ps.Play();
                Destroy(particleInstance, ps.main.duration + ps.main.startLifetime.constantMax);
            }
        }
    }


}

