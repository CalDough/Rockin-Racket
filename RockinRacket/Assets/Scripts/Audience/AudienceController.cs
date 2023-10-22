using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceController : MonoBehaviour
{
    [SerializeField] private List<AudienceRow> audienceRows;
    [SerializeField] private List<AudienceMember> audienceMembers;
    [SerializeField] private float lowHypeThresholdPercent = 20f; //  percentage
    [SerializeField] private float highHypeThresholdPercent = 50f; 
    [SerializeField] private float lowComfortThresholdPercent = 20f; 
    [SerializeField] private float highComfortThresholdPercent = 75f; 
    [SerializeField] private float audienceChangeInterval = 5f;
    [SerializeField] private List<GameObject> audienceMemberPrefabs;
    [SerializeField] private Transform audienceSpawnPoint;

    [SerializeField] private float moodRandomizationInterval = 15f;

    [SerializeField] public AudienceHypeState currentHypeState;
    [SerializeField] public AudienceComfortState currentComfortState;
    
    private Coroutine ManageAudienceMembersCoroutine;
    
    private Coroutine RandomizeAudienceMoodsCoroutine;

    [SerializeField] private float lowHypeThreshold;
    [SerializeField] private float highHypeThreshold;
    [SerializeField] private float lowComfortThreshold;
    [SerializeField] private float highComfortThreshold;

    void Start()
    {
        SubscribeEvents();

        if (audienceRows.Count == 0)
        {
            Debug.LogError("No audience rows assigned");
            return;
        }

        AssignAudienceMembersToRows();
    }
    
    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public AudienceRow GetRandomRow()
    {
        if (audienceRows.Count > 0)
        {
            int randomIndex = Random.Range(0, audienceRows.Count);
            return audienceRows[randomIndex];
        }
        return null;
    }
    
    private void AssignAudienceMembersToRows()
    {
        for (int i = 0; i < audienceMembers.Count; i++)
        {
            int rowIndex = i % audienceRows.Count;
            audienceMembers[i].SetRow(audienceRows[rowIndex]);
        }
    }

    private void CalculateThresholds()
    {
        lowHypeThreshold = MinigameStatusManager.Instance.maxHype * (lowHypeThresholdPercent / 100f);
        highHypeThreshold = MinigameStatusManager.Instance.maxHype * (highHypeThresholdPercent / 100f);
        lowComfortThreshold = MinigameStatusManager.Instance.maxComfort * (lowComfortThresholdPercent / 100f);
        highComfortThreshold = MinigameStatusManager.Instance.maxComfort * (highComfortThresholdPercent / 100f);
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
    }


    private IEnumerator ManageAudienceMembers()
    {
        while (true)
        {
            yield return new WaitForSeconds(audienceChangeInterval);
            UpdateAudienceState();
            MoodEvent.HypeAndComfortChange(this.currentHypeState, this.currentComfortState);

            if (currentHypeState == AudienceHypeState.HighHype && audienceMembers.Count < 20)
            {
                Debug.Log("Gained New Audience Member");
                AddAudienceMember();
            }
            else if (currentComfortState == AudienceComfortState.LowComfort && audienceMembers.Count > 5)
            {
                Debug.Log("Lost an Audience Member");
                RemoveAudienceMember();
            }
        }
    }

    private IEnumerator RandomizeAudienceMoods()
    {
        while (true)
        {
            yield return new WaitForSeconds(moodRandomizationInterval);

            int numberOfMembersToRandomize = Mathf.CeilToInt(audienceMembers.Count * 0.2f);
            List<int> alreadyRandomizedIndices = new List<int>();

            for (int i = 0; i < numberOfMembersToRandomize; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, audienceMembers.Count);
                } 
                while (alreadyRandomizedIndices.Contains(randomIndex));

                alreadyRandomizedIndices.Add(randomIndex);
                audienceMembers[randomIndex].RandomizeMood();
            }
        }
    }


    private void AddAudienceMember()
    {
        if (audienceMemberPrefabs.Count == 0)
        {
            Debug.LogError("No audience member prefabs assigned");
            return;
        }

        int randomIndex = Random.Range(0, audienceMemberPrefabs.Count);
        GameObject newMemberObj = Instantiate(audienceMemberPrefabs[randomIndex], audienceSpawnPoint.position, Quaternion.identity);
        AudienceMember newMember = newMemberObj.GetComponent<AudienceMember>();
        if (newMember != null)
        {
            newMember.Init(this);
            audienceMembers.Add(newMember);
        }
        newMember.EnterConcert();
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
                CalculateThresholds();
                ManageAudienceMembersCoroutine = StartCoroutine(ManageAudienceMembers());
                RandomizeAudienceMoodsCoroutine = StartCoroutine(RandomizeAudienceMoods());
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
                if(RandomizeAudienceMoodsCoroutine != null)
                {
                    StopCoroutine(RandomizeAudienceMoodsCoroutine);
                }
                break;
            default:
                break;
        }
    }
}
