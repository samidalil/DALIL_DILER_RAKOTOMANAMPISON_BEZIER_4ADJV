using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMenuManager : MonoBehaviour
{

    public GameObject targetEdit;
    
    public static EditMenuManager instance;

    
    public InputField posXField;
    public InputField posYField;
    public InputField posZField;
    [Space]
    public InputField rotXField;
    public InputField rotYField;
    public InputField rotZField;
    [Space]
    public InputField scaXField;
    public InputField scaYField;
    public InputField scaZField;

    private Transform targetTransform;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
            BezierManager.Instance.EditMenuManager = this;
    }


    private void Start()
    {
        targetTransform = targetEdit.transform;
    }

    private bool ValidateInput(string input)
    {
        float val;
        float.TryParse(input, out val);

        if (Math.Abs(val) < 0.01)
        {
            Debug.Log("invalid degree input");
            return false;
        }

        return true;
    }

    #region PositionChange

    public void OnPosXFieldChange()
    {

        bool isValid = ValidateInput(posXField.text);
        if (!isValid) return;
        float x = float.Parse(posXField.text);
        var position = targetTransform.position; 
        position = new Vector3(x, position.y, position.z);
        targetTransform.position = position;
    }
    
    public void OnPosYFieldChange()
    {

        bool isValid = ValidateInput(posYField.text);
        if (!isValid) return;
        float y = float.Parse(posYField.text);
        var position = targetTransform.position; 
        position = new Vector3(position.x, y, position.z);
        targetTransform.position = position;
    }
    
    public void OnPosZFieldChange()
    {

        bool isValid = ValidateInput(posZField.text);
        if (!isValid) return;
        float z = float.Parse(posZField.text);
        var position = targetTransform.position; 
        position = new Vector3(position.x, position.y, z);
        targetTransform.position = position;
    }

    #endregion

    #region RotationChange

    
    public void OnRotXFieldChange()
    {
        bool isValid = ValidateInput(rotXField.text);
        if (!isValid) return;
        float x = float.Parse(rotXField.text);
        var rotation = targetTransform.rotation; 
        rotation = new Quaternion(x, rotation.y, rotation.z, rotation.w);
        targetTransform.rotation = rotation;
    }
    
    public void OnRotYFieldChange()
    {
        bool isValid = ValidateInput(rotYField.text);
        if (!isValid) return;
        float y = float.Parse(rotYField.text);
        var rotation = targetTransform.rotation; 
        rotation = new Quaternion(rotation.x, y, rotation.z, rotation.w);
        targetTransform.rotation = rotation;
    }
    
    public void OnRotZFieldChange()
    {
        bool isValid = ValidateInput(rotZField.text);
        if (!isValid) return;
        float z = float.Parse(rotZField.text);
        var rotation = targetTransform.rotation; 
        rotation = new Quaternion(rotation.x, rotation.y, z, rotation.w);
        targetTransform.rotation = rotation;
    }

    #endregion

    #region ScaleChange
    public void OnScaXFieldChange()
    {
        bool isValid = ValidateInput(scaXField.text);
        if (!isValid) return;
        float x = float.Parse(scaXField.text);
        var scale = targetTransform.localScale; 
        scale = new Vector3(x, scale.y, scale.z);
        targetTransform.localScale = scale;
    }
    
    
    public void OnScaYFieldChange()
    {
        bool isValid = ValidateInput(scaYField.text);
        if (!isValid) return;
        float y = float.Parse(scaYField.text);
        var scale = targetTransform.localScale; 
        scale = new Vector3(scale.x, y, scale.z);
        targetTransform.localScale = scale;
    }
    
    public void OnScaZFieldChange()
    {
        bool isValid = ValidateInput(scaZField.text);
        if (!isValid) return;
        float z = float.Parse(scaZField.text);
        var scale = targetTransform.localScale; 
        scale = new Vector3(scale.x, scale.y, z);
        targetTransform.localScale = scale;
    }

    

    #endregion

    public void OnExtend()
    {
        //TODO no implem
    }

    public void OnDelete()
    {
        //TODO no implem
    }
    
}
