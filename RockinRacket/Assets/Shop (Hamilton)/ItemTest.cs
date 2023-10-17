using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    This script is a scriptable object for easily making items in the editor and placing it onto a shop, inventory, or event reward
*/

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Test Item", order = 0)]
public class ItemTest : ScriptableObject
{
    public enum MinigameType { NONE, DialTuning, TrashSort, Unique};
    public enum ItemType { Instrument, Stage, Audience};

    public int ID;
    public string itemName;
    public Sprite sprite;
    public ItemType itemType;
    public string description;
    public int cost;
    public double scoreMultiplier;
    public GameObject minigameObject;
    public MinigameType minigameType;
    //public double freqMod;
    //public double diffMod;
}
