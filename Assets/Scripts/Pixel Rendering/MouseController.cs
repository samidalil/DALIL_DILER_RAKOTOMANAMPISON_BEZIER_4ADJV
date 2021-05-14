using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    public Camera cam;


    public GameObject testSphere;
    private bool _click;
    // Update is called once per frame
    void Update()
    {
        _click = Input.GetMouseButtonDown(0);

        if (_click)
        {
            Vector3 position = Input.mousePosition;

            position.z = 3;
            Debug.Log(cam.ScreenToWorldPoint(position));
            Instantiate(testSphere, cam.ScreenToWorldPoint(position), Quaternion.identity);
        }
    }
}

