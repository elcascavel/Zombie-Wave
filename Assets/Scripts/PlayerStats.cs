using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;

    [SerializeField] private float maxStamina = 100;

    [SerializeField] private float currentStamina = 100;

    [SerializeField] private InputManager inputManager;

    [SerializeField] private UIManager uiManager;

    private float regenRate = 20;

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
            inputManager.onDisable();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            uiManager.deathScreen.SetActive(true);
        }
    }

    public void Heal(float healPoints)
    {
        currentHealth = currentHealth + healPoints;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void DrainStamina(float staminaDrain)
    {
        currentStamina -= staminaDrain;
    }

    public void RegenerateStamina()
    {
        currentStamina += (regenRate * Time.deltaTime);
    }
}
