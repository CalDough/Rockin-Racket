using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBounce : MonoBehaviour
{
    [SerializeField] private int startX = 0;
    [SerializeField] private int startHeight;
    [SerializeField] private int bounceHeight;
    [SerializeField] private float animTime;

    private Vector3 initPos;

    private void Awake()
    {
        initPos = new(startX, startHeight, 0);
        //initPos = transform.localPosition;
    }

    void OnEnable()
    {
        StartCoroutine(Up());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Up()
    {
        float counter = 0;

        Vector3 startPos = initPos;
        Vector3 endPos = new(startPos.x, startPos.y + bounceHeight, startPos.z);

        while (counter < animTime/2)
        {
            counter += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, counter / (animTime / 2f));
            yield return null;
        }
        StartCoroutine(Down());
    }

    private IEnumerator Down()
    {
        float counter = 0;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = new(startPos.x, startPos.y - bounceHeight, startPos.z);

        while (counter < animTime/2)
        {
            counter += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, counter / (animTime / 2f));
            yield return null;
        }
        StartCoroutine(Up());
    }
}
