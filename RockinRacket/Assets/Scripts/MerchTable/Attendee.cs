using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* 
 * This abstract class is used as the base template for audience members that are attending the concert and audience members who are purchasing items at the Merch Stand during intermission
 * 
 * List of classes that inherit from this class:
 *      - MTCustomer
 *      - ...Probably Concert Attendee
 * 
 */

public abstract class Attendee : MonoBehaviour
{
    [Header("Lerp Variables")]
    public float duration;
    [SerializeField] protected Transform lerpStart;
    [SerializeField] protected Transform lerpEnd;

    [Header("Mood Variables")]
    public int currentMoodRating;

    [Header("Appearance Variables")]
    public Sprite[] appearanceVariations;
    protected SpriteRenderer sr;

    protected void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        //appearanceVariations = new Sprite[3];

        Init();
    }

    /*
     * This method ensures the order of execution remains consistent between parent and child
     * 
     * This method must be implemented in a subclass
     */
    public abstract void Init();

    /*
     * This method triggers a positive reaction from the attendee
     * 
     * This method must be implemented in a subclass
     */
    public abstract void TriggerAttendeeHappyEffect();

    /*
     * This method triggers a negative reaction from the customer
     * 
     * This method must be implemented in a subclass
     */
    public abstract void TriggerAttendeeAngryEffect();

    /*
     * This method randomizes the attendee's appearance when called
     * 
     * This method can be overidden in a subclass
     */
    public virtual void RandomizeAppearance()
    {
        sr.sprite = appearanceVariations[Random.Range(0, appearanceVariations.Length)];
    }

    /*
     * This method, and its associated coroutine, lerps the attendee from one Vector position to another
     * 
     * This method cannot be overriden in a subclass
     */
    public void StartLerp(Transform start, Transform end)
    {
        lerpStart = start;
        lerpEnd = end;
        StartCoroutine(LerpAttendee());
    }

    protected IEnumerator LerpAttendee()
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(lerpStart.position, lerpEnd.position, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = lerpEnd.position;

        EndLerp();
    }

    /*
     * This method handles any actions you want the attendee to perform at the end of their lerp
     * 
     * This method must be implemented in a subclass
     */
    protected abstract void EndLerp();

}
