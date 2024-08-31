using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GridCell : MonoBehaviour, IGridCell
{
    [SerializeField] Material highlightMat;

    Material defaultMat;
    MeshRenderer meshRenderer;

    Building builtBuilding;
    bool isHighlighted = false;
    bool isCellOccupied = false;

    public int xCoord, zCoord;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        defaultMat = meshRenderer.material;
    }

    public void HighlightCell()
    {
        if(!isHighlighted)
        {
            isHighlighted = true;
            meshRenderer.material = highlightMat;
        }    
    }

    public void UnhighlightCell()
    {
        if(isHighlighted)
        {
            isHighlighted = false;
            meshRenderer.material = defaultMat;
        }
    }

    public void SetGridCoordinates(int _xCoord, int _zCoord)
    {
        xCoord = _xCoord;
        zCoord = _zCoord;
    }

    public void SetBuilding(Building newBuilding)
    {
        builtBuilding = newBuilding;
        SetOccupied(true);
    }

    public BuildingData GetBuildingData()
    {
        return builtBuilding.buildingData;
    }

    public Building GetBuilding()
    {
        return builtBuilding;
    }

    public GridCell GetGridCell()
    {
        return this;
    }

    public void SetOccupied(bool _isOccupied)
    {

        if(meshRenderer.enabled)
            meshRenderer.enabled = !_isOccupied;

        isCellOccupied = _isOccupied;
    }

    public bool IsCellOccupied()
    {
        return isCellOccupied;
    }
}
