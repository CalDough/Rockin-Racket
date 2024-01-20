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
    private bool isInitialized = false;
    private bool hasBeenServed = false;

    /*
     * This method is called by the Awake() method in the parent abstract class to ensure that the abstract class was intialized before the MTCustomer class
     */
    public override void Init()
    {
        isInitialized = true;
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
        MerchTableEvents.instance.e_customerHasArrived.Invoke();

    }
}
