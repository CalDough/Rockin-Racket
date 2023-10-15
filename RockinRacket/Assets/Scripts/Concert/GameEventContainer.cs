using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is meant to help level designs have an easy object to put the game event prefabs into.
    Other scripts should have a reference to a scriptable MinigameContainer to help manage where their events are stored.
    Example scripts that may need MinigameContainer.
    1. Animal Manager: One or two MinigameContainer for this script to hold events relating to specific Attendees
    2. Band Manager: Maybe 5? MinigameContainer for the band. One for each character and a related problem to their equipment or mood.
        Events from this class might need to interact with Audiomanager to disable/enable sounds
    3. GameEventManager: 1-2 for default events that can happen in any concert
    4. Venue: Each venue can have it's own custom events that only occur there.
    5. StoryManager: Upon reaching certain milestones in the story, we may want custom events related to the story to appear in the concert:
        These events might need to interact with multiple Managers.


*/
[CreateAssetMenu(fileName = "New MiniGame Container", menuName = "ScriptableObjects/Game Event Container", order = 1)]
public class MinigameContainer : ScriptableObject
{

    public List<GameObject> MiniGamesPrefabs;
    
}
