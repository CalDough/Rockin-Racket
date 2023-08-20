using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is for a test feature for an overworld level system like in mario. Ignore for now.
*/
public class OverworldEventArgs : EventArgs
{
    public LevelLocation LevelLocation { get; private set; }

    public OverworldEventArgs(LevelLocation levelLocation)
    {
        LevelLocation = levelLocation;
    }
}
