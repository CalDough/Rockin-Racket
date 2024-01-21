using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static ConcertAttendee;

public class ThoughtBubble : MonoBehaviour
{
    private Animator anim;
    [SerializeField]  private SpriteRenderer srItem;

    private RequestableItem currentThought;
    [SerializeField] private List<Sprite> requestableItemSprites;
    public bool IsOpenedBubble { get; private set; }

    private Coroutine TimerCoroutine;

    public void ShowItemThought(RequestableItem requestableItem)
    {
        currentThought = requestableItem;
        anim.Play("ThoughtBubbleExpand"); 
        srItem.sprite = requestableItemSprites[(int)requestableItem];
        IsOpenedBubble = true; 
        
    }

    public void HideItemThought(RequestableItem requestableItem)
    {
        if (currentThought == requestableItem)
        {
            anim.Play("ThoughtBubbleClose"); 
            IsOpenedBubble = false;
        }
    }

    public bool IsItemThoughtActive(RequestableItem requestableItem)
    {
        return currentThought == requestableItem;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        IsOpenedBubble = false; 
    }

    /*
    public void StartTimer(float duration)
    {
        TimerCoroutine = StartCoroutine(RadialTimerRoutine(duration));
    }

    public void StopTimer()
    {
        if(TimerCoroutine != null)
        {
            StopCoroutine(TimerCoroutine);
        }
    }
    */


    private void Update()
    {
       
    }
}