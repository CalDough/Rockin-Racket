using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TshirtLauncher : MonoBehaviour
{
    public static TshirtLauncher Instance { get; private set; }

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform aimingIcon;
    [SerializeField] private GameObject tshirtPrefab;
    [SerializeField] private GameObject tshirtIconPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private LayerMask validTargetMask;
    private Transform tshirtIcon;

    private void Awake()
    {
        Instance = this;
    }

    public void StartAiming()
    {
        lineRenderer.enabled = true;
        aimingIcon.gameObject.SetActive(true);
        tshirtIcon = FindObjectOfType<TshirtIcon>().transform; 
    }

    public void LaunchTshirt()
    {
        Vector3 direction = aimingIcon.position - tshirtIcon.position;
        GameObject tshirt = Instantiate(tshirtPrefab, tshirtIcon.position, Quaternion.identity);
        Rigidbody2D rb = tshirt.GetComponent<Rigidbody2D>();
        rb.AddForce(direction.normalized * launchForce);
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        aimingIcon.position = mousePosition;

        lineRenderer.SetPositions(new Vector3[] { tshirtIcon.position, mousePosition });

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, validTargetMask);
        if (hit.collider != null)
        {
            aimingIcon.GetComponent<SpriteRenderer>().color = Color.green; // Valid target
        }
        else
        {
            aimingIcon.GetComponent<SpriteRenderer>().color = Color.red; // Invalid target
        }
    }

    public void SpawnTshirtIcon()
    {
        Instantiate(tshirtIconPrefab, spawnPosition.position, Quaternion.identity);
    }
}