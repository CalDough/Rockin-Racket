using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MicNoteHelping : MiniGame
{
    [Header("UI Elements")]
    [SerializeField] GameObject PlayerCircle;
    [SerializeField] GameObject singingNote;
    [SerializeField] RectTransform[] spawnPoints;
    [SerializeField] RectTransform[] endPoints;
    [Header("Customization Settings")]
    [SerializeField] int numberOfNotes;
    [SerializeField] int moveDistance;

    private Queue<RectTransform> spawnQueue;
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
        Debug.Log("Populating spawn queue");
        spawnQueue = new Queue<RectTransform>();

        for (int i = 0; i < numberOfNotes; i++)
        {
            spawnQueue.Enqueue(spawnPoints[Random.Range(0, spawnPoints.Length)]);
        }
    }

    private void DisplayNotes()
    {
        if (!begunSpawning)
        {
            Debug.Log("Begun spawning notes");
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
            RectTransform spawnTransform = spawnQueue.Dequeue();
            GameObject note = Instantiate(singingNote, spawnTransform.transform);
            note.gameObject.GetComponent<SingingNote>().destination = new Vector3(spawnTransform.position.x - moveDistance, spawnTransform.position.y, 0);
            Debug.Log("Spawned one note");
            counter--;
        }
        Debug.Log("Note spawning complete");
    }

    public override void Activate()
    {
        Debug.Log("Event activated");
        base.Activate();
        PopulateSpawnQueue();
        isActive = true;
    }

    public override void Complete()
    {
        Debug.Log("Event complete");
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
