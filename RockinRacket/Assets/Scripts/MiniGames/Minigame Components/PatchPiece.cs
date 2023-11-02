using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class PatchPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    public int patchSize = 1;
    public bool canDrag = true;
    private Vector2 originalPosition;
    // Reference to the main TrashSorting script
    public DrumRepair drumRepair;

    public string patchPlaceSoundEvent = "";


    public float soundStartVolume = 1;

    public void PlaySound()
    {
        if (!string.IsNullOrEmpty(patchPlaceSoundEvent))
        {
            FMOD.Studio.EventInstance soundInstance = RuntimeManager.CreateInstance(patchPlaceSoundEvent);
            soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
            soundInstance.setVolume(soundStartVolume);
            soundInstance.start();
            soundInstance.release();
        }
    }

    void Start()
    {
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging Trash");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(canDrag)
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if we're over a dumpster
        foreach (RectTransform holes in drumRepair.drumHoles)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(holes, eventData.position))
            {
                DrumHole hole = holes.GetComponent<DrumHole>();

                if(hole && hole.isFilled == false && hole.holeSize == patchSize)
                {
                    this.canDrag = false;
                    hole.isFilled = true;
                    StartCoroutine(LerpToPosition(holes.position, 1.0f)); 
                    drumRepair.CheckForCompletion();
                    break;
                }
                else
                {
                    ResetPosition();
                }
            }
        }
    }

    private IEnumerator LerpToPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Ensure the object is exactly at the target position when the lerp is done
        transform.position = targetPosition;
        PlaySound();
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
        this.canDrag = true;
    }
}