using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is a scriptable object that holds data for each Song.
    Each song should have at least 1 position with filled track data.
    Track Data has a Primary and Secondary String path, which holds a path to an FMOD event
    When setting Track Data, you can set an Attribute to let the visual scripts know what instrument type the Raccoon should be using/holding
    Other variables such as Difficulty and Song Type help other systems such as Event and Animal Manager determine what guests want to show up
*/
[CreateAssetMenu(fileName = "New Song", menuName = "ScriptableObjects/Song", order = 1)]
public class SongData : ScriptableObject
{
    public int ID;
    public string SongName;
    [Multiline]
    public string Description;
    public float Duration;
    public int Difficulty; 
    public List<SongType> SongTypes;

    public TrackData Ace; //Usually Vocalist + Guitar Strings
    public TrackData Haley; //Usually Bass Strings + Vocals
    public TrackData Kurt; //Usually Strings + Vocals
    public TrackData MJ; //Usually Drums + Vocals
    public TrackData Speakers; //Any extra music you want for the concert that isnt played by raccoons

}
[System.Serializable]
public enum SongType
{
    Rock,
    Punk,
    Pop,
}