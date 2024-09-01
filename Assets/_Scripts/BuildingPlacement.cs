using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] BuildingData wallBuilding;
    [SerializeField] BuildingData bloodTempleBuilding;
    [SerializeField] BuildingData towerBuilding;
    [SerializeField] BuildingType buildingToPlace = BuildingType.None;

    [SerializeField] AudioClip buildingPlacementAudioClip;
    [SerializeField] AudioClip buildingUpgradeAudioClip;
    [SerializeField] AudioClip negativeSoundAudioClip;

    private void OnEnable()
    {
        MouseInput.OnEmptyGridCellClicked += PlaceBuilding;
        MouseInput.OnOccupiedGridCellClicked += UpgradeBuilding;
        HUDBuildButton.OnBuildingButtonClick += HandleChangeBuildingType;

    }

    private void OnDisable()
    {
        MouseInput.OnEmptyGridCellClicked -= PlaceBuilding;
        MouseInput.OnOccupiedGridCellClicked -= UpgradeBuilding;
        HUDBuildButton.OnBuildingButtonClick -= HandleChangeBuildingType;
    }

    private void HandleChangeBuildingType(BuildingType buildingType)
    {
        buildingToPlace = buildingType;
    }


    private bool IsUpgradeMode()
    {
        return buildingToPlace == BuildingType.Upgrade;
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
        clone.PlayPlacementAudio(buildingPlacementAudioClip);
    }

    void UpgradeBuilding(GridCell gridCell)
    {
        if (!IsUpgradeMode())
            return;


        Building building = gridCell.GetBuilding();
        if (building)
        {
            building.UpgradeBuilding(buildingUpgradeAudioClip, negativeSoundAudioClip);
        }
        return;

    }

    void PlaceBuilding(GridCell gridCell)
    {
        if (IsUpgradeMode())
        {
            return;
        }

        BuildingData buildingData = GetBuildingToBuild();

        if (!buildingData)
        {
            return;
        }

        if (buildingData.initialBloodCost > FindFirstObjectByType<BloodPool>().GetBlood())
        {
            return;
        }

        BuildBuilding(gridCell, buildingData);
    }
}
