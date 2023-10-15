using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceMinigameOwner : MonoBehaviour
{
    public string OwnerName = "";

    [Header("Default Settings")]
    public float defaultCooldownDuration = 12f;
    public float defaultChanceToOccur = 0.50f;
    public float defaultChanceIncrease = 0.05f;
    public float defaultChanceTimer = 1f;

    [Header("Current Settings")]
    public float currentCooldownDuration;
    public float currentChanceToOccur;
    public float chanceIncrease;
    public float chanceTimer;

    public bool isOnCooldown = false;

    [SerializeField] private MinigameContainer MiniGames;  //we randomly take from a pool of available minigames
    [SerializeField] private List<MiniGame> SpawnedMiniGames;  //old spawned minigames
    [SerializeField] private MiniGame AvailableMiniGame;  //current minigame that is spawned

    private void Start()
    {
        ResetToDefault();
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    private void CheckInventory()
    {
        ResetToDefault();
        // Will make a call to inventory to check for new stats
    }

    public void BeginCooldowns()
    {
        StartCoroutine(CheckOccurChanceRoutine());
    }

    public void EndCooldowns()
    {
        StopCoroutine(CheckOccurChanceRoutine());
    }

    public void ActivateMiniGame()
    {
        if(GameStateManager.Instance.CurrentGameState.GameType != GameModeType.Song)
        {
            Debug.Log("Not a song right now");
            return;
        }
        if(MinigameStatusManager.Instance.OpenedMiniGame != null)
        {
            Debug.Log("Another game is opened");
            return;
        }
        // Logic for starting the mini-game goes here
        if(AvailableMiniGame)
        {
            AvailableMiniGame.IsCompleted = false;
            if(AvailableMiniGame.IsCompleted)
            {
                
                AvailableMiniGame.RestartMiniGameLogic();
            }
            else
            {
                AvailableMiniGame.Activate();
            }
        }
        ResetToDefault();
        AvailableMiniGame.OpenEvent();


        BeginCooldowns();
    }

    private IEnumerator CheckOccurChanceRoutine()
    {
        while (true)
        {
            if (!isOnCooldown)
            {
                yield return new WaitForSeconds(chanceTimer);
                float randomValue = Random.value;
                if (randomValue <= currentChanceToOccur)
                {
                    SpawnMiniGame();
                    StartCooldown();
                }
                else
                {
                    IncreaseOccurChance();
                }
            }
            else
            {
                yield return new WaitForSeconds(currentCooldownDuration);
                isOnCooldown = false;
                ResetToDefault();
            }
        }
    }

    private void StartCooldown()
    {
        isOnCooldown = true;
    }

    private void IncreaseOccurChance()
    {
        currentChanceToOccur += chanceIncrease;
    }

    private void ResetToDefault()
    {
        currentChanceToOccur = defaultChanceToOccur;
        chanceIncrease = defaultChanceIncrease;
        chanceTimer = defaultChanceTimer;
        if(AvailableMiniGame)
        {
            AvailableMiniGame.CloseEvent();
        }
    }

    private void SpawnMiniGame()
    {
        if(AvailableMiniGame)
        {
            AvailableMiniGame.CloseEvent();
        }
        // Logic for starting the mini-game goes here
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                BeginCooldowns();
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        //Debug.Log("Game state ended: " + e.state.GameType);
        switch(e.stateType)
        {
            case GameModeType.Song:
                EndCooldowns();
                break;
            default:
                break;
        }
    }


}
