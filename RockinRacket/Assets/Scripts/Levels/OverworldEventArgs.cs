using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEventArgs : EventArgs
{
    public LevelLocation LevelLocation { get; private set; }

    public OverworldEventArgs(LevelLocation levelLocation)
    {
        LevelLocation = levelLocation;
    }
}
