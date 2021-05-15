using UnityEngine;
using UnityEngine.UI;

public enum Mode
{
    EDIT = 0,
    CREATION = 1,
}

public class MenuManager : MonoBehaviour
{
    private Mode mode;
    private int degree;
    
    public InputField degreeInput;
    public Mode Mode
    {
        get => mode;
        set => mode = value;
    }

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
        BezierManager.Instance.CreateCurve(degree);
        this.gameObject.SetActive(false);
    }

    public void SetPosOfMenu(Vector3 position)
    {
        degreeInput.SetTextWithoutNotify("3");
        this.gameObject.transform.SetPositionAndRotation(position, Quaternion.identity);
        this.gameObject.SetActive(true);
    }



}
