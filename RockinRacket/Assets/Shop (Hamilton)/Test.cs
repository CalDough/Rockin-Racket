using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image Highlight;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Highlight.color = new Color(255, 100, 255, 0);
        //Highlight.sprite_renderer.color = new Color(1f, 0f, 0f, 1f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Highlight.color = new Color(0, 0, 0, 255);
    }
}
