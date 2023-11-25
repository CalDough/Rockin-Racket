using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePhysics : MonoBehaviour
{
    private Camera mainCamera;
    private CrowdTrash currentlyDragging;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
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