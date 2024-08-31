using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] BuildingData buildingToPlace;

    [SerializeField] AudioClip buildingPlacementAuidoClip;
 
    private void OnEnable()
    {
        MouseInput.OnEmptyGridCellClicked += PlaceBuilding;
    }

    private void OnDisable()
    {
        MouseInput.OnEmptyGridCellClicked -= PlaceBuilding;
    }

    void PlaceBuilding(GridCell gridCell)
    {
        if (!buildingToPlace)
            return;

        Building clone = Instantiate(buildingToPlace.buildingPrefab, gridCell.transform.position, gridCell.transform.rotation);
        clone.InitBuildingData(buildingToPlace);
        clone.PlayPlacementAudio(buildingPlacementAuidoClip);
        gridCell.SetBuilding(clone);
        
    }
}
