using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] LayerMask gridCellMask;
    GameObject hitObject;

    // Update is called once per frame
    void Update()
    {
        MouseRaycast();
    }

    void MouseRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, gridCellMask))
        {
            hitObject = hit.transform.gameObject;
            
        }

    }


}
