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
    public enum MinigameEnum { Guitar, Microphone, Popcorn, Fire, TShirt };
    public enum Type { Instrument, Stage, Minigame };

    public int ID;
    public string itemName;
    public Sprite sprite;
    public Type itemType;
    public string description;
    public int cost;
    public double scoreMultiplier = 1;
    public GameObject minigameToSpawn;

    public MinigameEnum MinigameBonus;
    public double freqMod;
    public double diffMod;
}
