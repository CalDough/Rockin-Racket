using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameController : MonoBehaviour
{
    public bool CanActivate { get; set; }
    public bool IsActive { get; set; }

    public float spawnTimerDuration = 10f; 
    public float spawnTimerRemainingDuration = 0f; 

    public float gameplayTimerDuration = 10f; 
    public float gameplayRemainingDuration = 0f; 

    protected private Coroutine spawnTimerCoroutine;
    protected private Coroutine gameplayTimerCoroutine;

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

    public IEnumerator SpawnTimerCoroutine()
    {
        while (spawnTimerRemainingDuration > 0)
        {
            spawnTimerRemainingDuration -= Time.deltaTime;
            yield return null;
        }
        CanActivate = true;
        MinigameQueue.Instance.TryActivateMinigame(this);
    }

    public IEnumerator GameplayTimerCoroutine()
    {
        while (gameplayRemainingDuration > 0)
        {
            gameplayRemainingDuration -= Time.deltaTime;
            yield return null;
        }
        // gameplay timer logic
        FailMinigame();
    }

    public abstract void StartMinigame();

    public abstract void FailMinigame();

    public abstract void FinishMinigame();

    public abstract void CancelMinigame();

    public abstract void OpenMinigame();


    public abstract void CloseMinigame();

}