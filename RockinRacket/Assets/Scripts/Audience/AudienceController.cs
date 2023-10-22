using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceController : MonoBehaviour
{
    [SerializeField] private List<AudienceMember> audienceMembers;
    [SerializeField] private float lowHypeThreshold = 333f;
    [SerializeField] private float highHypeThreshold = 666f;
    [SerializeField] private float lowComfortThreshold = 333f;
    [SerializeField] private float highComfortThreshold = 666f;
    [SerializeField] private float audienceChangeInterval = 5f;
    [SerializeField] private GameObject audienceMemberPrefab;
    [SerializeField] private Transform audienceSpawnPoint;

    [SerializeField] public AudienceHypeState currentHypeState;
    [SerializeField] public AudienceComfortState currentComfortState;
    
    private Coroutine ManageAudienceMembersCoroutine;

    void Start()
    {
        SubscribeEvents();
        
        if (MinigameStatusManager.Instance != null)
        {
            UpdateAudienceState();
        }
        StartCoroutine(ManageAudienceMembers());
    }
    
    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void UpdateAudienceState()
    {
        if (MinigameStatusManager.Instance.hype < lowHypeThreshold)
        {
            currentHypeState = AudienceHypeState.LowHype;
        }
        else if (MinigameStatusManager.Instance.hype < highHypeThreshold)
        {
            currentHypeState = AudienceHypeState.MidHype;
        }
        else
        {
            currentHypeState = AudienceHypeState.HighHype;
        }

        if (MinigameStatusManager.Instance.comfort < lowComfortThreshold)
        {
            currentComfortState = AudienceComfortState.LowComfort;
        }
        else if (MinigameStatusManager.Instance.comfort < highComfortThreshold)
        {
            currentComfortState = AudienceComfortState.MidComfort;
        }
        else
        {
            currentComfortState = AudienceComfortState.HighComfort;
        }

        foreach (var audienceMember in audienceMembers)
        {
            audienceMember.UpdateState(currentHypeState, currentComfortState);
        }
    }

    private IEnumerator ManageAudienceMembers()
    {
        while (true)
        {
            yield return new WaitForSeconds(audienceChangeInterval);
            MoodEvent.HypeAndComfortChange(this.currentHypeState, this.currentComfortState);

            if (currentHypeState == AudienceHypeState.HighHype && audienceMembers.Count < 20)
            {
                AddAudienceMember();
            }
            else if (currentComfortState == AudienceComfortState.LowComfort && audienceMembers.Count > 5)
            {
                RemoveAudienceMember();
            }
        }
    }

    private void RandomizeAudienceMembers()
    {
       
    }

    private void AddAudienceMember()
    {
        GameObject newMemberObj = Instantiate(audienceMemberPrefab, audienceSpawnPoint.position, Quaternion.identity);
        AudienceMember newMember = newMemberObj.GetComponent<AudienceMember>();
        if (newMember != null)
        {
            newMember.Init(this);
            audienceMembers.Add(newMember);
        }
        //newMember.MoveToConcertSpot()
    }

    private void RemoveAudienceMember()
    {
        if (audienceMembers.Count > 0)
        {
            int randomIndex = Random.Range(0, audienceMembers.Count);
            AudienceMember memberToRemove = audienceMembers[randomIndex];
            memberToRemove.ExitConcert(this.audienceSpawnPoint.position);
            audienceMembers.RemoveAt(randomIndex);
        }
    }

    public int GetAudienceCount()
    {
        return audienceMembers.Count;
    }

    private void SubscribeEvents()
    {
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        switch(e.stateType)
        {
            case GameModeType.Song:
                ManageAudienceMembersCoroutine = StartCoroutine(ManageAudienceMembers());
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        switch(e.stateType)
        {
            case GameModeType.Song:
                if(ManageAudienceMembersCoroutine != null)
                {
                    StopCoroutine(ManageAudienceMembersCoroutine);
                }
                break;
            default:
                break;
        }
    }
}
