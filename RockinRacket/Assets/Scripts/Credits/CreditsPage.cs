using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPage : MonoBehaviour
{
    [SerializeField] private UIFade[] UIObjects;

    public void FadeIn(float animationTime)
    {
        foreach (UIFade UIObject in UIObjects)
        {
            UIObject.FadeIn(animationTime);
        }
    }
    public void FadeOut(float animationTime)
    {
        foreach (UIFade UIObject in UIObjects)
        {
            UIObject.FadeOut(animationTime);
            print("fading out");
        }
    }
}