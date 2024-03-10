using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardNote : MonoBehaviour, IPointerClickHandler
{
    [field:SerializeField] public bool IsClickable { get; private set; } = true;
    [field:SerializeField] public bool WasClicked { get; set; } = false;
    
    [field:SerializeField] public Chord AssignedChord { get; set; }
    [field:SerializeField] public KeyboardPlayer GameInstance { get; set; } 

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Image noteImage;
    [SerializeField] private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        noteImage = GetComponent<Image>(); 
        if (AssignedChord != null)
        {
            noteImage.color = AssignedChord.GetChordColor(); 
            Color color = noteImage.color;
            color.a = .75f;
            noteImage.color = color;
        }
    }

    void FixedUpdate()
    {
        if (AssignedChord != null )
        {
            Vector2 startPos = AssignedChord.GetWorldPosition(AssignedChord.StringStart);
            Vector2 endPos = AssignedChord.GetWorldPosition(AssignedChord.StringEnd);

            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, endPos, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(rectTransform.anchoredPosition, endPos) < 0.1f)
            {
                IsClickable = false;
                if(!WasClicked)
                {
                    GameInstance?.NoteWasMissed();
                }
                Destroy(this.gameObject);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Note CLicked");
        if (IsClickable && !WasClicked)
        {
            ChangeOpacity(0.25f); 
            GameInstance?.NoteWasClicked(this);
        }
    }

    private void ChangeOpacity(float opacity)
    {
        var sprite = GetComponent<Image>();
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
