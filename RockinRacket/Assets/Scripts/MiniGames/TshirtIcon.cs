using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TshirtIcon : MonoBehaviour
{
    private void OnMouseDown()
    {
        TshirtLauncher.Instance.StartAiming();
    }
}

