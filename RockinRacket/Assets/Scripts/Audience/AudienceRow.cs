using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceRow : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private List<Transform> audienceMemberPositions;

    void Start()
    {
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("Start or End point not assigned in " + gameObject.name);
            return;
        }

        audienceMemberPositions = new List<Transform>();
    }

    public Vector3 GetRandomPosition()
    {
        float randomT = Random.Range(0f, 1f);
        Vector3 position = Vector3.Lerp(startPoint.position, endPoint.position, randomT);
        return position;
    }
}