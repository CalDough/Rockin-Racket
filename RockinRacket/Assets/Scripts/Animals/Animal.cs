using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
    This script contains an animal object in code
    These are attendees of concerts
    Classes like AnimalManager will use the Load function to load Animals from a list of scriptable objects 
    Variables like BandInterest, Frugality, CarryOverChance, Mood are still being determined for their complete interactions
    BandInterest is used to see if an animal will try to attend
    Frugality is used to check if a ticket price is too high
    CarryOverChance will check if they continue to follow you into the next concert no matter what
    Mood is how happy/excited they are, which would affect stuff like mini-games involving them or events
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
