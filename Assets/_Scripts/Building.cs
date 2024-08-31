
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData buildingData;

    bool isCurrentlySelected;

    private void OnEnable()
    {
        MouseInput.OnOccupiedGridCellClicked += SelectBuilding;
    }

    private void OnDisable()
    {
        MouseInput.OnOccupiedGridCellClicked -= SelectBuilding;
    }

    public void InitBuildingData(BuildingData _buildingData)
    {
        buildingData = _buildingData;
    }

    public void SelectBuilding(GridCell occupiedGridCell)
    {
        if (occupiedGridCell.GetBuilding() != this)
            return;

        if (isCurrentlySelected)
            return;

        isCurrentlySelected = true;
        Debug.Log("Selected " +  occupiedGridCell.GetBuildingData().name);
    }
}
