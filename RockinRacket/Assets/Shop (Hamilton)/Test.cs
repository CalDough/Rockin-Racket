using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public InputAction MouseEnter;
    private void OnMouseEnter()
    {
        print("Mouse Enter");
    }
    //private void OnMouseExit()
    //{
    //    print("Mouse Exit");
    //}
    //private void OnMouseDown()
    //{
    //    print("Mouse Down");
    //}
}
