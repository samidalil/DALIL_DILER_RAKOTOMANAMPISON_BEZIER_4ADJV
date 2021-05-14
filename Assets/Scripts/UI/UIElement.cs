using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    public void Entered()
    {
        BezierManager.Instance.IsOnUi = true;
    }

    public void Exited()
    {
        BezierManager.Instance.IsOnUi = false;
    }
}
