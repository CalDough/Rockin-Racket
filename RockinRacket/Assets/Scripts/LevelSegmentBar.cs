using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSegmentBar : MonoBehaviour
{
    // 0 - first song, 1 - second song, 2 - intermission, 3 - third song, 4 - fourth song 
    public int SegmentIndex = 0;
    public List<Slider> sliders;

    void Start()
    {
        ConcertEvents.instance.e_SongEnded.AddListener(HandleSongEnd);
        ConcertEvents.instance.e_ConcertStarted.AddListener(HandleConcertStarted);

    }

    void HandleConcertStarted()
    {
        if(GameManager.Instance.currentConcertData.isPostIntermission)
        {
            IncrementSegmentIndex(3); // Increment by 3 for post intermission
        }
    }

    void HandleSongEnd()
    {
        IncrementSegmentIndex(1); // Increment by 1 when a song ends
    }

    void Update()
    {
        UpdateCurrentSliderFill();
    }

    void UpdateCurrentSliderFill()
    {
        if(!ConcertController.instance){return;}

        if(SegmentIndex < sliders.Count)
        {
            float currentSongLength = ConcertController.instance.currentSongLength;
            float songTimer = ConcertController.instance.songTimer;

            if(currentSongLength > 0)
            {
                sliders[SegmentIndex].value = songTimer / currentSongLength;
            }

            if(songTimer >= currentSongLength)
            {
                sliders[SegmentIndex].value = 1;
            }
        }
    }

    public void IncrementSegmentIndex(int increment)
    {
        int newSegmentIndex = SegmentIndex + increment;
        if(newSegmentIndex > sliders.Count)
        {
            newSegmentIndex = sliders.Count;
        }
        for (int i = 0; i < newSegmentIndex; i++)
        {
            sliders[i].value = 1;
        }
        SegmentIndex = newSegmentIndex;
    }
}