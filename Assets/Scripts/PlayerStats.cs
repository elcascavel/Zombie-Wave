using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;

    public float CurrentHealth
    {
        get => currentHealth;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
}
