using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePhysics : MonoBehaviour
{
    private Camera mainCamera;
    private CrowdTrash currentlyDragging;
    public bool isGamePaused = false;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        TimeEvents.OnGamePaused += Pause;
        TimeEvents.OnGameResumed += Resume;
    }

    private void OnDisable()
    {
        TimeEvents.OnGamePaused -= Pause;
        TimeEvents.OnGameResumed -= Resume;
    }

    private void Resume()
    {isGamePaused = false;}

    private void Pause()
    {
        if(currentlyDragging != null)
        {
            currentlyDragging.StopDragging();
            currentlyDragging = null;
        }
        isGamePaused = true;
    }
    
    void Update()
    {
        if(isGamePaused) {return;}

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);

            if (hit.collider != null)
            {
                currentlyDragging = hit.collider.GetComponent<CrowdTrash>();
                if (currentlyDragging != null)
                {
                    currentlyDragging.StartDragging();
                }
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && currentlyDragging != null)
        {
            currentlyDragging.StopDragging();
            currentlyDragging = null;
        }
    }
    
}