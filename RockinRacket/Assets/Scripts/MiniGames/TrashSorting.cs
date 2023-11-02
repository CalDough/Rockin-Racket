using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
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
    public GameObject[] trashPrefabs; // Drag your trash prefabs here in the Unity inspector
    public RectTransform[] dumpsters;
    [SerializeField] Transform trashParentTransform;
    private List<GameObject> spawnedTrashItems = new List<GameObject>();

    // Debugging fields
    [Header("Debugging Fields")]
    [SerializeField] private int totalTrash;
    [SerializeField] private int sortedTrash;

    public string trashCleanedSoundEvent = "";

    public float soundStartVolume = 1;

    public void PlaySound()
    {
        if (!string.IsNullOrEmpty(trashCleanedSoundEvent))
        {
            FMOD.Studio.EventInstance soundInstance = RuntimeManager.CreateInstance(trashCleanedSoundEvent);
            soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
            soundInstance.setVolume(soundStartVolume);
            soundInstance.start();
            soundInstance.release();
        }
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
        totalTrash = Random.Range(minTrashSpawn, maxTrashSpawn);

        // Looping through and spawning the number of trash prefabs as set in our
        // total trash variable
        for (int i = 0; i < totalTrash; i++)
        {
            GameObject chosenPrefab = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
            DraggableTrash draggableComponent = chosenPrefab.GetComponent<DraggableTrash>();
            
            // If the chosen trash prefab doesn't have a DraggableTrash component, skip
            if(draggableComponent == null) 
            {continue;}

            GameObject trashInstance = Instantiate(chosenPrefab);
            DraggableTrash trashScript = trashInstance.GetComponent<DraggableTrash>();
            
            trashScript.trashSorting = this; 
            if(trashParentTransform != null)
            {
                trashInstance.transform.SetParent(trashParentTransform, false);
            }
            else
            {
                trashInstance.transform.SetParent(transform, false);
            } 

            // Set position within spawnArea
            Vector3 randomPosWithinArea = new Vector3(
                Random.Range(spawnArea.rect.xMin, spawnArea.rect.xMax),
                Random.Range(spawnArea.rect.yMin, spawnArea.rect.yMax),
                0 
            );
            spawnedTrashItems.Add(trashInstance);
            trashInstance.transform.position = spawnArea.TransformPoint(randomPosWithinArea);
        }

    }

    public void TrashSorted(DraggableTrash sortedItem)
    {
        sortedTrash++;
        Destroy(sortedItem.gameObject);
        PlaySound();
        if (sortedTrash == totalTrash)
        {
            // All trash sorted
            Debug.Log("All trash sorted!");
            Complete();
        }
    }

    private void CleanupSpawnedTrash()
    {
        foreach (GameObject trashItem in spawnedTrashItems)
        {
            Destroy(trashItem);
        }
        spawnedTrashItems.Clear();
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
        CleanupSpawnedTrash();
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
        CleanupSpawnedTrash();
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
        CleanupSpawnedTrash();
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
        { //RestartMiniGameLogic(); 
        }
    }

    public override void RestartMiniGameLogic()
    {
        
        CleanupSpawnedTrash();
        this.sortedTrash = 0;
        this.totalTrash = 0;
    }
}
