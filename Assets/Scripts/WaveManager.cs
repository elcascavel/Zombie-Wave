using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public float timeBetweenSpawns = 0.5f;
    public int enemiesPerWave = 10;

    private int enemiesSpawned = 0;
    private int currentWave = 0;

    private UIManager uiManager;

    void Start()
    {
        uiManager = GetComponent<UIManager>();
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        currentWave++;
        uiManager.SetWaveText(currentWave);
        enemiesSpawned = 0;
        uiManager.SetWaveTimerText(currentWave, timeBetweenWaves);
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnEnemies());
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
