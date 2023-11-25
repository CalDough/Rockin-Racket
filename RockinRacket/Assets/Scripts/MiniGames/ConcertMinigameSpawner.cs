using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ConcertMinigameSpawner : MonoBehaviour
{
    public BandRoleName bandName = BandRoleName.Default;

    public int TimesCompleted = 0;
    public int TimesFailed = 0;
    public int TimesMissed = 0;
    public bool MinigameIsActive = false;
    public bool MinigameIsOnCooldown = false;

    [Header("Default Values")]
    public float defaultCooldownDuration = 20f;
    public float defaultChanceToOccur = 0.10f;
    public float defaultChanceIncrease = 0.05f;
    public float defaultChanceTimer = 5f;
    public float defaultCountDown = 25;

    [Header("Current Values")]
    public float currentCooldown;
    public float currentChanceToOccur;
    public float chanceIncrease;
    public float chanceTimer;
    public float countDown;

    [Header("Refs")]
    public BandAnimationController bandAnimationController;
    public Image radialTimerImage;
    public GameObject MinigameContainer;
    [SerializeField] private MiniGame MiniGamePrefab;
    [SerializeField] private MiniGame SpawnedMiniGame;
    [SerializeField] private GameObject button;



    private Coroutine randomChanceCoroutine;
    private Coroutine countdownCoroutine;

    private IEnumerator RandomChanceRoutine()
    {
        while (true)
        {
            if (MinigameIsOnCooldown)
            {
                yield return new WaitForSeconds(currentCooldown);
                MinigameIsOnCooldown = false;
            }

            yield return new WaitForSeconds(chanceTimer);
            if (!MinigameIsActive && Random.Range(0f, 1f) < currentChanceToOccur)
            {
                Debug.Log("Minigame Available "+ bandName.ToString());
                
                StartCountdownCoroutine();
                currentChanceToOccur = defaultChanceToOccur; 
                MinigameIsActive = true;
                MinigameIsOnCooldown = true;

            }
            else
            {
                currentChanceToOccur = Mathf.Min(1f, currentChanceToOccur + chanceIncrease);
            }
        }
    }

    private IEnumerator TimerCountdownRoutine()
    {
        if(StateManager.Instance.CurrentState.stateType != StateType.Song)
        {
            StopCountdownCoroutine();
            yield return null;
        }
        StopRandomChanceCoroutine();
        radialTimerImage.fillAmount = 0;
        radialTimerImage.transform.parent.gameObject.SetActive(true);
        button.SetActive(true);
        bandAnimationController.PlayProblemParticles();

        float timer = countDown;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateRadialTimer(timer / countDown);
            yield return null;
        }

        FailMiniGame(); 
        Debug.Log("Failed Countdown timer! "+ bandName.ToString());
        StopCountdownCoroutine();
    }


    private void ResetValuesToDefault()
    {
        currentCooldown = defaultCooldownDuration;
        currentChanceToOccur = defaultChanceToOccur;
        chanceIncrease = defaultChanceIncrease;
        chanceTimer = defaultChanceTimer;
        MinigameIsOnCooldown = true;
        countDown = defaultCountDown;
        button.SetActive(false);        
        radialTimerImage.fillAmount = 0;
        radialTimerImage.transform.parent.gameObject.SetActive(false);
        MinigameIsActive = false;
    }

    public void StartRandomChanceCoroutine()
    {
        Debug.Log("starting minigame coroutine");
        if(randomChanceCoroutine == null)
        {randomChanceCoroutine = StartCoroutine(RandomChanceRoutine());}
    }

    public void StopRandomChanceCoroutine()
    {
        if(randomChanceCoroutine != null)
        {StopCoroutine(randomChanceCoroutine);}
        randomChanceCoroutine = null;
    }

    public void StartCountdownCoroutine()
    {
        if(countdownCoroutine == null)
        { countdownCoroutine = StartCoroutine(TimerCountdownRoutine());}
    }
    
    public void StopCountdownCoroutine()
    {
        if(countdownCoroutine != null)
        {StopCoroutine(countdownCoroutine);}
        countdownCoroutine = null;
    }

    public void OnMinigameButtonClicked()
    {
        Debug.Log("Minigame Trying to be clicked "+ bandName.ToString());
        if(!SpawnedMiniGame)
        {return;}
        radialTimerImage.fillAmount = 0;
        radialTimerImage.transform.parent.gameObject.SetActive(false);
        ActivateOrOpenSpawnedMiniGame();
    }

    public void ActivateOrOpenSpawnedMiniGame()
    {
        Debug.Log("Minigame Trying to open "+ bandName.ToString());
        if(!SpawnedMiniGame)
        { return;}
        if(SpawnedMiniGame.isActiveEvent)
        { 
            SpawnedMiniGame.OpenEvent();
        }
        else if(!SpawnedMiniGame.isActiveEvent)
        { 
            SpawnedMiniGame.Activate();
            SpawnedMiniGame.OpenEvent();
        }
    }

    private void SpawnMiniGame()
    {
        if(MiniGamePrefab && MinigameContainer)
        {
            MiniGame newGame = Instantiate(MiniGamePrefab);
            newGame.transform.SetParent(MinigameContainer.gameObject.transform, false);
            SpawnedMiniGame = newGame;
        }
    }

    private void FailMiniGame()
    {
        if (SpawnedMiniGame == null)
        {return;}
        SpawnedMiniGame.Activate();
        SpawnedMiniGame.End();
        MinigameIsActive = false;
        radialTimerImage.fillAmount = 0;
        radialTimerImage.transform.parent.gameObject.SetActive(false);
        button.SetActive(false);
        StartRandomChanceCoroutine();
        ResetValuesToDefault();
    }

    private void UpdateRadialTimer(float progress)
    {
        radialTimerImage.fillAmount = 1 - progress; 
    }


    private void Start()
    {
        ResetValuesToDefault();
        SubscribeEvents();
        SpawnMiniGame();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameEvents.OnEventStart += HandleEventStart;
        GameEvents.OnEventFail += HandleEventFail;
        GameEvents.OnEventCancel += HandleEventCancel;
        GameEvents.OnEventComplete += HandleEventComplete;
        GameEvents.OnEventMiss += HandleEventMiss;
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnEventStart -= HandleEventStart;
        GameEvents.OnEventFail -= HandleEventFail;
        GameEvents.OnEventCancel -= HandleEventCancel;
        GameEvents.OnEventComplete -= HandleEventComplete;
        GameEvents.OnEventMiss -= HandleEventMiss;
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }
    
    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                StartRandomChanceCoroutine();
                break;
            case StateType.Intermission:
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
                StopRandomChanceCoroutine();
                StopCountdownCoroutine();
                ResetValuesToDefault();
                break;
            default:
                break;
        }
    }

    void HandleEventStart(object sender, GameEventArgs e)
    {
        
    }


    void HandleEventComplete(object sender, GameEventArgs e)
    {
        if(e.EventObject == this.SpawnedMiniGame)
        {
            this.TimesCompleted++;
            ResetValuesToDefault();
            StartRandomChanceCoroutine();
        }
    }

    void HandleEventFail(object sender, GameEventArgs e)
    {
        if(e.EventObject == this.SpawnedMiniGame)
        {
            this.TimesFailed++;
            ResetValuesToDefault();
            StartRandomChanceCoroutine();
        }
        
    }

    void HandleEventMiss(object sender, GameEventArgs e)
    {
        if(e.EventObject == this.SpawnedMiniGame)
        {
            this.TimesMissed++;
            ResetValuesToDefault();
            StartRandomChanceCoroutine();
        }
    }

    void HandleEventCancel(object sender, GameEventArgs e)
    {
        if(e.EventObject == this.SpawnedMiniGame)
        {
            ResetValuesToDefault();
            StartRandomChanceCoroutine();
        }
    }

}
