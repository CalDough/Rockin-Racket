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

    [SerializeField] private MinigameContainer MiniGames;  
    [SerializeField] private List<MiniGame> SpawnedMiniGames;  

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
    }

    private void SpawnMiniGame()
    {
        
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
