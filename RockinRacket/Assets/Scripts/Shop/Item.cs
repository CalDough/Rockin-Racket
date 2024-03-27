using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    This script is a scriptable object for easily making items in the editor
*/

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Test Item", order = 0)]
public class Item : ScriptableObject
{
    public int ShopIndex;
    public Sprite sprite;
    public Sprite selectedSprite;
    public Bandmate itemType;
    public string description;
    public int cost;
    public double scoreMultiplier = 1;
    public GameObject minigameObject;
    //public double freqMod;
    //public double diffMod;
}
