using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalData", menuName = "ScriptableObjects/AnimalData", order = 1)]
public class AnimalData : ScriptableObject
{
    [field:SerializeField]public int ID { get; private set; }
    [field:SerializeField] public string AnimalName { get; private set; }

    public int FollowerCount;
    [Range(-100, 100)] public int BandInterest;
    [Range(0, 100)] public int Frugality;
    [Range(0, 100)] public int CarryOverChance;
    [Range(0, 100)] public int Mood = 0;

}
