using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OverworldEvent
{
    public static event EventHandler<OverworldEventArgs> OnLevelSelected;
    public static event EventHandler<OverworldEventArgs> OnLevelHovered;
    public static event EventHandler<OverworldEventArgs> OnLevelUnhovered;
    public static event EventHandler<OverworldEventArgs> OnLevelEntered;
    public static event EventHandler<OverworldEventArgs> OnTeleporterDoubleClick;

    public static void LevelSelected(LevelLocation levelLocation)
    {
        OnLevelSelected?.Invoke(null, new OverworldEventArgs(levelLocation));
    }

    public static void LevelHovered(LevelLocation levelLocation)
    {
        OnLevelHovered?.Invoke(null, new OverworldEventArgs(levelLocation));
    }

    public static void LevelUnhovered(LevelLocation levelLocation)
    {
        OnLevelUnhovered?.Invoke(null, new OverworldEventArgs(levelLocation));
    }

    public static void LevelEntered(LevelLocation levelLocation)
    {
        OnLevelEntered?.Invoke(null, new OverworldEventArgs(levelLocation));
    }
    
    public static void TeleporterDoubleClick(LevelLocation levelLocation)
    {
        OnTeleporterDoubleClick?.Invoke(null, new OverworldEventArgs(levelLocation));
    }
}
