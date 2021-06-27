using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    #region Evènements

    public event Action OnPositionChanged;

    #endregion

    #region Propriétés

    public Vector3 Position => this.transform.position;

    #endregion

    #region Variables d'instance

    private Vector3 _oldPosition;

    #endregion

    #region Fonctions Unity

    private void Start()
    {
        this._oldPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        if (this._oldPosition != this.transform.position) this.OnPositionChanged?.Invoke();
    }

    #endregion
}
