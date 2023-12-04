using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAudio : MonoBehaviour
{
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shopBell;
    [SerializeField] private AudioClip pageTurn;
    [SerializeField] private AudioClip checkoutBell;

    private void Start()
    {
        audioSource.clip = shopBell;
        audioSource.Play();
    }

    public void Play() { audioSource.Play(); }
    public void PlayEnterShop() { audioSource.PlayOneShot(shopBell); }
    public void PlayExitShop() { audioSource.PlayOneShot(shopBell); }
    // called by catalog manager when bookmark pressed
    public void PlayFlipPage() { print("PlayFlipPage"); audioSource.PlayOneShot(pageTurn); }
    // called by receipt button
    public void PlayCheckout() { print("PlayCheckout"); audioSource.PlayOneShot(checkoutBell); }
}
