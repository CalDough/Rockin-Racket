using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static ConcertAttendee;

public class TShirtCannon : MonoBehaviour
{
    public static TShirtCannon Instance { get; private set; }

    [Header("Reference Variables")]
    public Shirt tShirtPrefab;
    public SpriteRenderer readySprite;
    public GameObject shirtSpawnLocation;
    public LineRenderer trajectoryLineRenderer;
    public Transform cannonBody;
    public Transform cannonRestTransform; 

    [Header("Cannon Variables")]
    public List<RequestableItem> AvailableShirtTypes;
    public RequestableItem SelectedShirtType;
    public float power = 5f;
    public int steps = 100;
    public float stepDistance = 10;
    public float cooldown = 1f;
    public float lerpSpeed = 1f;
    public float gravityScale;
    [Header("Rotation Clamp")]
    public float minRotationAngle = -45f; 
    public float maxRotationAngle = 45f;

    public Vector2 velocity;
    public bool isReadyToShoot = true;
    private bool isDragging = false;
    private Camera mainCamera;
    private Vector3 initialMousePos;
    private Vector3 currentMousePos;
    private OverworldControls controls;
    

    public void ChangeShirtType(ConcertAttendee.RequestableItem shirtType)
    {
        this.SelectedShirtType = shirtType;
    }

    private void Awake()
    {
        if (Instance == null)
        {Instance = this;}
        else
        {Destroy(gameObject);}

        controls = new OverworldControls();
        controls.Player.Fire.performed += ctx => StartDrag(); 
        controls.Player.Fire.canceled += ctx => EndDrag();
    }

    void Start()
    {
        mainCamera = Camera.main;
        readySprite.enabled = true;
        this.SelectedShirtType = RequestableItem.RedShirt;
        trajectoryLineRenderer.enabled = false;

        Rigidbody2D rbPrefab = tShirtPrefab.GetComponent<Rigidbody2D>();
        if (rbPrefab != null) {
            gravityScale = rbPrefab.gravityScale;
        } else {
            Debug.LogError("tShirtPrefab does not have a Rigidbody2D");
            gravityScale = 1f; 
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void StartDrag()
    {
        //Debug.Log("Drag Start");
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (hit.collider != null && hit.collider.transform.IsChildOf(cannonBody))
        {
            Debug.Log("Is Dragging Cannon");
            isDragging = true;
            initialMousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            initialMousePos.z = 0; 
        }
    }

    private void EndDrag()
    {
        if (!isDragging) return;

        currentMousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        currentMousePos.z = 0;

        Vector2 direction = currentMousePos - (Vector3)shirtSpawnLocation.transform.position;
        float distance = direction.magnitude;
        velocity = direction.normalized * power * Mathf.Clamp(distance, 0.5f, 10); 

        isDragging = false;

        FireShirt(direction, velocity);
    }

    private void FireShirt(Vector2 direction, Vector2 velocity)
    {
        trajectoryLineRenderer.enabled = false; 
        if (!isReadyToShoot)
        {
            Debug.Log("Cannon is on cooldown. Cannot fire");
            return;
        }

        Shirt firedShirt = Instantiate(tShirtPrefab, shirtSpawnLocation.transform.position, Quaternion.identity);
        Rigidbody2D rb = firedShirt.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = velocity;
        firedShirt.SetColor(this.SelectedShirtType);

        isReadyToShoot = false;
        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown()
    {
        readySprite.enabled = false;
        isReadyToShoot = false; 
        yield return new WaitForSeconds(cooldown);
        readySprite.enabled = true;
        isReadyToShoot = true; 
    }

    public RequestableItem SelectRandomShirtType()
    {
        if (AvailableShirtTypes.Count > 0)
        {
            int randomIndex = Random.Range(0, AvailableShirtTypes.Count);
            SelectedShirtType = AvailableShirtTypes[randomIndex];
            return SelectedShirtType;
        }
        else
        {
            Debug.LogWarning("No available shirt types to select");
            return RequestableItem.RedShirt;
            
        }
    }

    void LerpToRestPosition()
    {
        cannonBody.rotation = Quaternion.Lerp(cannonBody.rotation, cannonRestTransform.rotation, Time.deltaTime * lerpSpeed);
    }

    void Update()
    {
        if (isDragging)
        {
            RotateCannonTowardsMouse();
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = mousePosition - (Vector2)shirtSpawnLocation.transform.position;
            float distance = direction.magnitude;
            Vector2 initialVelocity = direction.normalized * power * Mathf.Clamp(distance, 1, 10); 
            UpdateTrajectoryLine(shirtSpawnLocation.transform.position, new Vector3(initialVelocity.x, initialVelocity.y, 0));
            trajectoryLineRenderer.enabled = true;
        }
        else if (!isDragging) 
        {
            LerpCannonToRestPosition();
            trajectoryLineRenderer.enabled = false; 
        }
    }

    private void RotateCannonTowardsMouse()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mousePosition - (Vector2)cannonBody.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        float restAngle = cannonRestTransform.eulerAngles.z;
        
        float relativeAngle = Mathf.DeltaAngle(restAngle, angle);
        
        relativeAngle = Mathf.Clamp(relativeAngle, minRotationAngle, maxRotationAngle);
        
        float clampedAngle = restAngle + relativeAngle;
        
        cannonBody.rotation = Quaternion.Euler(0, 0, clampedAngle);
    }

    void LerpCannonToRestPosition()
    {
        if (Quaternion.Angle(cannonBody.rotation, cannonRestTransform.rotation) > 0.1f) 
        {
            cannonBody.rotation = Quaternion.Lerp(cannonBody.rotation, cannonRestTransform.rotation, Time.deltaTime * lerpSpeed);
        }
    }

    void UpdateTrajectoryLine(Vector3 initialPosition, Vector3 initialVelocity)
    {
        Vector3[] points = new Vector3[steps];
        trajectoryLineRenderer.positionCount = steps;
        
        Vector3 gravity = new Vector3(0, Physics2D.gravity.y * gravityScale, 0);
        
        for (int i = 0; i < steps; ++i)
        {
            float time = i * (stepDistance / steps);
            Vector3 position = initialPosition + initialVelocity * time + 0.5f * gravity * time * time;
            points[i] = position;
        }

        trajectoryLineRenderer.SetPositions(points);
    }
}