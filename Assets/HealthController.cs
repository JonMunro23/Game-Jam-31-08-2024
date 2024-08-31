using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour, IDamageable
{
    bool isDead;

    [SerializeField] float maxHealth;
    float currentHealth;
    [Space]
    public UnityEvent onDeathEvents;

    public static event Action<float> onHealthUpdated;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damageTaken)
    {
        if (isDead)
            return;

        currentHealth -= damageTaken;

        if (currentHealth <= 0)
            OnDeath();

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        onHealthUpdated?.Invoke(currentHealth);
    }

    void OnDeath()
    {
        isDead = true;
        onDeathEvents?.Invoke();
    }


    
}
