using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameOwner : MonoBehaviour
{
    public string OwnerName = "";

    [Header("Default Settings")]
    public float defaultCooldownDuration = 12f;

    [Header("Current Settings")]
    public float currentCooldownDuration;

    public bool isOnCooldown = false;
    public int TimesCompleted = 0;
    public int TimesFailed = 0;
    
    public bool IsAvailable = false;

    [Header("Attempt Settings")]
    public bool useAttempts = false;
    public int maxAttempts = 5;
    private int attempts = 0;

    [SerializeField] private MiniGame AvailableMiniGame;  

    private void Start()
    {
        ResetToDefault();
        CheckInventory();
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
        
        IsAvailable = MinigameStatusManager.Instance.IsMinigameAvailable(AvailableMiniGame);
    }

    private void CheckInventory()
    {
        // Will make a call to inventory to check for new stats for cooldowns and such
    }

    public void BeginCooldowns()
    {
        StartCoroutine(CooldownRoutine());
    }

    public void EndCooldowns()
    {
        StopAllCoroutines();
        isOnCooldown = false;
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(currentCooldownDuration);
        isOnCooldown = false;
    }

    private void ResetToDefault()
    {
        currentCooldownDuration = defaultCooldownDuration;
        attempts = 0;

        if(AvailableMiniGame)
        {
            AvailableMiniGame.RestartMiniGameLogic();
        }
    }

    private void ActivateMiniGame()
    {
        if (useAttempts && attempts >= maxAttempts)
        {
            Debug.Log("Max attempts reached!");
            return;
        }

        // Logic for starting the mini-game goes here
        ResetToDefault();
        AvailableMiniGame.OpenEvent();

        if (useAttempts)
        {
            attempts++;
        }

        BeginCooldowns();
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Debug.Log("State Started: " + e.stateType);
        switch(e.stateType)
        {
            case GameModeType.Song:
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
