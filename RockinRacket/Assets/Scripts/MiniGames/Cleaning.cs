using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cleaning : MiniGame
{
    public GameObject[] trashPrefabs; 
    public int minTrashCount = 5;    // Minimum number of trash items to spawn
    public int maxTrashCount = 10;    // Maximum number of trash items to spawn

    public RectTransform spawnArea;

    [SerializeField] private int totalTrashCount;
    [SerializeField] private int cleanedTrashCount = 0;
    [SerializeField] private int score = 0; //Not sure if I want to have the player get rewarded more for more trash or type of trash

    public override void Activate()
    {
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
        totalTrashCount = Random.Range(minTrashCount, maxTrashCount + 1);

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
            Vector3 spawnPosition = new Vector3
            (
                Random.Range(spawnArea.rect.min.x, spawnArea.rect.max.x),
                Random.Range(spawnArea.rect.min.y, spawnArea.rect.max.y),
                0
            );

            // adjust for panel's actual position in world space 
            spawnPosition += (Vector3)spawnArea.position;

            GameObject spawnedTrash = Instantiate(trashPrefab, spawnPosition, Quaternion.identity, spawnArea);
            trashScript = spawnedTrash.GetComponent<TrashObject>();
            trashScript.cleaning = this;
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

    public override void RestartMiniGameLogic()
    {
        Debug.Log("No reset added yet");
    }

}
