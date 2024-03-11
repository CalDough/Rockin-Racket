using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField] private MaskableGraphic UIElement;
    public void Fade(float animationTime)
    {
        StartCoroutine(FadeElement(animationTime));
    }

    private IEnumerator FadeElement(float animationTime)
    {
        float counter = 0f;
        Color startColor = UIElement.color;
        Color endColor = new(UIElement.color.r, UIElement.color.g, UIElement.color.b, 0f);
        while (counter < animationTime)
        {
            counter += Time.unscaledDeltaTime;
            UIElement.color = Color.Lerp(startColor, endColor, counter / animationTime);
            yield return null;
        }
    }
}
