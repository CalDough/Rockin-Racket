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

    public string concertName;
    public Material concertBackground;
    public Sprite backstageBackground;
    public TransitionData returnToConcertLoader;
    public SceneIndex currentLevel;
    public Levels concertLevelName;
    public Vector2[] intermissionCharacterLocations;

    [Header("Concert Player Stats")]
    public bool isPostIntermission = false;
    public float currentAudienceRating = 0;
    public int localMoney = 0;
    public int numMerchTableCustomers = 0;
    public int currentConcertScore = 0;
    public string currentConcertLetter = "C";
}
