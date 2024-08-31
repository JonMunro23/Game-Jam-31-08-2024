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


    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGridCell(int xCoord, int zCoord)
    {

        float posX = transform.position.x + (xCoord * cellSize) + xSpaceBetweenCells;
        float posZ = transform.position.z + (zCoord * cellSize) + zSpaceBetweenCells;
        float posY = 0;

        Vector3 position = new Vector3(posX, posY, posZ);
        GridCell clone = Instantiate(gridCell, position, Quaternion.identity);

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

        float bloodPoolSize = cellSize * 3;
        bloodPool.transform.localScale = new Vector3(bloodPoolSize, 1, bloodPoolSize);
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
