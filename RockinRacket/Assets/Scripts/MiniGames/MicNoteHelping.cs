using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicNoteHelping : MiniGame
{
    [Header("UI Elements")]
    [SerializeField] GameObject PlayerCircle;
    [SerializeField] GameObject singingNote;
    [SerializeField] Transform[] spawnPoints;
    [Header("Customization Settings")]
    [SerializeField] int numberOfNotes;

    private Queue<Transform> spawnQueue;
    private bool isActive = false;
    private bool begunSpawning = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            DisplayNotes();
        }
    }

    // This method populates the spawn queue with the list of notes that will be spawning
    private void PopulateSpawnQueue()
    {
        spawnQueue = new Queue<Transform>();

        for (int i = 0; i < numberOfNotes; i++)
        {
            spawnQueue.Enqueue(spawnPoints[Random.Range(0, spawnPoints.Length)]);
        }
    }

    private void DisplayNotes()
    {
        if (!begunSpawning)
        {
            begunSpawning = true;
            StartCoroutine(SpawnNotes(spawnQueue.Count));
        }
    }

    IEnumerator SpawnNotes(int numberOfNotes)
    {
        int counter = numberOfNotes;
        while (counter > 0)
        {
            yield return new WaitForSeconds(counter);
            Transform spawnTransform = spawnQueue.Dequeue();
            Instantiate(singingNote, spawnTransform);
            counter--;
        }
        Debug.Log("Note spawning complete");
    }

    public override void Activate()
    {
        base.Activate();
        PopulateSpawnQueue();
        isActive = true;
    }

    public override void Complete()
    {
        base.Complete();
    }

    public override void Miss()
    {
        GameEvents.EventMiss(this);
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    public override void OpenEvent()
    {
        GameEvents.EventOpened(this);
        HandleOpening();
    }
    public override void CloseEvent()
    {
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    public override void HandleOpening()
    {
        if (!IsCompleted)
        {
            Panels.SetActive(true);
        }
    }
    public override void HandleClosing()
    {
        Panels.SetActive(false);
        if (IsCompleted == false)
        {
            //RestartMiniGameLogic(); 
        }
    }
}
