using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums : MonoBehaviour
{
    //All Enums below here along with any helper class
}
[System.Serializable]
public enum BandRoleName
{
    Default,
    Ace,
    Haley,
    Harvey,
    Kurt,
    MJ,
    Speakers,
    Other

}
public static class BandRoleEnumHelper
{
    private static System.Random random = new System.Random();

    public static BandRoleName GetRandomBandRoleName(List<BandRoleName> exclusions = null)
    {
        if(exclusions == null)
        {
            exclusions = new List<BandRoleName>();
        }

        List<BandRoleName> values = new List<BandRoleName>((BandRoleName[]) Enum.GetValues(typeof(BandRoleName)));

        foreach (var exclusion in exclusions)
        {
            values.Remove(exclusion);
        }

        return values[random.Next(values.Count)];
    }
}
[System.Serializable]
public enum SongType
{
    Rock,
    Punk,
    Pop,
}
[System.Serializable]
public enum GameModeType
{
    Default, //
    SceneIntro, // Scene intro with characters entering location
    SceneOutro, // Scene outro with characters leaving to resting location

    SongIntro, //
    Song, //holds track to play as well as plays animations for music
    SongOutro, // Just adding a short intro outro for each song for the animation sequence

    IntermissionIntro, // 
    Intermission, // paused time for player to talk and choose when to go back to song
    IntermissionOutro, //

    Cutscene, //
    Dialogue, //
    BandBattle,

}
/*
    I was planning on having Venue size determine max occupancy of Attendees and stuff
*/
[System.Serializable]
public enum VenueSize
{
    Default,
    Small,
    Medium,
    Large
}
/*
    I was planning on having enviroment determine potential mini-games that could occur
*/
[System.Serializable]
public enum Environment
{
    Urban,
    Rural,
    City,
    Indoors,
    Outdoors,
}
/*
    This script is an enum that has the core values related to items, skills, and roles in the game.
    It is meant to simplify what an item is for, what skills can be, and what instruement or job a raccoon is doing in the concert at the time.
*/

public enum Attribute
{
    Vocal,
    Strings,
    Percussion,
    Keyboard,
    Brass,
    Woodwind,
    Repair,
    Management

}
[System.Serializable]
public enum DifficultyMode 
{
    VeryEasy,
    Easy,
    Normal,
    Hard,
    VeryHard
}

[System.Serializable]
public enum CustomerWants // Enums for the customer wants for the Merch Table minigame during the concert
{
    tshirt,
    mug,
    button
}