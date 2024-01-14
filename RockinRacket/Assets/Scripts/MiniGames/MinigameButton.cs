using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameButton : MonoBehaviour
{
    public MinigameController minigameController;
    public Image radialTimerImage;
    public GameObject button;

    void Update()
    {
        if (minigameController && minigameController.CanActivate && radialTimerImage.gameObject.activeSelf)
        {
            //Debug.Log("Updating Radial Timer: " + minigameController.availabilityRemainingDuration + " / " + minigameController.availabilityTimerDuration);

            UpdateRadialTimer(minigameController.availabilityRemainingDuration / minigameController.availabilityTimerDuration);
            
        }
    }

    private void ResetValuesToDefault()
    {
        button.SetActive(false);         
        radialTimerImage.fillAmount = 0;
        radialTimerImage.transform.parent.gameObject.SetActive(false);
    }

    private void UpdateRadialTimer(float progress)
    {
        radialTimerImage.fillAmount = progress; 
    }

    public void OnGameButtonClick()
    {
        if(minigameController)
        {
            minigameController.StartMinigame();
            minigameController.OpenMinigame();
        }
    }

    private void OnMinigameAvailable(object sender, GameEventArgs e)
    {
        //Debug.Log("Minigame Avaiable For Button");
        if(e.EventObject == this.minigameController)
        {
            //Debug.Log("Minigame matches this Button");
            button.SetActive(true);         
            radialTimerImage.fillAmount = 0;
            radialTimerImage.transform.parent.gameObject.SetActive(true);
            
        }
    }

    private void OnMinigameClosed(object sender, GameEventArgs e)
    {
        if(e.EventObject == this.minigameController)
        {
            ResetValuesToDefault();
        }
    }

    private void OnEnable()
    {
        MinigameEvents.OnMinigameAvailable += OnMinigameAvailable;
        MinigameEvents.OnMinigameCancel += OnMinigameClosed;
        MinigameEvents.OnMinigameFail += OnMinigameClosed;
        MinigameEvents.OnMinigameComplete += OnMinigameClosed;
    }

    private void OnDisable()
    {
        MinigameEvents.OnMinigameAvailable -= OnMinigameAvailable;
        MinigameEvents.OnMinigameCancel -= OnMinigameClosed;
        MinigameEvents.OnMinigameFail -= OnMinigameClosed;
        MinigameEvents.OnMinigameComplete -= OnMinigameClosed;
    }
}
