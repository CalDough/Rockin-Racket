using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Screw : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject screwImage;
    public int totalClicksToUnscrew = 3;
    public float rotationPerClick = 90f;
    public float cooldownTime = 0.5f;

    private int currentClicks = 0;
    private bool isCooldown = false;

    public bool IsUnscrewed => currentClicks >= totalClicksToUnscrew;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Screw Clicked");
        if (!isCooldown && currentClicks < totalClicksToUnscrew)
        {
            StartCoroutine(RotateAndCooldown());
        }
    }

    public void ResetScrew()
    {
        StopAllCoroutines();
        currentClicks = 0;
        isCooldown = false;
        transform.rotation = Quaternion.identity;
        if (screwImage != null) 
        {
            screwImage.SetActive(true);
        }
    }

    private IEnumerator RotateAndCooldown()
    {
        isCooldown = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotationPerClick);
        float timeElapsed = 0f;

        while (timeElapsed < cooldownTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, timeElapsed / cooldownTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;  
        currentClicks++;
        isCooldown = false;
        if (IsUnscrewed && screwImage != null)
        {
            screwImage.SetActive(false);
        }
    }
}
