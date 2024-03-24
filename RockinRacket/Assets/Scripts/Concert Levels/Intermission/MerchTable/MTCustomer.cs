using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This class is the child of the following parent: Attendee
 * 
 * This class is the associated logic container for the Merch Table Customers
 */

public class MTCustomer : Attendee
{
    //private bool isInitialized = false;
    private bool markForDestruction = false;

    public Sprite[] altAppearanceVariations;

    /*
     * This method is called by the Awake() method in the parent abstract class to ensure that the abstract class was intialized before the MTCustomer class
     */
    public override void Init()
    {
        //isInitialized = true;
    }

    private void Start()
    {
        RandomizeAppearanceAlt();
    }

    /*
     * TODO
     */
    public override void TriggerAttendeeAngryEffect()
    {
        throw new System.NotImplementedException();
    }

    /*
     * TODO
     */
    public override void TriggerAttendeeHappyEffect()
    {
        throw new System.NotImplementedException();
    }

    /*
     * This method is called after the attendee finishes their current lerp cycle
     */
    protected override void EndLerp()
    {
        if (markForDestruction)
        {
            Destroy(gameObject);
        }
        else
        {
            MerchTableEvents.instance.e_customerHasArrived.Invoke();
        }

    }

    /*
     * The following method marks the customer for destruction after they go offscreen
     */
    public void MarkCustomerForDestruction()
    {
        markForDestruction = true;
    }

    public void RandomizeAppearanceAlt()
    {
        sr.sprite = altAppearanceVariations[Random.Range(0, altAppearanceVariations.Length)];
    }
}
