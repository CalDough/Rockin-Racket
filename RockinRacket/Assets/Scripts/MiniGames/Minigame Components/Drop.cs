using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    [SerializeField] string slotName;
    [SerializeField] int minigameID;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.transform.name == slotName)
        {
            Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
            if(draggable != null)
            {
                draggable.startPosition = transform.position;
            }
        }
    }
}
