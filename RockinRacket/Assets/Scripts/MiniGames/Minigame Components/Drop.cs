using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    [SerializeField] int slotID;  // Using ID instead of slotName for better clarity
    [SerializeField] int minigameID;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("DROPPED");

        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (draggable != null && draggable.SlotID == slotID)
        {
            DropEvent.ItemDropped(minigameID, draggable);
            
            draggable.transform.position = transform.position;
            Debug.Log("Dragged");
        }
    }
}