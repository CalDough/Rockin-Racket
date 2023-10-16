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

    public int ID { get; private set; }
    public string ItemName { get; private set; }
    public Sprite Sprite { get; private set; }
    public ItemType itemType;
    public string Description { get; private set; }
    public int Cost { get; private set; }
    public double ScoreMultiplier { get; private set; }
    public GameObject minigameObject;
    public MinigameType minigameType;
    //public double freqMod;
    //public double diffMod;
}
