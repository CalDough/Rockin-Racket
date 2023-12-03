using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovingCircle : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ChordFinding parent;
    [SerializeField] float duration;

    private RawImage rawImage;

    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        duration = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<RawImage>().color = Color.green;
        parent.IncrementClickCount();
        rawImage.enabled = false;
        gameObject.GetComponent<RawImage>().color = Color.red;
    }

    public void StartToShrink()
    {
        //StartCoroutine(ShrinkOverTime());
    }

    IEnumerator ShrinkOverTime()
    {
        Vector3 initialScale = rawImage.transform.localScale;
        Vector3 targetScale = initialScale * 0.1f;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float lerpFactor = timer / duration;

            rawImage.transform.localScale = Vector3.Lerp(initialScale, targetScale, lerpFactor);

            yield return null;
        }

        rawImage.transform.localScale = targetScale;

        rawImage.enabled = false;
        rawImage.transform.localScale = initialScale;
        gameObject.GetComponent<RawImage>().color = Color.red;


        yield return null;
    }

}
