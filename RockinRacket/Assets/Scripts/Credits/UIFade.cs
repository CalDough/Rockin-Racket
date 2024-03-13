using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIFade : MonoBehaviour
{
    [SerializeField] private MaskableGraphic UIElement;
    public void FadeIn(float animationTime)
    {
        StartCoroutine(FadeElement(animationTime, new(UIElement.color.r, UIElement.color.g, UIElement.color.b, 1f)));
    }
    public void FadeOut(float animationTime)
    {
        StartCoroutine(FadeElement(animationTime, new(UIElement.color.r, UIElement.color.g, UIElement.color.b, 0f)));
    }

    private IEnumerator FadeElement(float animationTime, Color endState)
    {
        float counter = 0f;
        Color startColor = UIElement.color;
        Color endColor = endState;
        while (counter < animationTime)
        {
            counter += Time.unscaledDeltaTime;
            UIElement.color = Color.Lerp(startColor, endColor, counter / animationTime);
            yield return null;
        }
    }
}
