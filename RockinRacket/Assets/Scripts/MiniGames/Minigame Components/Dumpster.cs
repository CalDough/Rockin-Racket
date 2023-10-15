using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumpster : MonoBehaviour
{
    public TrashType acceptsType; // Set this in the inspector for each dumpster
}

public enum TrashType
{
    Red,
    Blue,
    Yellow,
    Green,
    Orange,
    White
}