using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static ConcertAttendee;

public class TShirtCannon : MonoBehaviour
{
    public Shirt tShirtPrefab;
    public GameObject shirtSpawnLocation;
    public LineRenderer trajectoryLineRenderer;
    public SpriteRenderer readySprite;
    public float power = 5f;
    public int steps = 100;
    public float stepDistance = 10;
    public float cooldown = 1f;
    private bool onCooldown = false;
    public float lerpSpeed = 1f;
    public Transform cannonBody;
    public Transform cannonRestTransform; 
    public Transform LineRendererOrigin;

    private Camera mainCamera;
    public Vector2 velocity;
    public bool isReadyToShoot = true;
    private Vector3 initialMousePos;
    private Vector3 currentMousePos;
    private bool isDragging = false;


    public List<RequestableItem> AvailableShirtTypes;
    public RequestableItem SelectedShirtType;

    private OverworldControls controls;
    
    public static TShirtCannon Instance { get; private set; }

    public void ChangeShirtType(ConcertAttendee.RequestableItem shirtType)
    {
        this.SelectedShirtType = shirtType;
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
            Debug.LogWarning("No available shirt types to select.");
            return RequestableItem.RedShirt;
            
        }
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
        //Debug.Log("Drag Attempt");
        if (isReadyToShoot)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0;  

            RaycastHit2D hit = Physics2D.Raycast(worldMousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                initialMousePos = worldMousePos;
                isDragging = true;
            }
        }
    }

    private void EndDrag()
    {
        if (isDragging)
        {
            FireTShirt();
            isDragging = false;
            StartCoroutine(Cooldown());
        }
        else if (onCooldown)
        {
            LerpToRestPosition();
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        readySprite.enabled = true;
        this.SelectedShirtType = RequestableItem.RedShirt;
    }

    void Update() 
    {
        if (isDragging)
        {
            currentMousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            currentMousePos.z = 0;

            Vector2 direction = (Vector2)currentMousePos - (Vector2)cannonBody.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            cannonBody.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            CalculateTrajectory(direction.magnitude, angle - 90);
        }
        else if (onCooldown)
        {
            LerpToRestPosition();
        }
    }

    void CalculateTrajectory(float dragDistance, float angle)
    {
        Vector2[] trajectory = Plot(tShirtPrefab.rb, (Vector2)transform.position, (currentMousePos - initialMousePos) * power, steps);
        trajectoryLineRenderer.positionCount = trajectory.Length;
        Vector3[] positions = new Vector3[trajectory.Length];
        for (int i = 0; i < trajectory.Length; i++)
        {
            positions[i] = trajectory[i];
        }
        trajectoryLineRenderer.SetPositions(positions);
    }

    Vector2[] Plot(Rigidbody2D rb2, Vector2 pos, Vector2 vel, int steps)
    {
        Vector2[] results = new Vector2[steps];
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations * stepDistance; 
        Vector2 gravityAccel = Physics2D.gravity * rb2.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rb2.drag;
        Vector2 moveStep = vel * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        return results;
    }

    void FireTShirt()
    {
        trajectoryLineRenderer.enabled = false;
        Shirt shirtInstance = Instantiate(tShirtPrefab, shirtSpawnLocation.transform.position, Quaternion.identity);
        shirtInstance.rb.velocity = (currentMousePos - initialMousePos) * power;
        shirtInstance.ShirtType = this.SelectedShirtType;
        shirtInstance.SetColor();
        StartCoroutine(Cooldown());
    }

    void LerpToRestPosition()
    {
        //cannonBody.position = Vector3.Lerp(cannonBody.position, cannonRestTransform.position, Time.deltaTime);
        cannonBody.rotation = Quaternion.Lerp(cannonBody.rotation, cannonRestTransform.rotation, Time.deltaTime * lerpSpeed);
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        readySprite.enabled = false;

        yield return new WaitForSeconds(cooldown);

        onCooldown = false;
        readySprite.enabled = true;
        isReadyToShoot = true;
    }


}