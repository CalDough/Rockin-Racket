using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GamestateData")]

public class GameStateData : ScriptableObject
{
    public ConcertState CurrentConcertState;
    public PlayerTools CurrentToolState;
}
