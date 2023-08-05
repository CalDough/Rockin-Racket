using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour
{
    private int index;
    private IScrollSelector Selector;

    public void Setup(int index, IScrollSelector Selector)
    {
        this.index = index;
        this.Selector = Selector;

        // Set the onClick event
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        Selector.OnButtonClick(index);
    }
}