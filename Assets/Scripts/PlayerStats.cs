using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;

    public float CurrentHealth
    {
        get; private set;
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }
}
