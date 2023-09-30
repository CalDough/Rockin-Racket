using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is to be used in conjunction with the Audience Prefab and may reference values from
 * Animal.cs and AnimalManager.cs
 */

public class Audience : MonoBehaviour
{

    // Private variables
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /*
     * This method is to be used to play the audience member's reaction to the T-Shirt cannon hitting them
     */
    public void PlayTCMReaction(string pressure)
    {
        // Temporary Debug Statement
        Debug.Log("<color=green>PlayTCMReaction called with pressure value</color>" + pressure);

        // To set the blend tree, we first need to set the int value that controls it
        switch (pressure)
        {
            case "Good":
                anim.SetInteger("TCM_Mood", 25);
                break;
            case "Weak":
                anim.SetInteger("TCM_Mood", 15);
                break;
            case "Bad":
                anim.SetInteger("TCM_Mood", 5);
                break;
            default:
                Debug.Log("<color=red>INVALID PRESSURE GIVEN IN AUDIENCE.CS</color>");
                break;
        }

        // Then we trigger the swap from the mood tree to the TCM tree
        //anim.SetTrigger("TCM_Trigger"); Commented out until animation implemented
    }
}
