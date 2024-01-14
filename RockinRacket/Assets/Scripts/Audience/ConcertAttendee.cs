using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcertAttendee : Attendee
{
    public enum MoodState { Hyped, Frustrated, Pleased }

    [Header("Lerp Variables")]
    //public float duration;
    [SerializeField] private Vector3 lerpStart;
    [SerializeField] private Vector3 lerpEnd;

    [Header("Mood Variables")]
    //public int currentMoodRating;
    public MoodState currentMood = MoodState.Pleased;

    [Header("Appearance Variables")]
    //public Sprite[] appearanceVariations;
    private SpriteRenderer sr;
    private Animator anim;
    public ParticleSystem goodParticles;
    public ParticleSystem badParticles; 

    private Rigidbody2D rb;






    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public override void TriggerAttendeeHappyEffect()
    {
        
    }

    public override void TriggerAttendeeAngryEffect( )
    {

    }

    public override void RandomizeAppearance()
    {
        sr.sprite = appearanceVariations[Random.Range(0, appearanceVariations.Length)];
    }

    /*
    public void StartLerp(Vector3 start, Vector3 end)
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
            transform.position = Vector3.Lerp(lerpStart, lerpEnd, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = lerpEnd;
    }
    */
}
