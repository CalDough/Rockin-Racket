using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPage : MonoBehaviour
{
    [SerializeField] private GameObject[] UIObjects;

    private List<IUIFade> UIFades = new();

    public void GetUIFades()
    {
        foreach (GameObject UIObject in UIObjects)
        {
            UIFades.Add(UIObject.GetComponent<IUIFade>());
        }
    }

    public void FadeIn(float animationTime)
    {
        foreach (IUIFade UIFade in UIFades)
        {
            UIFade.FadeIn(animationTime);
            //print("Fading in " + gameObject.name);
        }
    }
    public void FadeOut(float animationTime)
    {
        foreach (IUIFade UIFade in UIFades)
        {
            UIFade.FadeOut(animationTime);
            //print("Fading out " + gameObject.name);
        }
    }

    public void SetAlpha(float alpha)
    {
        foreach (IUIFade UIFade in UIFades)
        {
            UIFade.SetAlpha(alpha);
        }
    }
}