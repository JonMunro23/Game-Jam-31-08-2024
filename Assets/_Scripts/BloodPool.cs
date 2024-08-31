using System;
using TMPro;
using UnityEngine;
public class BloodPool : MonoBehaviour
{
    [SerializeField] private float blood = 100;

    public static event Action<float> OnBloodChanged;

    private void Start()
    {
        UpdateBloodScoreUI();
    }

    private void UpdateBloodScoreUI()
    {
        OnBloodChanged?.Invoke(blood);
    }

    public void AddBlood(float amount)
    {

        blood += amount;
        UpdateBloodScoreUI();
    }

    public void SubtractBlood(float amount)
    {
        blood -= amount;
        UpdateBloodScoreUI();
    }

    public float GetBlood()
    {
        return blood;
    }
}
