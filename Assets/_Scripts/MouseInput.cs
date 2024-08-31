using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] LayerMask gridCellMask;
    IGridCell hitGridCell;

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

            }
        }
        else if (hitGridCell != null)
        {
            hitGridCell.UnhighlightCell();
            hitGridCell = null;
        }

    }
}
