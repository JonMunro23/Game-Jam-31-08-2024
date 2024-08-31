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

    public void SetBuilding(Building newBuilding)
    {
        builtBuilding = newBuilding;
        isCellOccupied = true;
        meshRenderer.enabled = false;
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


    public bool IsCellOccupied()
    {
        return isCellOccupied;
    }
}
