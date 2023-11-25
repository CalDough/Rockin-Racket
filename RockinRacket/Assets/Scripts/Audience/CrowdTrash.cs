using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrowdTrash : MonoBehaviour
{
    public bool IsProjectile { get; set; } = true;
    private bool isDragging = false;
    private Vector2 lastMousePosition;
    private Rigidbody2D rb;
    public float maxVelocity = 10f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = mousePosition;
            Vector2 velocity = (mousePosition - lastMousePosition) / Time.deltaTime;
            velocity = Vector2.ClampMagnitude(velocity, maxVelocity);
            rb.velocity = velocity;

            lastMousePosition = mousePosition; 
        }
    }

    public void StartDragging()
    {
        isDragging = true;
        rb.gravityScale = 0; 
        lastMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void StopDragging()
    {
        isDragging = false;
        rb.gravityScale = 1; 
    }
}