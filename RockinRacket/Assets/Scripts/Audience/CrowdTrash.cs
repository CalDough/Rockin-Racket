using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrowdTrash : MonoBehaviour
{
    public bool IsProjectile { get; set; } = true;
    private bool isDragging = false;
    private Vector2 lastPosition;
    private float lastTime;
    private Rigidbody2D rb;
    public float maxVelocity = 10f; 
    public float updateInterval = 0.1f; 

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

            if (Time.time - lastTime >= updateInterval)
            {
                lastPosition = mousePosition;
                lastTime = Time.time;
            }
        }
    }

    public void StartDragging()
    {
        isDragging = true;
        rb.gravityScale = 0;
        lastPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        lastTime = Time.time;
    }

    public void StopDragging()
    {
        Vector2 endPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        float endTime = Time.time;

        Vector2 velocity = (endPosition - lastPosition) / (endTime - lastTime);
        velocity = Vector2.ClampMagnitude(velocity, maxVelocity);
        rb.velocity = velocity;

        isDragging = false;
        rb.gravityScale = 1;
    }
}