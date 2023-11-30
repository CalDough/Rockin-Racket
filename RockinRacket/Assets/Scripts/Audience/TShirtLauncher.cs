using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TShirtLauncher : MonoBehaviour
{
    public Rigidbody2D tShirtPrefab;
    [SerializeField] private float cooldown = 3;
    [SerializeField] private GameObject spawnPos;



    private Coroutine spawnRoutine;



    private void Start()
    {
        if(CrowdController.Instance != null)
        {cooldown = CrowdController.Instance.tshirtSpawningCooldown;}
        
        StartTShirtSpawning();
    }

    public void StartTShirtSpawning()
    {
        if(spawnRoutine == null)
        {
            spawnRoutine = StartCoroutine(TShirtSpawnRoutine());
        }
    }

    public void StopTShirtSpawning()
    {
        if(spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
    }



    IEnumerator TShirtSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown); 
            if (spawnPos.transform.childCount == 0) 
            {
                Instantiate(tShirtPrefab, spawnPos.transform.position, Quaternion.identity, spawnPos.transform);
            }
        }
    }
}