using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IpodOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PauseManager pauseManeger;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ScaleOverTime(new Vector3(1.06f, 1.06f, 1f), .1f));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //StartCoroutine(ScaleOverTime(new Vector3(1f, 1f, 1f), .5f));
        transform.localScale = new Vector3(1, 1, 1);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pauseManeger.PlayButtonDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pauseManeger.PlayButtonUp();
    }

    private IEnumerator ScaleOverTime(Vector3 toScale, float duration)
    {
        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = transform.localScale;

        while (counter < duration)
        {
            counter += Time.unscaledDeltaTime;
            transform.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            //transform.rotation = 
            yield return null;
        }
    }
}
