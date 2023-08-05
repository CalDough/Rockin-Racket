using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Game Event Container", menuName = "ScriptableObjects/Game Event Container", order = 1)]
public class GameEventContainer : ScriptableObject
{

    public List<GameObject> EventPrefabs;
    
    //public List<ConcertEvent> ConcertEvent;
}
