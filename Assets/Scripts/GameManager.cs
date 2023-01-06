using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private PlayerStats playerStats;

    private UIManager uiManager;

    private WaveManager waveManager;

    private bool gameStarted;

    private float gamePreparationTime = 20f;

    public GameObject Player
    {
        get { return player; }
    }

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        uiManager = GetComponent<UIManager>();
        waveManager = GetComponent<WaveManager>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    void StartGame()
    {
        gameStarted = true;
        uiManager.ShowWaveText();
        uiManager.SetWaveText(0);
        StartCoroutine(waveManager.StartNextWave());
        waveManager.WaveStarted = true;
    }

    bool UpdateGamePreparationTime()
    {
        if (gamePreparationTime >= 0)
        {
            gamePreparationTime -= Time.deltaTime;
            uiManager.SetWaveTimerText(Mathf.RoundToInt(gamePreparationTime));
            return false;
        }
        else
        {
            return true;
        }
    }

    void Update()
    {
        if (UpdateGamePreparationTime() && !gameStarted)
        {
            StartGame();
        }
        uiManager.HealthBar.SetHealth(playerStats.CurrentHealth);
        uiManager.StaminaBar.SetStamina(playerStats.CurrentStamina);
    }
}
