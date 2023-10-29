using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TrashObject : MonoBehaviour, IPointerClickHandler
{
    public int hitPoints = 3; 
    public int value = 1;     // Value of the trash when cleaned
    public Cleaning cleaning;
    public TextMeshProUGUI hpText;

    [Header("Shake Settings")]
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.5f;

    private Vector3 originalPosition;

    void Start()
    {
        UpdateHpText();
        originalPosition = transform.localPosition;
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hitPoints--;
        UpdateHpText();
        if (hitPoints <= 0)
        {
            cleaning.CleanupTrash(this);
        }
        else
        {
            StartCoroutine(Shake());
        }
    }

    private void UpdateHpText()
    {
        if (hpText != null)
        {
            hpText.text = hitPoints.ToString();
        }
    }
    
    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }

}
