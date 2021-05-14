using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    private BezierManager _bezierManager;
    public Text stepValueTxt;
    
    private void Start()
    {
        _bezierManager = BezierManager.Instance;
        stepValueTxt.text =  _bezierManager.Step.ToString();
    }

    public void OnCreatePoint()
    {
        
    }

    public void OnEditPoint()
    {
        
    }

    public void OnPlusStep()
    {
        _bezierManager.Step += 1;
        stepValueTxt.text =  _bezierManager.Step.ToString();
    }

    public void OnMinusStep()
    {
        BezierManager.Instance.Step -= 1;
        stepValueTxt.text =  _bezierManager.Step.ToString();
    }


}
