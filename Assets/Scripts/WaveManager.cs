using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private List<GameObject> spawnedEnemies;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private int enemiesPerWave = 10;
    private int enemiesSpawned = 0;
    private int currentWave = 0;
    private float waveTimer = 10f;
    private UIManager uiManager;

    private bool waveObjectiveComplete;

    private bool waveStarted;

    public bool WaveStarted
    {
        get => waveStarted;
        set => waveStarted = value;
    }

    void Start()
    {
        uiManager = GetComponent<UIManager>();
        uiManager.SetWaveText(currentWave);
    }

    void Update()
    {
        if (waveStarted)
        {
            UpdateWaveTimer();
        }
    }

    public IEnumerator StartNextWave()
    {
        currentWave++;
        uiManager.SetWaveText(currentWave);
        enemiesSpawned = 0;
        waveTimer = timeBetweenWaves;
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            enemiesSpawned++;
            int randomEnemy = Random.Range(0, enemies.Length);
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            spawnedEnemies.Add(Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity));
            spawnedEnemies[i].GetComponent<ZombieNavMesh>().Player = player;

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        if (waveObjectiveComplete)
        {
            StartCoroutine(StartNextWave());
        }
    }

    void UpdateWaveTimer()
    {
        if (waveTimer >= 0 && waveStarted)
        {
            waveTimer -= Time.deltaTime;
            uiManager.SetWaveTimerText(Mathf.RoundToInt(waveTimer));
        }
    }
}
