using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubChangingDialogueTrigger : DialogueTrigger
{
    [Header("Ink JSONs")]
    // [SerializeField] TextAsset inkJSON_Hub0;
    [SerializeField] TextAsset inkJSON_Hub1;
    [SerializeField] TextAsset inkJSON_Hub2;
    [SerializeField] TextAsset inkJSON_Hub3;
    [SerializeField] TextAsset inkJSON_Hub4;

    override public void Button_StartDialogue()
    {
        if (!DialogueManager.GetInstance().dialogueActive)
        {   
            switch(RoomManager.GetInstance().currentHub)
            {
                // case 0:
                // {
                //     currentDialogue = inkJSON_Hub0;
                //     break;
                // }
                case 1:
                {
                    inkJSON = inkJSON_Hub1;
                    break;
                }
                case 2:
                {
                    inkJSON = inkJSON_Hub2;
                    break;
                }
                case 3:
                {
                    inkJSON = inkJSON_Hub3;
                    break;
                }
                case 4:
                {
                    inkJSON = inkJSON_Hub4;
                    break;
                }
                default:
                {
                    inkJSON = inkJSON_Hub1;
                    break;
                }
            }

            visualCue.SetActive(false);
            DialogueManager.GetInstance().StartDialogue(inkJSON);
            isShown = false;
            this.gameObject.GetComponent<Image>().enabled = false;
            thisDialogueActive = true;
        }


    }
}
