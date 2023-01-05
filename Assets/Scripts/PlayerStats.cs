using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;

    [SerializeField] private float maxStamina = 100;

    [SerializeField] private float currentStamina = 100;

    public float CurrentHealth
    {
        get => currentHealth;
    }

    public float CurrentStamina
    {
        get => currentStamina;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // Game over
        }
    }

    public void Heal(float healPoints)
    {
        currentHealth = currentHealth + healPoints;

        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
    }
    public void DrainStamina(float staminaDrain)
    {
        currentStamina -= staminaDrain;
    }

    IEnumerator RegenerateStamina()
    {
        while (currentStamina < maxStamina)
        {
            currentStamina += 1;
            yield return new WaitForSeconds(1);
        }
    }
}
