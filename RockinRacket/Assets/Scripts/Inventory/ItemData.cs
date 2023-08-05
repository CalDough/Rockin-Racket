using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemData : ScriptableObject
{
    public int ID;
    public string ItemName;
    public string ImagePath;
    public string Description;
    public int Value;
    public int Amount;
    public int Durability;
    public int MoodBonus;
    public bool IsKeyItem; //Prevents selling or removal
    public bool IsConsumable; //Can be activated before concert or during
    
    public List<Attribute> ItemTypes;
    public List<Skill> SkillBonus;
}
