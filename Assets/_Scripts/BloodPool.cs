using UnityEngine;
public class BloodPool : MonoBehaviour
{
    [SerializeField] private float blood = 100;

    public void AddBlood(float amount)
    {

        blood += amount;
    }

    public void SubtractBlood(float amount)
    {
        blood -= amount;
    }

    public float GetBlood()
    {
        return blood;
    }
}
