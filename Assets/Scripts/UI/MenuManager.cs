using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    private BezierManager _bezierManager;
    private Mode mode;
    
    public Text stepValueTxt;
    public GameObject bezierMenu;



    public Mode Mode => mode;


    private static MenuManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
        BezierManager.Instance.MenuManager = this;
    }

    public static MenuManager Instance => instance;

    private void Start()
    {
       
        SwitchToNoneMode();
        _bezierManager = BezierManager.Instance;
        stepValueTxt.text =  _bezierManager.Step.ToString();
    }


    public void SwitchToEditMode()
    {
        mode = Mode.EDIT;
    }

    public void SwitchToNoneMode()
    {
        mode = Mode.NONE;
    }
    
    public void SwitchToCreator()
    {
        mode = Mode.CREATION;
        bezierMenu.SetActive(false);
    }

    public void setPosOfMenu(Vector3 position)
    {
        bezierMenu.transform.SetPositionAndRotation(position, Quaternion.identity);
        bezierMenu.SetActive(true);
    }


}
