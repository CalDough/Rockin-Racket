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
}
