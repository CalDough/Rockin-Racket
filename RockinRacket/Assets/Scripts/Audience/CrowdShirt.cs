using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrowdShirt : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private LineRenderer trajectoryLineRenderer;
    [SerializeField] private float power = 5f;
    [SerializeField] private int steps = 100;
    [SerializeField] private float stepDistance = 10;

    [SerializeField] private bool hasLaunched = false;
    private Vector3 initialMousePos;
    private Vector3 endMousePos;
    private Vector2 _velocity;
    private Camera mainCamera;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

    }

    void Update()
    {
        
    }

    
    void OnMouseDown()
    {       
        if(hasLaunched)
        {return;}
        initialMousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        trajectoryLineRenderer.enabled = true;
    }

    void OnMouseDrag()
    {
        if(hasLaunched)
        {return;}
        endMousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _velocity = (endMousePos-initialMousePos) * power;

        Vector2[] trajectory = Plot(_rb, transform.position, _velocity, steps);
        trajectoryLineRenderer.positionCount = trajectory.Length;
        Vector3[] positions = new Vector3[trajectory.Length];
        for(int i = 0; i < trajectory.Length; i++)
        {
            positions[i] = trajectory[i];
        }
        trajectoryLineRenderer.SetPositions(positions);

    }

    void OnMouseUp()
    {
        if(hasLaunched)
        {return;}
        endMousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _velocity = (endMousePos-initialMousePos) * power;

        _rb.velocity = _velocity;
        trajectoryLineRenderer.enabled = false;
        _rb.isKinematic = false;
        Destroy(this.gameObject, 10);
        
        hasLaunched = true;
    }

    public Vector2[] Plot(Rigidbody2D rb2, Vector2 pos, Vector2 vel, int steps)
    {
        Vector2[] results = new Vector2[steps];
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations * stepDistance; 
        Vector2 gravityAccel = Physics2D.gravity * rb2.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rb2.drag;
        Vector2 moveStep = _velocity * timestep;

        for(int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        return results;
    }

    

}