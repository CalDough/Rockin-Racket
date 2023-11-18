using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

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
    [Tooltip("X is minimum number of audience members, Y is maximum number")]
    [SerializeField] Vector2 audienceSpawningRange;

    [SerializeField] private float moodRandomizationInterval = 15f;

    [SerializeField] public AudienceHypeState currentHypeState;
    [SerializeField] public AudienceComfortState currentComfortState;
    
    private Coroutine ManageAudienceMembersCoroutine;
    
    private Coroutine RandomizeAudienceMoodsCoroutine;

    [SerializeField] private float lowHypeThreshold;
    [SerializeField] private float highHypeThreshold;
    [SerializeField] private float lowComfortThreshold;
    [SerializeField] private float highComfortThreshold;

    [SerializeField] private Transform audienceHolder;

    public string cheerSoundEvent = "";
    public string booSoundEvent = "";
    public float soundStartVolume = 1;

    [SerializeField] private float cheerCooldown = 10f;
    [SerializeField] private float booCooldown = 10f;
    private float lastCheerTime = -10f; 
    private float lastBooTime = -10f;

    public void PlayBooSound()
    {
        Debug.Log("Playing Boo");
        if (!string.IsNullOrEmpty(booSoundEvent) && Time.time - lastBooTime > booCooldown)
        {
            lastBooTime = Time.time;
            StartCoroutine(PlaySoundWithFadeOut(booSoundEvent, 3f, 2f));
        }
    }

    public void PlayBooReactionSound()
    {
        if (!string.IsNullOrEmpty(booSoundEvent))
        {
            lastBooTime = Time.time;
            StartCoroutine(PlaySoundWithFadeOut(booSoundEvent, 2f, 2f));
        }
    }
    public void PlayCheerSound()
    {
        Debug.Log("Playing Cheer");
        if (!string.IsNullOrEmpty(cheerSoundEvent) && Time.time - lastCheerTime > cheerCooldown)
        {
            lastCheerTime = Time.time;
            StartCoroutine(PlaySoundWithFadeOut(cheerSoundEvent, 3f, 2f)); 
        }
    }

    public void PlayCheerReactionSound()
    {
        if (!string.IsNullOrEmpty(cheerSoundEvent))
        {
            
            StartCoroutine(PlaySoundWithFadeOut(cheerSoundEvent, 2f, 2f)); 
        }
    }

    private IEnumerator PlaySoundWithFadeOut(string soundEventPath, float playDuration, float fadeOutDuration)
    {

        FMOD.Studio.EventInstance soundInstance = FMODUnity.RuntimeManager.CreateInstance(soundEventPath);
        soundInstance.start();

        yield return new WaitForSeconds(playDuration);

        float elapsedTime = 0f;
        float startVolume = soundStartVolume;
        soundInstance.setVolume(soundStartVolume);

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeOutDuration);
            soundInstance.setVolume(newVolume);
            yield return null;
        }

        soundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundInstance.release();
    }
    
    void Start()
    {
        SubscribeEvents();

        if (audienceRows.Count == 0)
        {
            Debug.LogError("No audience rows assigned");
            return;
        }
        InitializeAudience();
    
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

            if(currentComfortState == AudienceComfortState.LowComfort)
            {
                PlayBooSound();
            }
            else if(currentHypeState == AudienceHypeState.HighHype)
            {
                PlayCheerSound();
            }

            if ((currentComfortState == AudienceComfortState.MidComfort || currentComfortState == AudienceComfortState.HighComfort)   
                && currentHypeState == AudienceHypeState.HighHype && audienceMembers.Count < 20)
            {
                Debug.Log("Gained New Audience Member");
                AddAudienceMember();
            }
            else if (currentComfortState == AudienceComfortState.LowComfort && audienceMembers.Count > 4)
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

    private void InitializeAudience()
    {
        int membersToSpawn = (int) Random.Range(audienceSpawningRange.x, audienceSpawningRange.y);
        Debug.Log(membersToSpawn);

        for (int i = 0; i < membersToSpawn; i++)
        {
            AddAudienceMember();
        }
        //if (audienceMembers.Count < 5)
        //{
        //    int membersToSpawn = 5 - audienceMembers.Count;

        //    for (int i = 0; i < membersToSpawn; i++)
        //    {
        //        AddAudienceMember();
        //    }
        //}
    }

    private void AddAudienceMember()
    {
        if (audienceMemberPrefabs.Count == 0)
        {
            Debug.LogError("No audience member prefabs assigned");
            return;
        }

        int randomIndex = Random.Range(0, audienceMemberPrefabs.Count);
        Quaternion audienceHolderRotation = audienceHolder.rotation;
        GameObject newMemberObj = Instantiate(audienceMemberPrefabs[randomIndex], audienceSpawnPoint.position, audienceHolderRotation, audienceHolder);
        AudienceMember newMember = newMemberObj.GetComponent<AudienceMember>();
        
        if (newMember != null)
        {
            audienceMembers.Add(newMember);
        }
        else
        {
            Debug.LogError("The spawned object does not have an AudienceMember component!");
        }

        newMember.Init(this);
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
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }

    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.stateType)
        {
            case StateType.Song:
                CalculateThresholds();
                ManageAudienceMembersCoroutine = StartCoroutine(ManageAudienceMembers());
                RandomizeAudienceMoodsCoroutine = StartCoroutine(RandomizeAudienceMoods());
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        switch(e.stateType)
        {
            case StateType.Song:
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
