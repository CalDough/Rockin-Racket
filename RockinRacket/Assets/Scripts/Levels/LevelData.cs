using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "ScriptableObjects/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    [Multiline]
    public string levelDescription;
    [Multiline]
    public string levelInformation;
    public int fameRequirements;
    public int moneyRequirements;
    public List<string> itemRequirements;
    
    [Header("Checked = No Reqs to Enter")]
    public bool requirementsDisabled;
    
    [Header("IMPORTANT")]
    public string sceneToLoad;
}