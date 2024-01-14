using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameController : MonoBehaviour
{
    [field:SerializeField] public bool CanActivate { get; set; }
    [field:SerializeField] public bool IsActive { get; set; }

    //Variables for how long between adding the game back to queue
    public float spawnTimerDuration = 10f; 
    public float spawnTimerRemainingDuration = 0f; 

    //Variables for how long the player can have the game open
    public float gameplayTimerDuration = 10f; 
    public float gameplayRemainingDuration = 0f; 

    //Variables for how long the player has to open the game
    public float availabilityTimerDuration = 10f; 
    public float availabilityRemainingDuration = 0f; 

    protected private Coroutine spawnTimerCoroutine;
    protected private Coroutine gameplayTimerCoroutine;
    protected private Coroutine availabilityTimerCoroutine;

    // Could probably refactor this to not use 3 co-routines but it makes the logic slightly easier than some weirdo function
    public void ResetSpawnTimer()
    {
        spawnTimerRemainingDuration = spawnTimerDuration;
        if (spawnTimerCoroutine != null)
        {
            StopCoroutine(spawnTimerCoroutine);
        }
        spawnTimerCoroutine = StartCoroutine(SpawnTimerCoroutine());
    }

    public void ResetGameplayTimer()
    {
        gameplayRemainingDuration = gameplayTimerDuration;
        if (gameplayTimerCoroutine != null)
        {
            StopCoroutine(gameplayTimerCoroutine);
        }
        gameplayTimerCoroutine = StartCoroutine(GameplayTimerCoroutine());
    }

    public void RestartAvailabilityGameplayTimer()
    {
        availabilityRemainingDuration = availabilityTimerDuration;
        if (availabilityTimerCoroutine != null)
        {
            StopCoroutine(availabilityTimerCoroutine);
        }
        availabilityTimerCoroutine = StartCoroutine(AvailabilityTimerCoroutine());
    }

    public IEnumerator SpawnTimerCoroutine()
    {
        while (spawnTimerRemainingDuration > 0)
        {
            spawnTimerRemainingDuration -= Time.deltaTime;
            yield return null;
        }
        CanActivate = true;
        // The game is no longer on cooldown, so it will add itself to the minigame queue
        MinigameQueue.Instance.TryActivateMinigame(this);
    }

    public IEnumerator GameplayTimerCoroutine()
    {
        while (gameplayRemainingDuration > 0)
        {
            gameplayRemainingDuration -= Time.deltaTime;
            yield return null;
        }
        // Gameplay timer logic
        // If the player took too long during the gameplay, the game closes and they fail
        Debug.Log("MGC: Minigame Failed, opened and took too long: " + this.name);
        FailMinigame();
    }

    public IEnumerator AvailabilityTimerCoroutine()
    {
        while (availabilityRemainingDuration > 0)
        {
            availabilityRemainingDuration -= Time.deltaTime;
            yield return null;
        }
        // Availability timer logic
        CanActivate = false;
        // If the game was never opened (active) then the player missed it so they fail it
        if(!IsActive)
        {
            Debug.Log("MGC: Minigame Failed, never opened " + this.name);
            FailMinigame();
        }
    }

    public void StopGameplayTimer()
    {
        if(gameplayTimerCoroutine != null)
        {
            StopCoroutine(gameplayTimerCoroutine);
        }
    }

    public void StopAvailabilityTimer()
    {
        if(availabilityTimerCoroutine != null)
        {
            StopCoroutine(availabilityTimerCoroutine);
        }
    }

    public void StopSpawnTimer()
    {
        if(spawnTimerCoroutine != null)
        {
            StopCoroutine(spawnTimerCoroutine);
        }
    }

    public virtual void MakeMinigameAvailable()
    {
        CanActivate = true;
        RestartAvailabilityGameplayTimer();
        StopSpawnTimer();
        StopGameplayTimer();
        MinigameEvents.EventAvailable(this);
        Debug.Log("MGC: Minigame " + this.name + " now available for clicking");

    }

    public abstract void StartMinigame();

    public abstract void FailMinigame();

    public abstract void FinishMinigame();

    public abstract void CancelMinigame();

    public abstract void OpenMinigame();

    public abstract void CloseMinigame();

}