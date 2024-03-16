using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageFade : MonoBehaviour, IUIFade
{
    [SerializeField] private Image image;
    public void FadeIn(float animationTime)
    {
        StartCoroutine(FadeElement(animationTime, new(image.color.r, image.color.g, image.color.b, 1f)));
    }
    public void FadeOut(float animationTime)
    {
        StartCoroutine(FadeElement(animationTime, new(image.color.r, image.color.g, image.color.b, 0f)));
    }

    public void SetAlpha(float alpha)
    {
        image.color = new(image.color.r, image.color.g, image.color.b, alpha);
    }

    private IEnumerator FadeElement(float animationTime, Color endColor)
    {
        float counter = 0f;
        Color startColor = image.color;
        while (counter < animationTime)
        {
            counter += Time.unscaledDeltaTime;
            image.color = Color.Lerp(startColor, endColor, counter / animationTime);
            yield return null;
        }
    }
}
