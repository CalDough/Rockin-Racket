using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cleaning : MiniGame
{
    public GameObject[] trashPrefabs; 
    public int minTrashSpawn = 3;    // Minimum number of trash items to spawn
    public int maxTrashSpawn = 7;    // Maximum number of trash items to spawn

    public RectTransform spawnArea;
    [SerializeField] Transform trashParentTransform;
    [SerializeField] private int totalTrashCount;
    [SerializeField] private int cleanedTrashCount = 0;
    [SerializeField] private int score = 0; //Not sure if I want to have the player get rewarded more for more trash or type of trash
    [SerializeField] private List<GameObject> spawnedTrashItems = new List<GameObject>();
    



    public override void Activate()
    {
        RestartMiniGameLogic();
        isActiveEvent = true;
        remainingDuration = duration;
        if (!infiniteDuration) {
            remainingDuration = duration;
            durationCoroutine = StartCoroutine(EventDurationCountdown());
        }
        GameEvents.EventStart(this);
        SpawnTrash();
    }

    public void SpawnTrash()
    {
        totalTrashCount = Random.Range(minTrashSpawn, maxTrashSpawn);

        for (int i = 0; i < totalTrashCount; i++)
        {
            GameObject trashPrefab = trashPrefabs[Random.Range(0, trashPrefabs.Length)];

            TrashObject trashScript = trashPrefab.GetComponent<TrashObject>();
            if (trashScript == null) 
            {
                i--;
                continue;
            }

            // calculate random position within spawnArea
            Vector3 randomPosWithinArea = new Vector3(
                Random.Range(spawnArea.rect.xMin, spawnArea.rect.xMax),
                Random.Range(spawnArea.rect.yMin, spawnArea.rect.yMax),
                0 
            );
            GameObject spawnedTrash = Instantiate(trashPrefab);
            if(trashParentTransform != null)
            {
                spawnedTrash.transform.SetParent(trashParentTransform, false);
            }
            else
            {
                spawnedTrash.transform.SetParent(transform, false);
            } 
            spawnedTrash.transform.position = spawnArea.TransformPoint(randomPosWithinArea);
            trashScript = spawnedTrash.GetComponent<TrashObject>();
            trashScript.cleaning = this;
            spawnedTrashItems.Add(spawnedTrash);
        }
    }


    public void CleanupTrash(TrashObject trash)
    {
        cleanedTrashCount++;
        score += trash.value;
        Destroy(trash.gameObject);

        //Debug.Log($"Cleaned: {cleanedTrashCount}/{totalTrashCount}");

        if (cleanedTrashCount == totalTrashCount)
        {
            Debug.Log("All trash cleaned up!");
            this.Complete();
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
        //Debug.Log("No reset added yet");
        CleanupSpawnedTrash();
        totalTrashCount = 0;
        cleanedTrashCount = 0;
    }

}
