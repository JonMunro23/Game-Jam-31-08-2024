using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData buildingData;

    public GridCell occupiedGridCell;

    bool isCurrentlySelected;
    public int upgradeLevel = 1;

    AudioSource audioSource;

    public static event Action OnBuildingBuilt;

    private void OnEnable()
    {
        MouseInput.OnOccupiedGridCellClicked += SelectBuilding;
    }

    private void OnDisable()
    {
        MouseInput.OnOccupiedGridCellClicked -= SelectBuilding;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        OnBuildingBuilt?.Invoke();
        FindFirstObjectByType<BloodPool>().SubtractBlood(buildingData.initialBloodCost);
    }

    public virtual void InitBuilding(BuildingData _buildingData, GridCell gridCell)
    {
        buildingData = _buildingData;
        occupiedGridCell = gridCell;
    }

    public void SelectBuilding(GridCell occupiedGridCell)
    {
        if (occupiedGridCell.GetBuilding() != this)
            return;

        if (isCurrentlySelected)
            return;

        isCurrentlySelected = true;
        Debug.Log("Selected " + occupiedGridCell.GetBuildingData().name);
    }

    public void PlayPlacementAudio(AudioClip clipToPlay)
    {
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

    public int GetUpgradeCost()
    {
        return buildingData.buildingUpgradeCost * upgradeLevel;
    }

    public virtual void UpgradeBuilding(AudioClip successSound, AudioClip negativeSound)
    {

        Debug.Log("Upgrade Building");
        if (buildingData.buildingUpgradeMaxLevel == upgradeLevel)
        {
            Debug.Log("Max level reached");
            PlayPlacementAudio(negativeSound);
            return;
        }

        int upgradeCost = GetUpgradeCost();

        if (FindFirstObjectByType<BloodPool>().GetBlood() < upgradeCost)
        {
            Debug.Log("Not enough blood");
            PlayPlacementAudio(negativeSound);
            return;
        }

        FindFirstObjectByType<BloodPool>().SubtractBlood(upgradeCost);
        upgradeLevel++;
        PlayPlacementAudio(successSound);
        Debug.Log("Building upgraded to level " + upgradeLevel);
    }
}
