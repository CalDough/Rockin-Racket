using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Transition", menuName = "ScriptableObjects/Transition", order = 1)]
public class TransitionData : ScriptableObject
{
    public int sceneIndex;
    public GameObject animatorPrefab;
    public float transitionTime = 1f;
    public string loadingSceneName = "LoadingScene";
}
