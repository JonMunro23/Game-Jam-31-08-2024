using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{
    [SerializeField] int xCellAmount, zCellAmount;
    [SerializeField] float xSpaceBetweenCells, zSpaceBetweenCells;
    [SerializeField] GridCell gridCell;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < xCellAmount * zCellAmount; i++)
        {
            GridCell clone = Instantiate(gridCell, new Vector3(transform.position.x + (xSpaceBetweenCells * (i % xCellAmount)), 0, transform.position.z + (zSpaceBetweenCells * (i / xCellAmount))), Quaternion.identity);
        }
    }
}
