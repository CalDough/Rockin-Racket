using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ShopAudio : MonoBehaviour
{
    public StudioEventEmitter checkout;
    public StudioEventEmitter shopEnter;
    public StudioEventEmitter pageFlip;

    //private void Start()
    //{
    //    audioSource.clip = shopBell;
    //    audioSource.Play();
    //}

    //public void Play() { audioSource.Play(); }
    //public void PlayEnterShop() { audioSource.PlayOneShot(shopBell); }
    //public void PlayExitShop() { audioSource.PlayOneShot(shopBell); }
    //// called by catalog manager when bookmark pressed
    //public void PlayFlipPage() { audioSource.PlayOneShot(pageTurn); }
    // called by receipt button
    public void PlayCheckout() { checkout.Play(); }
    public void PlayShopEnter() { shopEnter.Play(); }
    public void PlayPageFlip() { pageFlip.Play(); }
}
