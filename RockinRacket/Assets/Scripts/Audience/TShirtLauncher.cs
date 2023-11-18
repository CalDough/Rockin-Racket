using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TShirtLauncher : MonoBehaviour
{
    public Rigidbody2D tShirtPrefab;
    public Transform launchPoint;

    private void Start()
    {
        StartCoroutine(TShirtSpawnRoutine());
    }

    IEnumerator TShirtSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f); 
            if (launchPoint.childCount == 0) 
            {
                Instantiate(tShirtPrefab, launchPoint.position, Quaternion.identity, launchPoint);
            }
        }
    }
}