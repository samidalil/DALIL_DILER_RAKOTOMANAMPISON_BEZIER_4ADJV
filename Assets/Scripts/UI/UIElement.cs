using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    public void Entered()
    {
        
        Debug.Log("setting on ui true");
        BezierManager.Instance.IsOnUi = true;
    }

    public void Exited()
    {
        Debug.Log("setting on ui false");
        BezierManager.Instance.IsOnUi = false;
    }
}
