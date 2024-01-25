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

    //These are the buttons that open the minigames and its 1:1 linked to the band member/game
    //Extra game is meant for future expansion such as speakers or harvey
    [SerializeField] private MinigameButton HaleyMinigameButton;
    [SerializeField] private MinigameButton AceMinigameButton;
    [SerializeField] private MinigameButton KurtMinigameButton;
    [SerializeField] private MinigameButton MJMinigameButton;
    [SerializeField] private MinigameButton ExtraMinigameButton;

    public int MaxActiveMinigames = 1;
    public int MinigameQueueSize = 0;
    [SerializeField] private Queue<MinigameController> minigameQueue = new Queue<MinigameController>();
    [SerializeField] private int activeMinigamesCount = 0;
    [SerializeField] private float delayBetweenMinigames = 4.5f; 
    [SerializeField] private float nextMinigameActivationTime = 0f; // Helper var for time

    private void Awake()
    {
        if (Instance == null)
        {Instance = this;}
        else
        {Destroy(gameObject);}
    }

    void Start()
    {
        //SetupMinigames();
        ConcertEvents.instance.e_ConcertStarted.AddListener(SetupMinigames);
    }

    public void SetupMinigames()
    {
        GetMinigamesFromShop();
        InstantiateMinigames();
    }

    private void Update()
    {
        MinigameQueueSize = minigameQueue.Count;
        if (minigameQueue.Count > 0 && activeMinigamesCount < MaxActiveMinigames && Time.time >= nextMinigameActivationTime)
        {
            ActivateNextMinigame();
        }
    }

    private void ActivateNextMinigame()
    {
        var minigame = minigameQueue.Dequeue();
        if (minigame.CanActivate)
        {
            Debug.Log("QUEUE: Minigame dequeued and available " + minigame.name);
            minigame.MakeMinigameAvailable();
            activeMinigamesCount++;
        }
    }

    public void TryActivateMinigame(MinigameController minigame)
    {
        if (!minigame.IsActive && minigame.CanActivate)
        {
            Debug.Log("QUEUE: Minigame Added to queue " + minigame.name);
            minigameQueue.Enqueue(minigame);
        }
    }

    private void OnMinigameComplete(object sender, GameEventArgs e)
    {
        activeMinigamesCount--;
        Debug.Log("QUEUE: Minigame ended " + e.EventObject.name);

        nextMinigameActivationTime = Time.time + delayBetweenMinigames;

        if (activeMinigamesCount <= 0)
        {
            activeMinigamesCount = 0;
        }
    }

    public void GetMinigamesFromShop()
    {
        
        if(ItemInventory.GetBandmateMinigame(Bandmate.Haley) != null)
        {
            HaleyMinigame = ItemInventory.GetBandmateMinigame(Bandmate.Haley);
            Debug.Log("QUEUE: Shop Minigame Found "+ HaleyMinigame.name);
        }
        else
        {
            Debug.Log("QUEUE: No Shop Minigame Found for Haley");
        }

        if(ItemInventory.GetBandmateMinigame(Bandmate.Ace) != null)
        {
            AceMinigame = ItemInventory.GetBandmateMinigame(Bandmate.Ace);
            Debug.Log("QUEUE: Shop Minigame Found "+ AceMinigame.name);
        }
        else
        {
            Debug.Log("QUEUE: No Shop Minigame Found for Ace");
        }

        if(ItemInventory.GetBandmateMinigame(Bandmate.MJ) != null)
        {
            MJMinigame = ItemInventory.GetBandmateMinigame(Bandmate.MJ);
            Debug.Log("QUEUE: Shop Minigame Found "+ MJMinigame.name);
        }
        else
        {
            Debug.Log("QUEUE: No Shop Minigame Found for MJ");
        }

        if(ItemInventory.GetBandmateMinigame(Bandmate.Kurt) != null)
        { 
            KurtMinigame = ItemInventory.GetBandmateMinigame(Bandmate.Kurt);
            Debug.Log("QUEUE: Shop Minigame Found "+ KurtMinigame.name);
        }
        else
        {
            Debug.Log("QUEUE: No Shop Minigame Found for Kurt");
        }
        

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
            Debug.Log("QUEUE: Spawning Minigames");
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