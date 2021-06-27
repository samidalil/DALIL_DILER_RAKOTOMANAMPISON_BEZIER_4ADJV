using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileMenuManager : MonoBehaviour
{

    public Toggle closedProfileToggle;

    public InputField profileInputField;

    private int profilePointNumber;
    private bool toggleVal;

    private BezierManager _bezierManager;
    private MenuManager _menuManager;
    #region Singleton

        private static ProfileMenuManager instance;
        private void Awake()
        {
            if (instance != null)
                Destroy(this);
            else instance = this;
            BezierManager.Instance.ProfileMenuManager = this;
        }
    
        public static ProfileMenuManager Instance => instance;


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _bezierManager = BezierManager.Instance;
        _menuManager = MenuManager.Instance;
        gameObject.SetActive(false);

    }

    
    public void SwitchToProfileCreator()
    {
        int.TryParse(profileInputField.text, out profilePointNumber);
        
        if (profilePointNumber == 0)
        {
            Debug.Log("invalid degree input");
            return;
        }

        toggleVal = closedProfileToggle.isOn;

        _bezierManager.ProfilePointNumber = profilePointNumber;

        _menuManager.Mode = Mode.PROFILECREATION;
        ///mode = Mode.CREATION;
        this.gameObject.SetActive(false);
    }

    public void ActivateMenu()
    {
        gameObject.SetActive(true);
    }
}
