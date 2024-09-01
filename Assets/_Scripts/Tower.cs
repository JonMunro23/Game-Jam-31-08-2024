using UnityEngine;

public class Tower : Building
{
  public override void InitBuilding(BuildingData _buildingData, GridCell gridCell)
  {
    base.InitBuilding(_buildingData, gridCell);
    Debug.Log("Tower Init");
  }
}
