using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData buildingData;

    public GridCell occupiedGridCell;

    bool isCurrentlySelected;

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
}
