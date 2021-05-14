using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class DragableObject : MonoBehaviour

{

    private Vector3 mOffset;
    private float mZCoord;
    void OnMouseDown()
    {
        if (BezierManager.Instance.MenuManager.Mode == Mode.CREATION) return;
        var position = gameObject.transform.position;
        mZCoord = Camera.main.WorldToScreenPoint(
            position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = position - GetMouseAsWorldPoint();

    }
    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()
    {
        if (BezierManager.Instance.MenuManager.Mode == Mode.CREATION) return;
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

}