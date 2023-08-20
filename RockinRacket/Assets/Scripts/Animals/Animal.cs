using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
    This script contains an animal object in code
    These are attendees of concerts
    

*/
[Serializable]
public class Animal 
{
    [field:SerializeField]public int ID { get; private set; }
    [field:SerializeField] public string AnimalName { get; private set; }

    public int FollowerCount;
    [Range(-100, 100)] public int BandInterest;
    [Range(0, 100)] public int Frugality;
    [Range(0, 100)] public int CarryOverChance;
    [Range(0, 100)] public int Mood = 0;

    public void Load(AnimalData animalData)
    {
        this.ID = animalData.ID;
        this.AnimalName = animalData.AnimalName;
        this.FollowerCount = animalData.FollowerCount;
        this.BandInterest = animalData.BandInterest;
        this.Frugality = animalData.Frugality;
        this.CarryOverChance = animalData.CarryOverChance;
        this.Mood = animalData.Mood;
    }
}
