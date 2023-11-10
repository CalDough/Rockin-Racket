using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySlot : MonoBehaviour
{
    private bool isOccupied = false;
    public RectTransform myRect;

    public bool IsValidAndEmpty()
    {
        return !isOccupied;
    }

    public void MarkAsOccupied()
    {
        isOccupied = true;
    }

    public void ResetSlot()
    {
        isOccupied = false;
    }
}
