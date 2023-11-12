using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is a scriptable object that holds data for each Song.
    The song's full track should be a string path to an FMOD event and should be in the FMODMultiTrack TrackData
    TrackData has been simplified to a Single Path String only, but may contain boolean values as well as other
    variables in the future
*/
[CreateAssetMenu(fileName = "New Song", menuName = "ScriptableObjects/Song", order = 1)]
public class SongData : ScriptableObject
{
    public int ID;
    public string SongName;
    [Multiline]
    public string Description;
    public float Duration;
    public TrackData FMODMultiTrack; //All Tracks in logic form in FMOD

    public TrackData Ace; //Usually Vocalist + Guitar Strings
    public TrackData Haley; //Usually Bass Strings + Vocals
    public TrackData Kurt; //Usually Strings + Vocals
    public TrackData MJ; //Usually Drums + Vocals
    public TrackData Speakers; //Any extra music you want for the concert that isnt played by raccoons

}
