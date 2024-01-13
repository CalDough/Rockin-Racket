using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameQueue : MonoBehaviour
{
    public static MinigameQueue Instance { get; private set; }

    public int MaxActiveMinigames = 1;
    private Queue<MinigameController> minigameQueue = new Queue<MinigameController>();
    private int activeMinigamesCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {Instance = this;}
        else
        {Destroy(gameObject);}
    }

    private void Update()
    {
        if (minigameQueue.Count > 0 && activeMinigamesCount < MaxActiveMinigames)
        {
            var minigame = minigameQueue.Dequeue();
            if (minigame.CanActivate)
            {
                minigame.StartMinigame();
                activeMinigamesCount++;
            }
        }
    }

    public void TryActivateMinigame(MinigameController minigame)
    {
        if (!minigame.IsActive && minigame.CanActivate)
        {
            minigameQueue.Enqueue(minigame);
        }
    }

    private void OnMinigameComplete(object sender, GameEventArgs e)
    {
        activeMinigamesCount--;
        
        if(activeMinigamesCount <= 0)
        {activeMinigamesCount = 0;}
    }

    private void OnEnable()
    {
        MinigameEvents.OnMinigameComplete += OnMinigameComplete;
    }

    private void OnDisable()
    {
        MinigameEvents.OnMinigameComplete -= OnMinigameComplete;
    }
}