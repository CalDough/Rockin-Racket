using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrowdShirt : MonoBehaviour
{
    public LineRenderer trajectoryLineRenderer;
    public float launchPower = 10f;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 dragStartPos;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        rb.isKinematic = true; 
    }

    public void OnDragStart(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        
        dragStartPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        isDragging = true;
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        if (!isDragging) return;
        
        Vector2 currentPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = dragStartPos - currentPos;
        Vector2 force = direction * launchPower;
        trajectoryLineRenderer.positionCount = 10;
        ShowTrajectory(rb.position, force);
    }

    public void OnDragEnd(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Canceled || !isDragging) return;

        Vector2 currentPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = dragStartPos - currentPos;
        rb.isKinematic = false;
        rb.AddForce(direction * launchPower, ForceMode2D.Impulse);
        trajectoryLineRenderer.positionCount = 0;
        isDragging = false;
    }

    void ShowTrajectory(Vector2 origin, Vector2 speed)
    {
        int numPoints = 10;
        Vector3[] points = new Vector3[numPoints]; 

        points[0] = origin;

        for (int i = 1; i < numPoints; i++)
        {
            float time = i * 0.1f;
            Vector2 point = origin + speed * time + Physics2D.gravity * time * time / 2f;
            points[i] = new Vector3(point.x, point.y, 0);
        }

        trajectoryLineRenderer.SetPositions(points);
    }

}
