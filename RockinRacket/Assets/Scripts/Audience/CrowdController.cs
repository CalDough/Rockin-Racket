using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    public List<CrowdMember> crowdMembers;
    private int maxTrashCount = 10;
    private int currentTrashCount = 0;

    void Start()
    {
        GameEvents.OnEventFail += HandleEventFail;
        GameEvents.OnEventComplete += HandleEventComplete;
    }

    void Update()
    {

    }

    public void UpdateCrowdMood()
    {

    }

    public void SpawnTrash()
    {
        if (currentTrashCount < maxTrashCount)
        {
            currentTrashCount++;
        }
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {

    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {

    }


}
