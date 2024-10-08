using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuildingType
{
    None,
    Wall,
    BloodTemple,
    Tower,
    Upgrade
}

[CreateAssetMenu(fileName = "BuildingData", menuName = "New Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    [TextArea(3, 5)]
    public string description;

    public BuildingType buildingType;
    public Building buildingPrefab;
    public Sprite buildingUISprite;
    public int buildingUpgradeMaxLevel;
    public int buildingUpgradeCost;
    public int buildingHealth;
    public int bloodGenerationAmount;
    public int initialBloodCost;
    public float timeToBuild;
}
