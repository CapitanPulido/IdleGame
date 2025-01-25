using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves; // Lista de oleadas configuradas

    public Transform[] spawnPoints; // Puntos donde aparecerán los enemigos
    private int currentWaveIndex = 0; // Índice de la oleada actual
    private bool isSpawning = false; // Indica si se están generando enemigos
    private int enemiesRemainingToSpawn;
    private int enemiesRemainingInWave;

    public delegate void WaveStarted(int waveIndex);
    public static event WaveStarted OnWaveStarted;

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("Todas las oleadas han terminado.");
            return;
        }

        Wave currentWave = waves[currentWaveIndex];
        enemiesRemainingToSpawn = GetTotalEnemiesInWave(currentWave);
        enemiesRemainingInWave = enemiesRemainingToSpawn;

        if (OnWaveStarted != null)
        {
            OnWaveStarted.Invoke(currentWaveIndex);
        }

        StartCoroutine(SpawnWave(currentWave));
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        for (int i = 0; i < wave.enemyPrefabs.Count; i++)
        {
            for (int j = 0; j < wave.enemyCounts[i]; j++)
            {
                if (enemiesRemainingToSpawn > 0)
                {
                    SpawnEnemy(wave.enemyPrefabs[i]);
                    enemiesRemainingToSpawn--;
                    yield return new WaitForSeconds(wave.spawnInterval);
                }
            }
        }

        isSpawning = false;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Enemigo generado: " + enemyPrefab.name);
    }

    private int GetTotalEnemiesInWave(Wave wave)
    {
        int total = 0;
        foreach (int count in wave.enemyCounts)
        {
            total += count;
        }
        return total;
    }

    public void OnEnemyDefeated()
    {
        enemiesRemainingInWave--;

        if (enemiesRemainingInWave <= 0 && !isSpawning)
        {
            Debug.Log("Oleada " + currentWaveIndex + " completada.");
            currentWaveIndex++;
            StartNextWave();
        }
    }
}


[System.Serializable]
public class Wave
{
    public string waveName; // Nombre de la oleada
    public List<GameObject> enemyPrefabs; // Prefabs de los enemigos de la oleada
    public List<int> enemyCounts; // Cantidad de cada tipo de enemigo
    public float spawnInterval = 1f; // Intervalo entre spawns
}
