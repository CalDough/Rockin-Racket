using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This scriptable object stores the cork board text that is displayed when the user clicks on a level
 * 
 * It is stored here to make changing it easier instead of having it inside ConcertBoardController.cs
 * 
 */


[CreateAssetMenu(fileName = "CorkBoardInfoText", menuName = "ScriptableObjects/Cork Board Info Text", order = 2)]
public class CorkBoardConcertInfoText : ScriptableObject
{
    [Header("Tutorial")]
    public string tutorialNotePadTitle;
    public string tutorialNotePadDescription;
    public string tutorialStartLevelText;
    public TransitionData tutorialTransitionData;

    [Header("Level One")]
    public string levelOneNotePadTitle;
    public string levelOneNotePadDescription;
    public string levelOneStartLevelText;
    public TransitionData levelOneTransitionData;

    [Header("Level Two")]
    public string levelTwoNotePadTitle;
    public string levelTwoNotePadDescription;
    public string levelTwoStartLevelText;
    public TransitionData levelTwoTransitionData;

    [Header("Level Three")]
    public string levelThreeNotePadTitle;
    public string levelThreeNotePadDescription;
    public string levelThreeStartLevelText;
    public TransitionData levelThreeTransitionData;

    [Header("Level Four")]
    public string levelFourNotePadTitle;
    public string levelFourNotePadDescription;
    public string levelFourStartLevelText;
    public TransitionData levelFourTransitionData;

}
