using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvents
{
    public static EventHandler DialogueEnd;

    public static void InvokeDialogueEnd()
    {
        if (DialogueEnd != null)
        {
            DialogueEnd(null, EventArgs.Empty);
        }
    }
}
