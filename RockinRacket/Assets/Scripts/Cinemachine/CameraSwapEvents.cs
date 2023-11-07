using cinemachine.actions.handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraSwapEvents : MonoBehaviour
{
    public static CameraSwapEvents instance;

    public UnityEvent e_SwapToBandView;
    public UnityEvent e_SwapToShopView;
    public UnityEvent e_SwapToBackstageView;
    public UnityEvent e_SwapToAudienceView;
    public UnityEvent e_SwapToVenueView;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (e_SwapToBandView != null)
        {
            e_SwapToBandView = new UnityEvent();
        }

        if (e_SwapToShopView != null)
        {
            e_SwapToShopView = new UnityEvent();
        }

        if (e_SwapToBackstageView != null)
        {
            e_SwapToBackstageView = new UnityEvent();
        }

        if (e_SwapToAudienceView != null)
        {
            e_SwapToAudienceView = new UnityEvent();
        }

        if (e_SwapToVenueView != null)
        {
            e_SwapToVenueView = new UnityEvent();
        }
    }
}
