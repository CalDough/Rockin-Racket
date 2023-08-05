using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public TrackData PositionOne; //Usually Vocalist + Guitar Strings
    public TrackData PositionTwo; //Usually Bass Strings + Vocals
    public TrackData PositionThree; //Usually Strings + Vocals
    public TrackData PositionFour; //Usually Drums + Vocals
    public TrackData Background; //Any extra music you want for the concert that isnt played by raccoons

}
[System.Serializable]
public enum SongType
{
    Rock,
    Punk,
    Pop,
}