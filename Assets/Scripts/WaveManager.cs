using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> fastEnemies;
    public List<GameObject> mediumEnemies;
    public List<GameObject> slowEnemies;
    public List<GameObject> bosses;

    public Transform[] spawnPoints;
    public Collider2D battleZone;
    public TMP_Text enemyCounterText;

    private int currentWave = 1;
    private bool isSpawning = false;
    private int enemiesRemainingInWave;
    private int currentEnemyCount = 0;

    private float waveStartTime;
    public delegate void WaveStarted(int waveIndex);
    public static event WaveStarted OnWaveStarted;

    private void Start()
    {
        UpdateEnemyCount();
        StartNextWave();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            StartNextWave();
        }

        // Verificar si la oleada puede terminar
        if (currentEnemyCount <= 0)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        if (isSpawning) return;

        isSpawning = true;
        waveStartTime = Time.time; // Registrar el tiempo de inicio de la oleada
        OnWaveStarted?.Invoke(currentWave);

        if (currentWave % 10 == 0)
        {
            StartCoroutine(SpawnBossWave());
        }
        else
        {
            StartCoroutine(SpawnRegularWave());
        }
        currentWave++;
    }

    private IEnumerator SpawnRegularWave()
    {
        int totalEnemies = Mathf.RoundToInt(currentWave * 1.5f) + 3;
        enemiesRemainingInWave = totalEnemies;

        while (totalEnemies > 0)
        {
            GameObject enemyToSpawn = GetEnemyForCurrentWave();
            if (enemyToSpawn != null)
            {
                SpawnEnemy(enemyToSpawn);
                totalEnemies--;
            }

            yield return new WaitForSeconds(1f / (currentWave * 0.2f + 1));
        }

        isSpawning = false;
    }

    public  IEnumerator SpawnBossWave()
    {
        int bossIndex = Mathf.Min(currentWave / 10 - 1, bosses.Count - 1);
        GameObject bossToSpawn = bosses[bossIndex];

        if (bossToSpawn != null)
        {
            SpawnEnemy(bossToSpawn);
            enemiesRemainingInWave = 1;
        }
        else
        {
            enemiesRemainingInWave = 0;
        }

        isSpawning = false;
        yield return null;
    }

    private GameObject GetEnemyForCurrentWave()
    {
        int waveLevel = currentWave;
        int fastChance = Mathf.Clamp(60 - waveLevel, 10, 40);
        int mediumChance = Mathf.Clamp(25 + waveLevel / 2, 20, 50);
        int slowChance = Mathf.Clamp(15 + waveLevel / 1, 2, 3);

        int randomValue = Random.Range(0, 100);

        if (waveLevel > 10 && randomValue < fastChance && fastEnemies.Count > 0)
        {
            return fastEnemies[Random.Range(0, fastEnemies.Count)];
        }

        if (waveLevel > 20 && randomValue < fastChance + mediumChance + slowChance && slowEnemies.Count > 0)
        {
            return slowEnemies[Random.Range(0, slowEnemies.Count)];
        }

        if (mediumEnemies.Count > 0)
        {
            return mediumEnemies[Random.Range(0, mediumEnemies.Count)];
        }

        return null;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab == null || spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        currentEnemyCount++; // Aumentar el contador al generar un enemigo
        UpdateEnemyCount();
    }

    public void OnEnemyDefeated()
    {
        enemiesRemainingInWave--;
        currentEnemyCount--; // Reducir el contador cuando un enemigo muere
        UpdateEnemyCount();
    }

    private void UpdateEnemyCount()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = "Enemigos en juego: " + currentEnemyCount;
        }
    }
}
