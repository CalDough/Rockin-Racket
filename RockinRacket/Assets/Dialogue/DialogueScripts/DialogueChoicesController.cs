using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoicesController : MonoBehaviour
{
    [SerializeField] float scrollSpeedMultiplier;
    private Vector3 defaultPosition;

    private void Awake()
    {
        defaultPosition = this.transform.localPosition;
    }


    private void Update()
    {
        if (DialogueManager.GetInstance().numChoices > 3)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                float newX = transform.localPosition.x + (scroll * scrollSpeedMultiplier);
                newX = Mathf.Clamp(newX, -800f, 0f);
                transform.localPosition = new Vector3(newX, transform.localPosition.y);
            }   
        }
        else
        {
            transform.localPosition = defaultPosition;
        }
    }
}
