using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    public Image starImage;
    public float fadeDuration = 0.5f; 

    private void Start()
    {
        SetStarColor(Color.black);
    }

    public void HighlightStars()
    {
        SetStarColor(Color.white);
    }
    
    public void HideStars()
    {
        SetStarColor(Color.black);
    }

    private void SetStarColor(Color color)
    {
        if (starImage != null)
        {
            starImage.color = color;
        }
    }


}
