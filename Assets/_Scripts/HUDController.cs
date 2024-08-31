using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] TMP_Text bloodText;
    [SerializeField] TMP_Text buildingCounterText;

    int buildingCount = 1;

    private void OnEnable()
    {
        BloodPool.OnBloodChanged += UpdateBloodText;
        Building.OnBuildingBuilt += UpdateBuildingCounter;
    }

    private void OnDisable()
    {
        BloodPool.OnBloodChanged -= UpdateBloodText;
        Building.OnBuildingBuilt -= UpdateBuildingCounter;
    }

    private void UpdateBloodText(float blood)
    {
        bloodText.text = blood.ToString();
    }

    private void UpdateBuildingCounter()
    {
        buildingCount++;
        buildingCounterText.text = buildingCount.ToString();
    }

}
