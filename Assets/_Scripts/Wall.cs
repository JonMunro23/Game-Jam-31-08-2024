
using System.Collections.Generic;
using UnityEngine;

enum WallModelMap
{
  FullWall = 0,
  HalfWall = 1,
  CornerWall = 2,
  TWall = 3,
  CrossWall = 4
}

enum CellDirectionMap
{
  North = 0,
  East = 1,
  South = 2,
  West = 3
}
public class Wall : Building
{
  List<GridCell> surroundingCells;
  int adjacentWallCount = 0;
  public override void InitBuilding(BuildingData _buildingData, GridCell gridCell)
  {
    base.InitBuilding(_buildingData, gridCell);
    GetSurroundingCells();
    SetModel();
  }

  public void SetModel()
  {
    int currentAdjacentWallCount = GetAdjacentWallsCount();

    if (currentAdjacentWallCount == adjacentWallCount) return;

    adjacentWallCount = currentAdjacentWallCount;

    if (adjacentWallCount == 0)
    {
      ShowModel(WallModelMap.CrossWall);
      return;
    }

    if (adjacentWallCount == 1)
    {
      Set1NeighbourModel();
      return;
    }

    if (adjacentWallCount == 2)
    {
      Set2NeighbourModel();
      return;
    }

    if (adjacentWallCount == 3)
    {
      Set3NeighbourModel();
      return;
    }

    if (adjacentWallCount == 4)
    {
      ShowModel(WallModelMap.CrossWall);
      return;
    }
  }


  void ShowModel(WallModelMap model)
  {
    HideAllModels();

    Transform modelToDisplay = transform.GetChild((int)model);
    modelToDisplay.gameObject.SetActive(true);
    modelToDisplay.transform.localScale = new Vector3(1, this.buildingData.buildingUpgradeLevel + 3, 1);

    UpdateNeighbourWalls();
  }

  void HideAllModels()
  {
    for (int i = 0; i < transform.childCount; i++)
    {
      transform.GetChild(i).gameObject.SetActive(false);
    }
  }


  private void UpdateNeighbourWalls()
  {
    surroundingCells.ForEach(cell =>
    {
      if (cell != null)
      {
        if (cell.GetBuilding() is Wall)
        {
          Wall wall = cell.GetBuilding() as Wall;
          wall.SetModel();
        }
      }
    });
  }

  private void Set1NeighbourModel()
  {
    ShowModel(WallModelMap.HalfWall);

    bool hasNorthWall = HasWallInDirection(CellDirectionMap.North);
    bool hasSouthWall = HasWallInDirection(CellDirectionMap.South);
    bool hasEastWall = HasWallInDirection(CellDirectionMap.East);

    if (hasNorthWall)
    {
      transform.rotation = Quaternion.Euler(0, 90, 0);
      return;
    }

    if (hasEastWall)
    {
      transform.rotation = Quaternion.Euler(0, 180, 0);
      return;
    }

    if (hasSouthWall)
    {
      transform.rotation = Quaternion.Euler(0, 270, 0);
      return;
    }
  }

  private void Set2NeighbourModel()
  {

    bool hasNorthWall = HasWallInDirection(CellDirectionMap.North);
    bool hasSouthWall = HasWallInDirection(CellDirectionMap.South);
    bool hasEastWall = HasWallInDirection(CellDirectionMap.East);
    bool hasWestWall = HasWallInDirection(CellDirectionMap.West);

    bool hasOppositeZWalls = HasOppositeZWalls();
    bool hasOppositeXWalls = HasOppositeXWalls();

    if (!hasOppositeZWalls && !hasOppositeXWalls)
    {
      ShowModel(WallModelMap.CornerWall);

      if (hasNorthWall && hasWestWall)
      {
        transform.rotation = Quaternion.Euler(0, 0, 0);
      }

      if (hasNorthWall && hasEastWall)
      {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        return;
      }

      if (hasSouthWall && hasEastWall)
      {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        return;
      }

      if (hasSouthWall && hasWestWall)
      {
        transform.rotation = Quaternion.Euler(0, 270, 0);
        return;
      }

      return;
    }


    ShowModel(WallModelMap.FullWall);

    if (hasOppositeZWalls)
    {
      transform.rotation = Quaternion.Euler(0, 90, 0);
    }
  }

  private void Set3NeighbourModel()
  {
    ShowModel(WallModelMap.TWall);

    bool hasNorthWall = HasWallInDirection(CellDirectionMap.North);
    bool hasSouthWall = HasWallInDirection(CellDirectionMap.South);
    bool hasEastWall = HasWallInDirection(CellDirectionMap.East);
    bool hasWestWall = HasWallInDirection(CellDirectionMap.West);

    if (hasNorthWall && hasSouthWall && hasEastWall)
    {
      transform.rotation = Quaternion.Euler(0, 0, 0);
      return;
    }

    if (hasEastWall && hasSouthWall && hasWestWall)
    {
      transform.rotation = Quaternion.Euler(0, 90, 0);
      return;
    }

    if (hasSouthWall && hasWestWall && hasNorthWall)
    {
      transform.rotation = Quaternion.Euler(0, 180, 0);
      return;
    }

    if (hasWestWall && hasNorthWall && hasEastWall)
    {
      transform.rotation = Quaternion.Euler(0, 270, 0);
      return;
    }
  }


  void GetSurroundingCells()
  {
    surroundingCells = new List<GridCell>
        {
            GetCell(occupiedGridCell.xCoord, occupiedGridCell.zCoord + 1),
            GetCell(occupiedGridCell.xCoord + 1, occupiedGridCell.zCoord),
            GetCell(occupiedGridCell.xCoord, occupiedGridCell.zCoord - 1),
            GetCell(occupiedGridCell.xCoord - 1, occupiedGridCell.zCoord)
        };
  }

  public GridCell GetCell(int x, int z)
  {
    GridCell cellToReturn = null;

    foreach (GridCell cell in FindFirstObjectByType<GridGeneration>().spawnedCells)
    {
      if (cell.xCoord != x)
        continue;

      if (cell.zCoord != z)
        continue;

      cellToReturn = cell;
    }

    return cellToReturn;
  }

  int GetAdjacentWallsCount()
  {
    int currentAdjacentWallCount = 0;

    surroundingCells.ForEach(cell =>
    {
      if (cell != null)
      {
        if (cell.GetBuilding() is Wall)
        {
          currentAdjacentWallCount++;
        }
      }
    });

    return currentAdjacentWallCount;
  }

  private bool HasWallInDirection(CellDirectionMap direction)
  {
    if (!surroundingCells?[(int)direction])
    {
      return false;
    }

    return surroundingCells[(int)direction].GetBuilding() is Wall;
  }

  private bool HasOppositeXWalls()
  {
    return HasWallInDirection(CellDirectionMap.East) && HasWallInDirection(CellDirectionMap.West);
  }

  private bool HasOppositeZWalls()
  {
    return HasWallInDirection(CellDirectionMap.North) && HasWallInDirection(CellDirectionMap.South);
  }


}
