using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateData : MonoBehaviour
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GamestateDataHolder")]

    public class RuntimeData : ScriptableObject
    {
        public ConcertState CurrentConcertState;
    }
}
