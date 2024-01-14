using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameQueue : MonoBehaviour
{
    public static MinigameQueue Instance { get; private set; }

    [SerializeField] private List<GameObject> minigamePrefabs;



    public int MaxActiveMinigames = 1;
    [SerializeField] private Queue<MinigameController> minigameQueue = new Queue<MinigameController>();
    [SerializeField] private int activeMinigamesCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {Instance = this;}
        else
        {Destroy(gameObject);}
    }

    void Start()
    {
        GetMinigamesFromShop();
        InstantiateMinigames();
    }


    private void Update()
    {
        if (minigameQueue.Count > 0 && activeMinigamesCount < MaxActiveMinigames)
        {
            var minigame = minigameQueue.Dequeue();
            if (minigame.CanActivate)
            {
                minigame.MakeMinigameAvailable();
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

    public void GetMinigamesFromShop()
    {
        Debug.Log("No Shop Minigame Behavior Added Yet");
    }

    private void InstantiateMinigames()
    {
        Debug.Log("Spawning All Minigames");
        foreach (var prefab in minigamePrefabs)
        {
            GameObject minigameObject = Instantiate(prefab);
            MinigameController minigameController = minigameObject.GetComponent<MinigameController>();
        }
    }

    private void OnEnable()
    {
        MinigameEvents.OnMinigameComplete += OnMinigameComplete;
        MinigameEvents.OnMinigameFail += OnMinigameComplete;
        MinigameEvents.OnMinigameCancel += OnMinigameComplete;
    }

    private void OnDisable()
    {
        MinigameEvents.OnMinigameComplete -= OnMinigameComplete;
        MinigameEvents.OnMinigameFail -= OnMinigameComplete;
        MinigameEvents.OnMinigameCancel -= OnMinigameComplete;
    }
}