using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class stores the songs that are going to be played during a particular concert
 * 
 */


[CreateAssetMenu(fileName = "ConcertData", menuName = "ScriptableObjects/ConcertData", order = 1)]
public class ConcertData : ScriptableObject
{
    public List<SongData> concertSongsFirstHalf;
    public List<SongData> concertSongsSecondHalf;
}
