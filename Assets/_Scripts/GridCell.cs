using UnityEngine;

public class GridCell : MonoBehaviour, IGridCell
{
    [SerializeField] Material highlightMat;

    Material defaultMat;
    MeshRenderer meshRenderer;

    bool isHighlighted = false;
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
}
