using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private PlayerStats playerStats;

    private UIManager uiManager;

    void Start()
    {
        uiManager = GetComponent<UIManager>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        uiManager.HealthBar.SetHealth(playerStats.CurrentHealth);
    }
}
