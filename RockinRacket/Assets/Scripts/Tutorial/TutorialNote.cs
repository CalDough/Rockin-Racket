using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNote : MonoBehaviour
{

    public GameObject note;
    public Animator anim;

    public void ShowNote()
    {
        anim.Play("ShowNote");
    
    }

    public void HideNote()
    {
        anim.Play("HideNote");
    }
}
