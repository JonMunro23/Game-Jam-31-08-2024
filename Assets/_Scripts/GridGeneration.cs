using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{
    [SerializeField] int xCellAmount, zCellAmount;
    [SerializeField] float xSpaceBetweenCells, zSpaceBetweenCells;
    [SerializeField] GridCell gridCell;

    [SerializeField] float cellSize;
    [SerializeField] BloodPool bloodPoolPrefab;
    GridCell centerCell;

    List<GridCell> spawnedCells = new List<GridCell>();


    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGridCell(int xCoord, int zCoord)
    {

        float posX = transform.position.x + (xCoord * cellSize) + (xCoord * xSpaceBetweenCells);
        float posZ = transform.position.z + (zCoord * cellSize) + (zCoord * zSpaceBetweenCells);
        float posY = 0;

        Vector3 position = new Vector3(posX, posY, posZ);
        GridCell clone = Instantiate(gridCell, position, Quaternion.identity, transform);

        clone.SetGridCoordinates(xCoord, zCoord);
        clone.transform.localScale = new Vector3(cellSize, 1, cellSize);

        spawnedCells.Add(clone);




        bool isCenterCell = xCoord == xCellAmount / 2 && zCoord == zCellAmount / 2;
        if (isCenterCell)
        {
            centerCell = clone;
        }
    }

    void SpawnBloodPool()
    {
        BloodPool bloodPool = Instantiate(bloodPoolPrefab);
        bloodPool.transform.SetParent(centerCell.transform);
        bloodPool.transform.localPosition = new Vector3(0, 0.1f, 0);

        bloodPool.transform.localScale = new Vector3(3, 1, 3);

        centerCell.SetOccupied(true);

        GridCell[] surroundingCells = GetSurroundingCells(centerCell);
        foreach (GridCell cell in surroundingCells)
        {
            if (cell != null)
            {
                cell.SetOccupied(true);
            }
        }

    }

    private GridCell[] GetSurroundingCells(GridCell centralCell)
    {
        List<GridCell> surroundingCells = new List<GridCell>
        {
            GetCell(centralCell.xCoord, centralCell.zCoord + 1),
            GetCell(centralCell.xCoord + 1, centralCell.zCoord),
            GetCell(centralCell.xCoord, centralCell.zCoord - 1),
            GetCell(centralCell.xCoord - 1, centralCell.zCoord),
            GetCell(centralCell.xCoord + 1, centralCell.zCoord + 1),
            GetCell(centralCell.xCoord + 1, centralCell.zCoord - 1),
            GetCell(centralCell.xCoord - 1, centralCell.zCoord + 1),
            GetCell(centralCell.xCoord - 1, centralCell.zCoord - 1)
        };

        return surroundingCells.ToArray();
    }

    public GridCell GetCell(int x, int z)
    {
        GridCell cellToReturn = null;

        foreach (GridCell cell in spawnedCells)
        {
            if (cell.xCoord != x)
                continue;

            if (cell.zCoord != z)
                continue;

            cellToReturn = cell;
        }

        return cellToReturn;
    }

    void GenerateGrid()
    {
        for (int i = 0; i < xCellAmount; i++)
        {
            for (int j = 0; j < zCellAmount; j++)
            {
                GenerateGridCell(i, j);
            }
        }

        SpawnBloodPool();
    }
}
