using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTCustomer : Attendee
{
    private SpriteRenderer sr;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    public override void RandomizeAppearance()
    {
        sr.sprite = appearanceVariations[Random.Range(0, appearanceVariations.Length)];
    }

    public override void TriggerAttendeeAngryEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void TriggerAttendeeHappyEffect()
    {
        throw new System.NotImplementedException();
    }
}
