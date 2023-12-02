using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingingNote : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] float lerpDuration;
    [SerializeField] int destroyTimer;
    [SerializeField] MicNoteHelping parent;
    public Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LerpToPosition(destination, lerpDuration));
        StartCoroutine(DestroyAfterXSeconds(destroyTimer));
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.Translate(new Vector3(gameObject.transform.position.x - moveSpeed, gameObject.transform.position.y, 0) * Time.deltaTime);
    }

    private IEnumerator LerpToPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private IEnumerator DestroyAfterXSeconds(int destroyAfter)
    {
        int counter = destroyAfter;
        while (counter > 0)
        {
            yield return new WaitForSeconds(counter);
            counter--;
        }
        Debug.Log("Destroying Note");
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<RawImage>().color = Color.green;
        parent.IncrementClickCount();
    }
}
