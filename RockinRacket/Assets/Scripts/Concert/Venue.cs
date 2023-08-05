using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//fix to not reload set lists
[CreateAssetMenu(fileName = "New Venue", menuName = "ScriptableObjects/Venue", order = 1)]
public class Venue : ScriptableObject
{
    public string VenueName;
    public int Difficulty;
    public int SongSlots;
    public List<Environment> environments;
    public VenueSize venueSize;

    public List<int> setGuests; //sorry but have to have this as an ID system for easier serialization

    [SerializeField] public List<GameState> StagedGameStates;

    [Header("Band Battle Settings")]
    [Header("Set their Songs in the Staged States")]
    public bool IsBandBattle;
    public List<Animal> OpponentBand;
    public int FameToBeat;
    public int MoneyToBeat;
    public int StyleScoreToBeat;
    [Header("Scene To Load")]
    
    public int SceneIndex;
}

[System.Serializable]
public enum VenueSize
{
    Default,
    Small,
    Medium,
    Large
}

[System.Serializable]
public enum Environment
{
    Urban,
    Rural,
    City,
    Indoors,
    Outdoors,
}