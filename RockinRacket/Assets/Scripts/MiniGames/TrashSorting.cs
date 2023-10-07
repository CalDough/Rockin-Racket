using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 * This is the script for the trash sorting minigame. The associated prefab with this is called 'DraggableTrash'
 * 
 */

public class TrashSorting : MiniGame
{
    // Serialized Fields
    [Header("Object References")]
    [SerializeField] RectTransform spawnArea;
    [SerializeField] int minTrashSpawn;
    [SerializeField] int maxTrashSpawn;
    [SerializeField] GameObject[] draggableTrashPrefabs;

    // Debugging fields
    [Header("Debugging Fields")]
    [SerializeField] private int totalTrashCleaned = 0;
    [SerializeField] private int totalTrashRemaining;
    [SerializeField] private int totalTrash;


    private void Start()
    {
        DropEvents.current.e_DropEvent.AddListener(SubtractTrash);
    }

    /*
     * This method is run when the minigame is activated
     */
    public override void Activate()
    {
        isActiveEvent = true;
        remainingDuration = duration;
        if (!infiniteDuration)
        {
            remainingDuration = duration;
            durationCoroutine = StartCoroutine(EventDurationCountdown());
        }
        GameEvents.EventStart(this);
        // Calling our method to spawn the trash
        SpawnTrash();
    }

    /*
     * This method spawns the trash objects in a certain area on the screen
     */
    public void SpawnTrash()
    {
        // Calculating the total trash the player will have to clean
        totalTrash = Random.Range(minTrashSpawn, maxTrashSpawn + 1);
        totalTrashRemaining = totalTrash;

        // Looping through and spawning the number of trash prefabs as set in our
        // total trash variable
        for (int i = 0; i < totalTrash; i++)
        {
            GameObject trashPrefab = draggableTrashPrefabs[Random.Range(0, draggableTrashPrefabs.Length)];

            // Calculating the spawn position in our rectangle
            Vector3 spawnPosition = new Vector3
            (
                Random.Range((spawnArea.rect.min.x + spawnArea.rect.width / 2), spawnArea.rect.max.x),
                Random.Range(spawnArea.rect.min.y, spawnArea.rect.max.y),
                0
            );

            // adjust for panel's actual position in world space 
            spawnPosition += (Vector3)spawnArea.position;

            // Instantiating the object at our spawn position
            GameObject spawnedTrash = Instantiate(trashPrefab, spawnPosition, Quaternion.identity, spawnArea);
        }
    }

    private void SubtractTrash(int i)
    {
        if (i == 0)
        {
            Debug.Log("Trash decrementing");
            totalTrashRemaining--;

            if (totalTrashRemaining <= 0)
            {
                Complete();
                Debug.Log("Complete Trash Sorting");
            }
        }
    }

    //When the player fails to complete the event in time.
    public override void End()
    {
        isActiveEvent = false;
        if (durationCoroutine != null)
        {
            StopCoroutine(durationCoroutine);
        }
        GameEvents.EventFail(this);
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    //When the player misses the event due to Game State Change.
    public override void Miss()
    {
        isActiveEvent = false;
        if (durationCoroutine != null)
        {
            StopCoroutine(durationCoroutine);
        }
        GameEvents.EventMiss(this);
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    //When the player completes the event in time.
    public override void Complete()
    {
        isActiveEvent = false;
        if (durationCoroutine != null)
        {
            StopCoroutine(durationCoroutine);
        }
        GameEvents.EventComplete(this);
        GameEvents.EventClosed(this);
        this.IsCompleted = true;
        HandleClosing();
    }

    //Calls the event to inform the UI to open or close the game
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

        //If you want to reset the game if they did not complete it
        if (IsCompleted == false)
        { RestartMiniGameLogic(); }
    }

}
