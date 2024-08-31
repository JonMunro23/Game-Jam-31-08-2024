using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] LayerMask gridCellMask;
    IGridCell hitGridCell;

    KeyCode placeBuildingInputKey = KeyCode.Mouse0;

    public static event Action<GridCell> OnEmptyGridCellClicked;
    public static event Action<GridCell> OnOccupiedGridCellClicked;

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
            if(hit.transform.TryGetComponent(out IGridCell gridCell))
            {

                if (hitGridCell != null)
                    if (hitGridCell != gridCell)
                    {
                        hitGridCell.UnhighlightCell();
                    }

                hitGridCell = gridCell;
                hitGridCell.HighlightCell();

                if(Input.GetKeyDown(placeBuildingInputKey))
                {
                    if (!hitGridCell.IsCellOccupied())
                        OnEmptyGridCellClicked?.Invoke(hitGridCell.GetGridCell());
                    else
                        OnOccupiedGridCellClicked?.Invoke(hitGridCell.GetGridCell());
                }
            }
        }
        else if (hitGridCell != null)
        {
            hitGridCell.UnhighlightCell();
            hitGridCell = null;
        }

    }
}
