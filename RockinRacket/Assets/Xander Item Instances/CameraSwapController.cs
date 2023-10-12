using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapController : MonoBehaviour
{
    [Header("Cinemachine Animator")]
    [SerializeField] Animator cinemachineAnimator;

    private void Start()
    {
        // BandView, ShopView, BackstageView, AudienceView, VenueView
        CameraSwapEvents.instance.e_SwapToBandView.AddListener(SwapToBandView);
        CameraSwapEvents.instance.e_SwapToShopView.AddListener(SwapToShopView);
        CameraSwapEvents.instance.e_SwapToBackstageView.AddListener(SwapToBackstageView);
        CameraSwapEvents.instance.e_SwapToAudienceView.AddListener(SwapToAudienceView);
        CameraSwapEvents.instance.e_SwapToVenueView.AddListener(SwapToVenueView);
    }

    private void SwapToBandView()
    {
        Debug.Log("<color=orange>Swap To Band View</color>");

        cinemachineAnimator.Play("BandView");
    }

    private void SwapToShopView()
    {
        Debug.Log("<color=orange>Swap To Shop View</color>");

        cinemachineAnimator.Play("ShopView");
    }

    private void SwapToBackstageView()
    {
        Debug.Log("<color=orange>Swap To Backstage View</color>");

        cinemachineAnimator.Play("BackstageView");
    }

    private void SwapToAudienceView()
    {
        Debug.Log("<color=orange>Swap To Audience View</color>");

        cinemachineAnimator.Play("AudienceView");
    }

    private void SwapToVenueView()
    {
        Debug.Log("<color=orange>Swap To Venue View</color>");

        cinemachineAnimator.Play("VenueView");
    }
}
