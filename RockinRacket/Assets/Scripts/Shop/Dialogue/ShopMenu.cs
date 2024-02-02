using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;

    private bool returningMenu;

    public void Open() { gameObject.SetActive(true); }
    public void Close() { gameObject.SetActive(false); }

    private void OnEnable()
    {
        if (returningMenu)
        {
            dialogueText.text = "Is there anything else I can do for you?";
            StartCoroutine(DisplayLine(dialogueText.text.Length));
        }
        else
        {
            dialogueText.text = "Hey there kiddo! What can I do for you today?";
            StartCoroutine(DisplayLine(dialogueText.text.Length));
            returningMenu = true;
        }
    }

    private IEnumerator DisplayLine(int length)
    {
        // Empty the dialogueText box to get the effect
        dialogueText.maxVisibleCharacters = 0;

        // Display each character one at a time (achieving the typing effect)
        for (int i = 0; i < length; i++)
        {
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(.005f);
        }
    }
}