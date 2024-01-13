using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChordNote : MonoBehaviour, IPointerClickHandler
{
    public delegate void ChordNoteClicked(ChordNote chordNote);
    public static event ChordNoteClicked OnChordNoteClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        //PlaySound();
        OnChordNoteClicked?.Invoke(this);
        Destroy(gameObject); // Destroy the note when clicked
    }
}
