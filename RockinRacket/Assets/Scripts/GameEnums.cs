using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums : MonoBehaviour
{
    //All Enums below here along with any helper class
}

public enum Bandmate { MJ, Kurt, Ace, Haley };

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
public enum StateType
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
    None,
    tshirt,
    mug,
    button
}

[System.Serializable]
public enum ConcertState 
{
    BandView, 
    ShopView, 
    BackstageView, 
    AudienceView, 
    VenueView 
};

[System.Serializable]
public enum PlayerTools
{ 
    DEFAULTNoTool, 
    TrashTool, 
    TShirtCannonTool 
}

[System.Serializable]
public enum Achievements
{
    ACH_TUTORIAL_COMPLETE,
    ACH_LEVEL_ONE_COMPLETE,
    ACH_LEVEL_TWO_COMPLETE,
    ACH_LEVEL_THREE_COMPLETE,
    ACH_LEVEL_FOUR_COMPLETE
}

[System.Serializable]
public enum Levels
{
    TUTORIAL,
    LEVEL_ONE,
    LEVEL_TWO,
    LEVEL_THREE,
    LEVEL_FOUR,
}