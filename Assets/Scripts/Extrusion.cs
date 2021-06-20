using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extrusion : MonoBehaviour
{
    [SerializeField] private BezierCurve _curve;

    //  [SerializeField] private Forme _profile;
    private List<Vector3> _profilePoints;

    private Vector3 getTangente(int t)
    {
        if (t > 0 && t < _curve.Positions.Count)
        {
            Vector3 pMoins1 = _curve.Positions[t - 1];
            Vector3 pPlus1 =  _curve.Positions[t + 1];
            return new Vector3(pMoins1.x - pPlus1.x, pMoins1.y - pPlus1.y, pMoins1.z-pPlus1.z);
        }
        
        Vector3 p =  _curve.Positions[t];

        if (t == 0)
        {
            Vector3 pPlus1 =  _curve.Positions[t + 1];
            return new Vector3(pPlus1.x-p.x , pPlus1.y-p.y , pPlus1.z-p.z);
        }

        if (t != _curve.Positions.Count) return new Vector3(0, 0, 0);
        {
            Vector3 pMoins1 = _curve.Positions[t - 1];
            return new Vector3(p.x - pMoins1.x, p.y - pMoins1.y, p.z-pMoins1.z);
        }
    }
}