using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChordNote : MonoBehaviour, IPointerClickHandler
{
    [field:SerializeField] public bool IsClickable { get; private set; } = true;
    //[field:SerializeField] public bool WasClicked { get; private set; } = false;
    public ChordFinding GameInstance { get; set; }
    [SerializeField] private Image noteImage;
    [SerializeField] private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        noteImage = GetComponent<Image>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        //PlaySound();
        
        if (IsClickable)
        {
            IsClickable = false;
            ChangeOpacity(0.25f);
            GameInstance?.NoteClicked(this); 
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
