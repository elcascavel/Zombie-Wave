using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public float timeBetweenSpawns = 0.5f;
    private float timer = 0f;
    public int enemiesPerWave = 10;
    private int enemiesSpawned = 0;
    private int currentWave = 0;

    private UIManager uiManager;

    void Start()
    {
        uiManager = GetComponent<UIManager>();
        StartCoroutine(StartNextWave());
    }

    void Update()
    {
        UpdateTimer();
    }

    IEnumerator StartNextWave()
    {
        currentWave++;
        uiManager.SetWaveText(currentWave);
        enemiesSpawned = 0;
        timer = timeBetweenWaves;
        uiManager.SetWaveTimerText(currentWave, timer);
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnEnemies());
    }

    void UpdateTimer()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        uiManager.SetWaveTimerText(currentWave, Mathf.RoundToInt(timer));
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < enemiesPerWave)
        {
            enemiesSpawned++;
            int randomEnemy = Random.Range(0, enemies.Length);
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        StartCoroutine(StartNextWave());
    }
}
