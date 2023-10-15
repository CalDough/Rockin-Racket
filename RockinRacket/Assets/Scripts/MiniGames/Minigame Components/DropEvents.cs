using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class DropEvent
{
    public static event EventHandler<DropEventArgs> OnItemDropped;

    public static void ItemDropped(int minigameID, Draggable item)
    {
        OnItemDropped?.Invoke(null, new DropEventArgs(minigameID, item));
    }
}

public class DropEventArgs : EventArgs
{
    public int MinigameID { get; }
    public Draggable DroppedItem { get; }

    public DropEventArgs(int minigameID, Draggable droppedItem)
    {
        MinigameID = minigameID;
        DroppedItem = droppedItem;
    }
}