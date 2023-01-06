using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private List<GameObject> spawnedEnemies;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private float timeBetweenSpawns = 0.1f;
    [SerializeField] private int enemiesPerWave = 10;
    private int enemiesSpawned = 0;
    private int currentWave = 0;
    private float waveTimer = 10f;
    private UIManager uiManager;
    private bool waveStarted;

    private bool bunker = false;

    private const int WAVE_MAX = 5;

    public bool Bunker
    {
        get => bunker;
    }

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
            CheckWaveObjective();
        }
    }

    public void CheckWaveObjective()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i].GetComponent<Enemy>().isDead)
            {
                Destroy(spawnedEnemies[i], 10f);
                spawnedEnemies.RemoveAt(i);
            }

            if (spawnedEnemies.Count == 0 && enemiesSpawned == enemiesPerWave)
            {
                if (currentWave == WAVE_MAX)
                {
                    bunker = true;
                }
                else
                {
                    StartCoroutine(StartNextWave());
                }
            }
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
            spawnedEnemies[i].GetComponent<Enemy>().Player = player;

            yield return new WaitForSeconds(timeBetweenSpawns);
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
