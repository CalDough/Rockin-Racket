using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VocalNote : MonoBehaviour, IPointerClickHandler
{
    [field:SerializeField] public bool WasClicked { get; private set; } = false;
    [field:SerializeField] public bool IsClickable { get; private set; } = true;
    public Chord AssignedChord { get; set; }
    public MicNoteHelp GameInstance { get; set; } 

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private RawImage noteImage;
    [SerializeField] private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        noteImage = GetComponent<RawImage>();
    }

    void FixedUpdate()
    {
        if (AssignedChord != null && IsClickable)
        {
            Vector2 startPos = AssignedChord.GetWorldPosition(AssignedChord.StringStart);
            Vector2 endPos = AssignedChord.GetWorldPosition(AssignedChord.StringEnd);

            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, endPos, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(rectTransform.anchoredPosition, endPos) < 0.1f)
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
            ChangeOpacity(0.25f); 
            GameInstance?.NoteClicked(this);
        }
    }

    private void ChangeOpacity(float opacity)
    {
        var sprite = GetComponent<RawImage>();
        if (sprite != null)
        {
            Color color = sprite.color;
            color.a = opacity;
            sprite.color = color;
        }
    }

    public void DisableNote()
    {
        if (noteImage != null)
        {
            noteImage.enabled = false;
        }
    }
}
