using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] BuildingData wallBuilding;
    [SerializeField] BuildingData bloodTempleBuilding;
    [SerializeField] BuildingData towerBuilding;
    [SerializeField] BuildingType buildingToPlace = BuildingType.None;

    [SerializeField] AudioClip buildingPlacementAuidoClip;

    private void OnEnable()
    {
        MouseInput.OnEmptyGridCellClicked += PlaceBuilding;
        HUDBuildButton.OnBuildingButtonClick += HandleChangeBuildingType;
    }

    private void OnDisable()
    {
        MouseInput.OnEmptyGridCellClicked -= PlaceBuilding;
        HUDBuildButton.OnBuildingButtonClick -= HandleChangeBuildingType;
    }

    private void HandleChangeBuildingType(BuildingType buildingType)
    {
        buildingToPlace = buildingType;
    }


    BuildingData GetBuildingToBuild()
    {
        switch (buildingToPlace)
        {
            case BuildingType.BloodTemple:
                return bloodTempleBuilding;
            case BuildingType.Wall:
                return wallBuilding;
            case BuildingType.Tower:
                return towerBuilding;
            default:
                return null;
        }
    }

    void BuildBuilding(GridCell gridCell, BuildingData buildingData)
    {
        Building clone = Instantiate(buildingData.buildingPrefab, gridCell.transform.position, gridCell.transform.rotation, gridCell.transform);
        gridCell.SetBuilding(clone);
        clone.InitBuilding(buildingData, gridCell);
        clone.PlayPlacementAudio(buildingPlacementAuidoClip);
    }

    void PlaceBuilding(GridCell gridCell)
    {
        BuildingData buildingData = GetBuildingToBuild();

        if (!buildingData)
        {
            return;
        }

        BuildBuilding(gridCell, buildingData);
    }
}
