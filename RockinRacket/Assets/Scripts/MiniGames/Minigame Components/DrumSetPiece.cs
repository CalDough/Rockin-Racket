using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrumSetPiece : MonoBehaviour, IPointerClickHandler
{
    public DrumGuide drumGuide; 
    public Image highlightImage;
    public float fadeDuration = 0.5f; 

    private void Start()
    {
        SetImageOpacity(highlightImage, 0);
    }

    public void HighlightDrum()
    {
        if (highlightImage.gameObject.activeInHierarchy)
        {
            StopAllCoroutines(); 
            StartCoroutine(FadeTo(highlightImage, 1, fadeDuration));
        }
        else
        {
            SetImageOpacity(highlightImage, 1); 
        }
    }
    
    public void HideDrum()
    {
        if (highlightImage.gameObject.activeInHierarchy)
        {
            StopAllCoroutines(); 
            StartCoroutine(FadeTo(highlightImage, 0, fadeDuration));
        }
        else
        {
            SetImageOpacity(highlightImage, 0); 
        }
    }

    IEnumerator FadeTo(Image image, float targetOpacity, float duration)
    {
        float startOpacity = image.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startOpacity, targetOpacity, time / duration);
            Color color = image.color;
            color.a = alpha;
            image.color = color;
            yield return null;
        }

        Color finalColor = image.color;
        finalColor.a = targetOpacity;
        image.color = finalColor;
    }

    private void SetImageOpacity(Image image, float opacity)
    {
        Color color = image.color;
        color.a = opacity;
        image.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} clicked.");

        drumGuide.OnDrumClicked(gameObject);

        HideDrum();
    }
}