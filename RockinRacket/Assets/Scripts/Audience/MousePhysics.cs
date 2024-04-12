using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePhysics : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] public CrowdTrash currentlyDragging;
    public bool isGamePaused = false;
    public LayerMask draggableLayer;
    
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
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, draggableLayer);

            if (hit.collider != null)
            {
                Debug.Log("Hit" + hit.collider.gameObject.name);
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