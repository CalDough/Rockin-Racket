using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameQueue : MonoBehaviour
{
    public static MinigameQueue Instance { get; private set; }

    [SerializeField] private RectTransform parentCanvasTransform;

    //I was gonna do a list, but no point if theres only 5 specific ones
    //These will have default ones in the scene, but the prefab will be replaced by shop system
    [SerializeField] private GameObject HaleyMinigame;
    [SerializeField] private GameObject AceMinigame;
    [SerializeField] private GameObject KurtMinigame;
    [SerializeField] private GameObject MJMinigame;
    [SerializeField] private GameObject ExtraMinigame; // For specific venue unique games

    //These are the buttons that open the minigames and its 1:1 linked to the band member
    [SerializeField] private MinigameButton HaleyMinigameButton;
    [SerializeField] private MinigameButton AceMinigameButton;
    [SerializeField] private MinigameButton KurtMinigameButton;
    [SerializeField] private MinigameButton MJMinigameButton;
    [SerializeField] private MinigameButton ExtraMinigameButton;

    public int MaxActiveMinigames = 1;
    public int MinigameQueueSize = 0;
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
        MinigameQueueSize = minigameQueue.Count;
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
        AssignMinigameToButton(ref HaleyMinigame, HaleyMinigameButton);
        AssignMinigameToButton(ref AceMinigame, AceMinigameButton);
        AssignMinigameToButton(ref KurtMinigame, KurtMinigameButton);
        AssignMinigameToButton(ref MJMinigame, MJMinigameButton);
        AssignMinigameToButton(ref ExtraMinigame, ExtraMinigameButton);
    }

    private void AssignMinigameToButton(ref GameObject minigamePrefab, MinigameButton minigameButton)
    {
        if (minigamePrefab != null)
        {
            Debug.Log("Spawning Minigames");
            GameObject minigameObject = Instantiate(minigamePrefab, parentCanvasTransform);
            MinigameController minigameController = minigameObject.GetComponent<MinigameController>();
            if (minigameController != null)
            {
                minigameButton.minigameController = minigameController;
            }
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