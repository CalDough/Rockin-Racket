using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static ConcertAttendee;

public class CannonShirtClickable : MonoBehaviour
{
    public RequestableItem SelectedShirtType;

    public SpriteRenderer boxSprite;
    public Sprite highlightedSprite;
    public Sprite unhighlightedSprite;

    private OverworldControls controls;

    private void Awake()
    {
        controls = new OverworldControls();
        controls.Player.Fire.performed += ctx => OnShirtClick();
        boxSprite.sprite = unhighlightedSprite;
    }

    public void UpdateHighlight(bool isSelected)
    {
        boxSprite.sprite = isSelected ? highlightedSprite : unhighlightedSprite;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }


    private void OnShirtClick()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        worldMousePos.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(worldMousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
        {
            Debug.Log("Now this shirt Color: " + SelectedShirtType);
            TShirtCannon.Instance.ChangeShirtType(SelectedShirtType);
            TShirtCannon.Instance.SelectShirtType(SelectedShirtType);
        }
    }
}
