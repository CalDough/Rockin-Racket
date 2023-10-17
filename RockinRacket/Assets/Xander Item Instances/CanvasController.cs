using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Public variables
    [SerializeField] GameStateData currentGameState;

    // Private Variables
    private GameObject BandViewPanel;
    private GameObject ShopViewPanel;
    private GameObject BackStageViewPanel;
    private GameObject AudienceViewPanel;
    private GameObject VenueViewPanel;
    private static List<GameObject> panels = new List<GameObject>();
    private ConcertState previousState;


    // Start is called before the first frame update
    void Start()
    {
        // Referencing the child panel objects associated with each view
        BandViewPanel = gameObject.transform.GetChild(1).gameObject;
        ShopViewPanel = gameObject.transform.GetChild(2).gameObject;
        BackStageViewPanel = gameObject.transform.GetChild(3).gameObject;
        AudienceViewPanel = gameObject.transform.GetChild(4).gameObject;
        VenueViewPanel = gameObject.transform.GetChild(5).gameObject;

        panels.Add(BandViewPanel);
        panels.Add(ShopViewPanel);
        panels.Add(BackStageViewPanel);
        panels.Add(AudienceViewPanel);
        panels.Add(VenueViewPanel);
    }

    private void Update()
    {
        ManageScenePanels();
    }

    private void ManageScenePanels()
    { 

        if (currentGameState.CurrentConcertState == ConcertState.BandView)
        {
            if (BandViewPanel.activeSelf)
            {
                return;
            }
            else
            {
                BandViewPanel.SetActive(true);
            }
        }
        else if (currentGameState.CurrentConcertState == ConcertState.ShopView)
        {
            if (ShopViewPanel.activeSelf)
            {
                return;
            }
            else
            {
                ShopViewPanel.SetActive(true);
            }
        }
        else if (currentGameState.CurrentConcertState == ConcertState.BackstageView)
        {
            if (BackStageViewPanel.activeSelf)
            {
                return;
            }
            else
            {
                BackStageViewPanel.SetActive(true);
            }
        }
        else if (currentGameState.CurrentConcertState == ConcertState.AudienceView)
        {
            if (AudienceViewPanel.activeSelf)
            {
                return;
            }
            else
            {
                AudienceViewPanel.SetActive(true);
            }
        }
        else if (currentGameState.CurrentConcertState == ConcertState.VenueView)
        {
            if (VenueViewPanel.activeSelf)
            {
                return;
            }
            else
            {
                VenueViewPanel.SetActive(true);
            }
        }
    }

    public void SwapToBandView()
    {
        previousState = currentGameState.CurrentConcertState;
        DeactivateCurrentUI(previousState);
        CameraSwapEvents.instance.e_SwapToBandView.Invoke();
        currentGameState.CurrentConcertState = ConcertState.BandView;
    }

    public void SwapToShopView()
    {
        previousState = currentGameState.CurrentConcertState;
        DeactivateCurrentUI(previousState);
        CameraSwapEvents.instance.e_SwapToShopView.Invoke();
        currentGameState.CurrentConcertState = ConcertState.ShopView;
    }

    public void SwapToBackstageView()
    {
        previousState = currentGameState.CurrentConcertState;
        DeactivateCurrentUI(previousState);
        CameraSwapEvents.instance.e_SwapToBackstageView.Invoke();
        currentGameState.CurrentConcertState = ConcertState.BackstageView;
    }

    public void SwapToAudienceView()
    {
        Camera.main.orthographic = false;
        previousState = currentGameState.CurrentConcertState;
        DeactivateCurrentUI(previousState);
        CameraSwapEvents.instance.e_SwapToAudienceView.Invoke();
        currentGameState.CurrentConcertState = ConcertState.AudienceView;
    }

    public void SwapToVenueView()
    {
        previousState = currentGameState.CurrentConcertState;
        DeactivateCurrentUI(previousState);
        CameraSwapEvents.instance.e_SwapToVenueView.Invoke();
        currentGameState.CurrentConcertState = ConcertState.VenueView;
    }

    private void DeactivateCurrentUI(ConcertState state)
    {
        switch (state)
        {
            case ConcertState.BandView:
                BandViewPanel.SetActive(false);
                break;
            case ConcertState.ShopView:
                ShopViewPanel.SetActive(false);
                break;
            case ConcertState.BackstageView:
                BackStageViewPanel.SetActive(false);
                break;
            case ConcertState.AudienceView:
                Camera.main.orthographic = true;
                AudienceViewPanel.SetActive(false);
                break;
            case ConcertState.VenueView:
                VenueViewPanel.SetActive(false);
                break;
            default: 
                break;
        }
    }
}
