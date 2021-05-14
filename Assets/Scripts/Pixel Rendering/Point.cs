using UnityEngine;

[ExecuteInEditMode]
public class Point : Emitter
{
    #region Variables d'instance

    private Vector3 _oldPosition;

    #endregion

    #region Propriétés

    public Vector3 Position
    {
        get => this.transform.position;
    }

    #endregion

    #region Fonctions Unity

    private void Awake()
    {
        this._oldPosition = this.transform.position;
    }

    private void Update()
    {
        if (this._oldPosition != this.transform.position)
        {
            this._oldPosition = this.transform.position;
            this.Emit("PositionChange");
            Debug.Log("Test");
        }
    }

    #endregion
}
