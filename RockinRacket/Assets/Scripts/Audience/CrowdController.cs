using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    public List<CrowdMember> crowdMembers;
    [SerializeField] public int maxTrashCount = 10;
    [SerializeField] public int currentTrashCount = 0;

    [SerializeField] private float trashSpawnIntervalMin = 7f;
    [SerializeField] private float trashSpawnIntervalMax = 14f;
    [SerializeField] private float trashCreatingMembers = 3f;

    [SerializeField] private float tshirtRequestIntervalMin = 14f;
    [SerializeField] private float tshirtRequestIntervalMax = 24f;
    [SerializeField] private float tshirtRequestMembers = 3f;

    [SerializeField]private string cheerSoundEvent = "";
    [SerializeField]private string booSoundEvent = "";
    [SerializeField]private float cheerCooldown = 10f; 
    [SerializeField]private float booCooldown = 10f;
    [SerializeField]private float lastCheerTime = -10f; 
    [SerializeField]private float lastBooTime = -10f; 
    [SerializeField]private float soundStartVolume = 1;

    private Coroutine TrashCoroutine;
    private Coroutine ShirtCoroutine;

    [SerializeField] public List<float> PotentialConcertRatings;
    [SerializeField] public List<float> EarnedConcertRatings;

    public static CrowdController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        SubscribeEvents();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    void Update()
    {
        if(StateManager.Instance.CurrentState.stateType != StateType.Song)
        {
            return;
        }
        CalculateAndReactToConcertRating();
    }
    
    public void StartShirtRequests()
    {
        if(ShirtCoroutine == null)
        {
            ShirtCoroutine = StartCoroutine(ShirtRequestRoutine());
        }
    }

    public void StartTrashThrowing()
    {
        if(TrashCoroutine == null)
        {
            TrashCoroutine = StartCoroutine(TrashSpawnRoutine());
        }
    }

    public void StopShirtRequests()
    {
        if(ShirtCoroutine != null)
        {
            StopCoroutine(ShirtCoroutine);
        }
        ShirtCoroutine = null;
    }

    public void StopTrashThrowing()
    {
        if(TrashCoroutine != null)
        {
            StopCoroutine(TrashCoroutine);
        }
        TrashCoroutine = null;
    }
    
    IEnumerator TrashSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(trashSpawnIntervalMin, trashSpawnIntervalMax));

            for (int i = 0; i < trashCreatingMembers; i++)
            {
                int randomIndex = Random.Range(0, crowdMembers.Count);
                crowdMembers[randomIndex].ThrowTrash();
            }
        }
    }

    IEnumerator ShirtRequestRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(tshirtRequestIntervalMin, tshirtRequestIntervalMax));
            for (int i = 0; i < tshirtRequestMembers; i++)
            {
                int randomIndex = Random.Range(0, crowdMembers.Count);
                crowdMembers[randomIndex].StartWantingShirts();
            }
        }
    }

    private void SubscribeEvents()
    {
        GameEvents.OnEventFail += HandleEventFail;
        GameEvents.OnEventComplete += HandleEventComplete;
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnEventFail -= HandleEventFail;
        GameEvents.OnEventComplete -= HandleEventComplete;
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }

    public void UpdateCrowdMood(float amount)
    {
        foreach(CrowdMember member in crowdMembers)
        {
            member.UpdateConcertRating(amount);
        }
    }

    public bool CanSpawnTrash()
    {
        if (currentTrashCount < maxTrashCount)
        {return true;}

        return false;
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        UpdateCrowdMood(-2);
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        UpdateCrowdMood(1);
    }

    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                StartShirtRequests();
                StartTrashThrowing();
                CalculatePotentialRating();
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                StopShirtRequests();
                StopTrashThrowing();
                CalculateEarnedRating();
                break;
            default:
                break;
        }
    }

    private void CalculatePotentialRating()
    {
        float potentialRating = crowdMembers.Count * 10;
        PotentialConcertRatings.Add(potentialRating);
    }

    private void CalculateEarnedRating()
    {
        UpdateCrowdMood(-0.5f * currentTrashCount);

        float earnedRating = 0;
        foreach(CrowdMember member in crowdMembers)
        {
            earnedRating += member.GetConcertRating();
        }

        EarnedConcertRatings.Add(earnedRating);
    }

    private void CalculateAndReactToConcertRating()
    {
        float averageConcertRating = CalculateAverageConcertRating();
        
        if (averageConcertRating <= 2 && Time.time - lastBooTime > booCooldown)
        {
            PlayBooSound();
            lastBooTime = Time.time;
        }
        else if (averageConcertRating >= 7 && Time.time - lastCheerTime > cheerCooldown)
        {
            PlayCheerSound();
            lastCheerTime = Time.time;
        }
    }

    private float CalculateAverageConcertRating()
    {
        float totalRating = 0;
        foreach (CrowdMember member in crowdMembers)
        {
            totalRating += member.GetConcertRating();
        }
        return totalRating / crowdMembers.Count;
    }


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
    
}
