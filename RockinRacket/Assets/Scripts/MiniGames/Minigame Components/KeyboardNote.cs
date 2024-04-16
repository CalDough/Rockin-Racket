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
    [SerializeField] private float fadeDuration = 2f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        noteImage = GetComponent<Image>(); 
        if (AssignedChord != null)
        {
            noteImage.color = AssignedChord.GetChordColor(); 
            Color color = noteImage.color;
            color.a = .7f;
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
        Debug.Log("Note Clicked");
        GameInstance?.NoteWasClicked(this);

    }

    public void ChangeOpacity()
    {
        if (IsClickable && !WasClicked)
        {
            WasClicked = true; 
            StartCoroutine(FadeOut(fadeDuration)); 

        }
    }

    IEnumerator FadeOut(float duration)
    {
        float startOpacity = noteImage.color.a;
        float endOpacity = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newOpacity = Mathf.Lerp(startOpacity, endOpacity, elapsedTime / duration);
            Color color = noteImage.color;
            color.a = newOpacity;
            noteImage.color = color;
            yield return null;
        }

        noteImage.color = new Color(noteImage.color.r, noteImage.color.g, noteImage.color.b, 0); 
    }

    public void DisableNote()
    {
        if (noteImage != null)
        {
            noteImage.enabled = false;
        }
    }
}
