using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    private BezierManager _bezierManager;
    public MouseController mController;
    public Text stepValueTxt;
    public Text zValueTxt;
    
    private void Start()
    {
        _bezierManager = BezierManager.Instance;
        stepValueTxt.text =  _bezierManager.Step.ToString();
        zValueTxt.text =  mController.zIndex.ToString("#.0");
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

    public void OnPlusZ()
    {
        mController.zIndex += .5f;
        zValueTxt.text = mController.zIndex.ToString("#.0");
    }

    public void OnMinusZ()
    {
        mController.zIndex -= .5f;
        zValueTxt.text =  mController.zIndex.ToString("#.0");
    }
}
