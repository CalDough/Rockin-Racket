using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is a scriptable object to help easily figure out what venue a level has and the data about it.
    Venues affect things such as difficulty, potnetial songs, max songs, events that can appear, animals that will appear, the enemy band, and more.
    GameStateManager is the main script that will hold a venue for other managers to view.
    
*/
[CreateAssetMenu(fileName = "New Venue", menuName = "ScriptableObjects/Venue", order = 1)]
public class Venue : ScriptableObject
{
    public string VenueName;
    public int Difficulty;
    public int SongSlots;
    public List<Environment> environments;
    public VenueSize venueSize;

    [SerializeField] public List<GameState> StagedGameStates;

    [Header("Band Battle Settings")]
    [Header("Set their Songs in the Staged States")]
    public bool IsBandBattle;
    public int FameToBeat;
    public int MoneyToBeat;
    public int StyleScoreToBeat;
    [Header("Scene To Load")]
    
    public int SceneIndex;
}
