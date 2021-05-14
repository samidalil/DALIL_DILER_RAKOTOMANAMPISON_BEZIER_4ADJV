using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Mode
{
    EDIT = 0,
    CREATION = 1
};
public class MenuManager : MonoBehaviour
{
    private BezierManager _bezierManager;
    private Mode mode;
    private int degree;
    
    public Text stepValueTxt;
    public GameObject bezierCreationMenu;
    public InputField degreeInput;
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
        SwitchToEditMode();
        _bezierManager = BezierManager.Instance;
        stepValueTxt.text =  _bezierManager.Step.ToString();
    }

    public void SwitchToEditMode()
    {
        mode = Mode.EDIT;
    }

    
    public void SwitchToCreator()
    {
        int.TryParse(degreeInput.text, out degree);

        if (degree == 0)
        {
            Debug.Log("invalid degree input");
            return;
        }

        //_bezierManager.Step = degree;
        mode = Mode.CREATION;
        BezierManager.Instance.CurrentCurve = Instantiate(
            BezierManager.Instance.CurvePrefab,
            Vector3.zero,
            Quaternion.identity
        ).GetComponent<BezierCurve>();
        BezierManager.Instance.CurrentCurve.Degree = degree;
        this.gameObject.SetActive(false);
    }

    public void setPosOfMenu(Vector3 position)
    {
        if (mode == Mode.EDIT)
        {
            this.gameObject.transform.SetPositionAndRotation(position, Quaternion.identity);
            this.gameObject.SetActive(true);
        }
        else if (mode == Mode.CREATION)
        {
            bezierCreationMenu.transform.SetPositionAndRotation(position, Quaternion.identity);
            bezierCreationMenu.SetActive(true);
        }
    }


}
