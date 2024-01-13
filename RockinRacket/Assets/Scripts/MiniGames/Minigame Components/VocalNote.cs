using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VocalNote : MonoBehaviour, IPointerClickHandler
{
    public bool WasClicked { get; private set; } = false;
    public bool IsClickable { get; private set; } = true;
    public Chord AssignedChord { get; set; }
    public MicNoteHelp GameInstance { get; set; } 

    private float moveSpeed = 5f;

    void FixedUpdate()
    {
        if (AssignedChord != null && IsClickable)
        {
            transform.position = Vector2.MoveTowards(transform.position, AssignedChord.StringEnd, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, AssignedChord.StringEnd) < 0.1f)
            {
                IsClickable = false; 
                GameInstance?.NoteReachedEnd(this);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsClickable)
        {
            WasClicked = true;
            ChangeOpacity(0.5f); 
            GameInstance?.NoteClicked(this);
        }
    }

    private void ChangeOpacity(float opacity)
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            Color color = sprite.color;
            color.a = opacity;
            sprite.color = color;
        }
    }
}
