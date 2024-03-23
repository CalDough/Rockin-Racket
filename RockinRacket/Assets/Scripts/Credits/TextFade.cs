using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TextFade : MonoBehaviour, IUIFade
{
    [SerializeField] private TMP_Text text;
    public void FadeIn(float animationTime)
    {
        StartCoroutine(FadeElement(animationTime, new(text.color.r, text.color.g, text.color.b, 1f)));
    }
    public void FadeOut(float animationTime)
    {
        StartCoroutine(FadeElement(animationTime, new(text.color.r, text.color.g, text.color.b, 0f)));
    }

    public void SetAlpha(float alpha)
    {
        text.color = new(text.color.r, text.color.g, text.color.b, alpha);
    }

    private IEnumerator FadeElement(float animationTime, Color endColor)
    {
        float counter = 0f;
        Color startColor = text.color;
        while (counter < animationTime)
        {
            counter += Time.unscaledDeltaTime;
            text.color = Color.Lerp(startColor, endColor, counter / animationTime);
            yield return null;
        }
        text.color = endColor;
    }
}
