using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using UnityEditor.ShaderGraph.Internal;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Object References")]
    [SerializeField] Image trashImage;
    // TEMPORARY - WILL FIX LATER
    [SerializeField] Vector2 DumpsterPos;
    [SerializeField] Vector2 DumpsterDimensions;
    GameObject dumpster;
    public Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        dumpster = GameObject.Find("Dumpster");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        trashImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    /*
     * NEEDS REFACTORING
     */
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Position: " + eventData.position);
        float xPos = eventData.position.x;
        float yPos = eventData.position.y;
        //Debug.Log(DumpsterPos.x + " " + (DumpsterPos.x + DumpsterDimensions.x));
        //Debug.Log(DumpsterPos.y + " " + (DumpsterPos.y + DumpsterDimensions.y));

        if (xPos < 950)
        {
            if (yPos > 50)
            {
                //Debug.Log("Within");
                DropEvents.current.e_DropEvent.Invoke(0);
                Destroy(gameObject);
            }
        }


        trashImage.raycastTarget = true;
    }
}
