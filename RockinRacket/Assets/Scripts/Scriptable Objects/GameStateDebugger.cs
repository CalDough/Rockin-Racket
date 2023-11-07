using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This class exists to set the game state to band view at the start of a concert
 * WILL BE REPLACED LATER
 */

public class GameStateDebugger : MonoBehaviour
{

    [SerializeField] GameStateData currentGameState;

    void Start()
    {
        currentGameState.CurrentConcertState = ConcertState.BandView;
    }


}
